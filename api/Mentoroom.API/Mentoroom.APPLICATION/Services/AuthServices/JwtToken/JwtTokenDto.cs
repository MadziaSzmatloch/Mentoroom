﻿namespace Mentoroom.APPLICATION.Services.AuthServices.JwtToken
{
    public class JwtTokenDto
    {
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
}
