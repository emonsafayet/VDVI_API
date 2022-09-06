namespace Framework.Constants
{
    public static partial class Constants
    {
        public static class Jwt
        {
            public static class ClaimIdentifiers
            {
                public const string Rol = "rol", UserId = "userId";
            }

            public static class Claims
            {
                public const string ApiAccess = "api_access";
                public const string CompanyId = "companyId";
            }
        }
    }
}
