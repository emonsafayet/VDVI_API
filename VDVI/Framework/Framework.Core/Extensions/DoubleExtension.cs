namespace Framework.Core.Extensions
{
    public static class DoubleExtension
    {
        public static double UpdateIfNotEqual(this double data, double updatedValue)
        {
            if (data != updatedValue)
                data = updatedValue;

            return data;
        }

        public static double? UpdateIfNotEqual(this double? data, double? updatedValue)
        {
            if (data != updatedValue)
                data = updatedValue;

            return data;
        }
    }
}
