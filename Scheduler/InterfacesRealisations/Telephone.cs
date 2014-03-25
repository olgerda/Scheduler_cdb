using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesRealisations
{
    public class Telephone: Scheduler_Controls_Interfaces.ITelephone
    {
        ulong number;

        public Telephone()
        {
            number = 0;
        }

        public Telephone(ulong telephoneNumber)
        {
            number = telephoneNumber;
        }

        public Telephone(string telephoneNumber)
        {
            if (!ulong.TryParse(GetOnlyDigitsFromString(telephoneNumber), out number))
                number = 0;
        }

        string Scheduler_Controls_Interfaces.ITelephone.TelephoneNumber
        {
            get
            {
                return number.ToString();
            }
            set
            {
                if (!ulong.TryParse(GetOnlyDigitsFromString(value), out number))
                    number = 0;
            }
        }

        string Scheduler_Controls_Interfaces.ITelephone.FormattedTelephoneNumber
        {
            get { return number.ToString("0(000) 000-0000"); }
        }

        private string GetOnlyDigitsFromString(string input)
        {
            string result = String.Empty;
            foreach (var c in input)
            {
                if (Char.IsDigit(c))
                    result += c;
            }
            return result;
        }
    }
}
