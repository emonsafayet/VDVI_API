namespace Framework.Core.Jwt
{
    public static class Constants
    {
        public static class Jwt
        {
            public static class ClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class Claims
            {
                public const string ApiAccess = "api_access";
            }
        }
    }
}
