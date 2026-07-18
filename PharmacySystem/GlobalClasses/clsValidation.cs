using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PharmacySystem.GlobalClasses
{
    public class clsValidation
    {
        public static bool EmailValidate(string email) 
        {
            string Pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            Regex regex = new Regex(Pattern);
            return regex.IsMatch(email);
        }

        public static bool ValidateIntegar(string number) 
        {
            string Pattern = @"^[0-9]*$";
            Regex regex = new Regex(Pattern);
            return regex.IsMatch(number);
        }

        public static bool ValidateFloat(string Number)
        {
            string Pattern = @"^[0-9]*(?:\.[0-9]*)?$";
            Regex regex = new Regex(Pattern);
            return regex.IsMatch(Number);
        }

        public static bool IsNumber(string Number) 
        {
            return ValidateIntegar(Number) || ValidateFloat(Number);
        }
    }
}
