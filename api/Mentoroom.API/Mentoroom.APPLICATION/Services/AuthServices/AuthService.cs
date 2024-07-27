using Mentoroom.APPLICATION.Managements.Auth.Login;
using Mentoroom.APPLICATION.Managements.Auth.Logout;
using Mentoroom.APPLICATION.Managements.Auth.Register;
using Mentoroom.APPLICATION.Services.AuthServices.JwtToken;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.DOMAIN.Models;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mentoroom.APPLICATION.Services.AuthServices
{
    public class AuthService(
        IStudentRepository studentRepository,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        AppDbContext context,
        TokenSettings tokenSettings) : IAuthService
    {
        private readonly IStudentRepository studentRepository = studentRepository;
        private readonly UserManager<AppUser> userManager = userManager;
        private readonly SignInManager<AppUser> signInManager = signInManager;
        private readonly AppDbContext context = context;
        private readonly TokenSettings tokenSettings = tokenSettings;

        public async Task<JwtTokenDto> SignIn(Login request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            else
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);
                if (result.Succeeded)
                {
                    var token = await GenerateUserToken(user);
                    return token;
                }
                else
                {
                    throw new Exception("Wrong password");
                }
            }
        }

        public async Task<JwtTokenDto> SignUp(Register request)
        {
            if (request.Password.Equals(request.PasswordConfirm))
            {
                var user = new AppUser()
                {
                    UserName = request.Email,
                    FirstName = request.FistName,
                    LastName = request.LastName,
                    Email = request.Email
                };

                var result = await userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var registeredUser = await userManager.FindByEmailAsync(request.Email);
                    if (registeredUser != null)
                    {
                        return await GenerateUserToken(registeredUser);
                    }
                    else
                    {
                        throw new Exception("Something went wrong. Try again");
                    }
                }
                else
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            else
            {
                throw new Exception("Passwords doesnt match");
            }
        }

        public async Task<JwtTokenDto> RefreshToken(JwtTokenDto request)
        {
            var principal = TokenUtils.GetPrincipalFromExpiredToken(tokenSettings, request.AccessToken);
            if (principal == null || principal.FindFirst("Email")?.Value == null)
            {
                throw new Exception("User not found");
            }
            else
            {
                var user = await userManager.FindByEmailAsync(principal.FindFirst("Email")?.Value ?? "");
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                else
                {
                    if (!await userManager.VerifyUserTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken", request.RefreshToken))
                    {
                        throw new Exception("Refresh token expired");
                    }
                    return await GenerateUserToken(user);
                }
            }
        }

        public async Task<bool> Logout(Logout request)
        {
            if (request.ClaimsPrincipal.Identity?.IsAuthenticated ?? false)
            {
                try
                {
                    var email = request.ClaimsPrincipal.Claims.First(x => x.Type == "Email").Value.ToUpper();
                    var appUser = context.Users.First(x => x.NormalizedEmail == email);
                    if (appUser != null)
                    {
                        await userManager.UpdateSecurityStampAsync(appUser);
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> PromoteTo(string role, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User with given id not found!");
            }

            IdentityResult? result = null;

            var userRoles = await userManager.GetRolesAsync(user);

            if (userRoles.Any(x => x.Equals(UserRoles.Student)))
            {
                await studentRepository.RemoveStudentByUserId(userId);
            }

            await userManager.RemoveFromRolesAsync(user, userRoles);

            switch (role)
            {
                case UserRoles.Admin:
                    result = await userManager.AddToRoleAsync(user, UserRoles.Admin);
                    break;
                case UserRoles.Lecturer:
                    result = await userManager.AddToRoleAsync(user, UserRoles.Lecturer);
                    break;
                case UserRoles.Student:
                    result = await userManager.AddToRoleAsync(user, UserRoles.Student);
                    break;
                default:
                    throw new Exception("Something went wrong!");
            }

            return result != null && result.Succeeded;
        }

        private async Task<JwtTokenDto> GenerateUserToken(AppUser user)
        {
            var claims = (from ur in context.UserRoles
                          where ur.UserId == user.Id
                          join r in context.Roles on ur.RoleId equals r.Id
                          join rc in context.RoleClaims on r.Id equals rc.RoleId
                          select rc)
              .Where(rc => !string.IsNullOrEmpty(rc.ClaimValue) && !string.IsNullOrEmpty(rc.ClaimType))
              .Select(rc => new Claim(rc.ClaimType!, rc.ClaimValue!))
              .Distinct()
              .ToList();

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = TokenUtils.GenerateToken(tokenSettings, user, claims);
            await userManager.RemoveAuthenticationTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken");
            var refreshToken = await userManager.GenerateUserTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken");
            await userManager.SetAuthenticationTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken", refreshToken);

            return new JwtTokenDto() { AccessToken = token, RefreshToken = refreshToken };
        }
    }
}