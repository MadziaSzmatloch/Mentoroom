namespace Mentoroom.DOMAIN.Models
{
    public static class UserRoles
    {
        /// <summary>
        /// The role for administrator
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// The role name for lecturer, when applyed change student role to this
        /// </summary>
        public const string Lecturer = "Lecturer";

        /// <summary>
        /// The role name for regular users
        /// </summary>
        public const string Student = "Student";
    }
}
