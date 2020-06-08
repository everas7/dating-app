using System;

namespace Helpers
{
    public static class Extensions
    {
        public static int CalculateAge(this DateTime dateTime)
        {
            var age = DateTime.Now.Year - dateTime.Year;
            if (dateTime.AddYears(age) > DateTime.Today)
                age--;

            return age;
        }
    }
}