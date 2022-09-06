namespace Framework.Core.Extensions
{
    public static class BoolExtension
    {
        public static bool UpdateIfNotEqual(this bool data, bool updatedValue)
        {
            if (data != updatedValue)
                data = updatedValue;

            return data;
        }
    }
}
