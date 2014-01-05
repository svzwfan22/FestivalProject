﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Project_MVVM.errorhandling
{
    class Email : ValidationRule
    {
        private int _min;
        private int _max;

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ValidationResult result = null;
            string pattern = @"^[_A-Za-z0-9-]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,4})$";
            string input = value.ToString();
            if (input.Length > Min && input.Length < Max)
            {
                try
                {
                    if (Regex.IsMatch(input, pattern))
                    {
                        result = new ValidationResult(true, null);
                    }
                    else
                    {
                        result = new ValidationResult(false, "De email moet van deze structuur zijn naam0123@msn.com");
                    }
                }
                catch (Exception)
                {
                    result = new ValidationResult(false, "De email moet van deze structuur zijn naam0123@msn.com");
                }
            }
            else
            {
                result = new ValidationResult(false, "De email moet tss " + Min + " en " + Max + " liggen.");
            }
            return result;
        }
    }
}
