using System;

namespace Framework.Core.Extensions
{
    public static class DatetimeExtension
    {
        public static string EncodeTransmissionTimestamp(this DateTime date)
        {
            long shortTicks = (date.Ticks - 631139040000000000L) / 10000L;
            return Convert.ToBase64String(BitConverter.GetBytes(shortTicks)).Substring(0, 7);
        }

        public static DateTime UpdateIfNotEqual(this DateTime data, DateTime updatedValue)
        {
            if (DateTime.Compare(data, updatedValue) != 0)
                data = updatedValue;

            return data;
        }

        public static DateTime? UpdateIfNotEqual(this DateTime? data, DateTime? updatedValue)
        {
            if (data != updatedValue)
                data = updatedValue;

            if (Nullable.Compare(data, updatedValue) != 0)
                data = updatedValue;

            return data;
        }

        public static DateTime FixDateTime(this DateTime date)
        {
            if (date > DateTime.UtcNow)
                date = date.AddYears(-1);

            return date;
        }

        
    }
}
