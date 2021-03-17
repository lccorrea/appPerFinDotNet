using System;

namespace appPerfinAPI.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime dateTime)
        {
            var hoje = DateTime.UtcNow;
            int age = hoje.Year - dateTime.Year;
            if (hoje < dateTime.AddYears(age))
                age--;
            return age;
        }
    }
}