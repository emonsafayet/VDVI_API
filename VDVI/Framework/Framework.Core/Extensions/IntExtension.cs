namespace Framework.Core.Extensions
{
    public static class IntExtension
    {
        public static int UpdateIfNotEqual(this int data, int updatedValue)
        {
            if (data != updatedValue)
                data = updatedValue;

            return data;
        }

        public static int? UpdateIfNotEqual(this int? data, int? updatedValue)
        {
            if (data != updatedValue)
                data = updatedValue;

            return data;
        }
    }
}
