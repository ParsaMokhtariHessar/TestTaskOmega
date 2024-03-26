namespace TestTaskOmega.Identity.Utilities
{
    public static class RolesExtensions
    {
        private static readonly Dictionary<Roles, string> roleToString = new Dictionary<Roles, string>
        {
            { Roles.User, "User" },
            { Roles.Manager, "Manager" }
        };

        public static string ToRoleString(this Roles role)
        {
            return roleToString[role];
        }
    }
}
