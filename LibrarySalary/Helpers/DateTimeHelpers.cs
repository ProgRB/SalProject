using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Salary.Helpers
{
    public static class DateTimeHelpers
    {
        /// <summary>
        /// Округляет дату до начала периода, указанного в формате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="format"> формат - это Year или Month</param>
        /// <returns></returns>
        public static DateTime Trunc(this DateTime sender, string format)
        {
            if (format.Trim().ToUpper() == "MONTH")
                return new DateTime(sender.Year, sender.Month, 1);
            else
                if (format.Trim().ToUpper() == "YEAR")
                    return new DateTime(sender.Year, 1, 1);
                else throw new ArgumentOutOfRangeException();
        }
    }
}
