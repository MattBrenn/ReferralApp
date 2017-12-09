using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReferralCodeGenerator
{
    public class CodeGenerator
    {
        public string GetCodeFromId(int customerid)
        {
            string cid = customerid.ToString();

            char[] letters = "KFRMTYUHWE".ToArray();
            string referralCode = "";

            for (int i = 0; i < cid.Length; i++)
                switch (cid[i])
                {
                    case '0':
                        referralCode += letters[0].ToString();
                        break;
                    case '1':
                        referralCode += letters[1].ToString();
                        break;
                    case '2':
                        referralCode += letters[2].ToString();
                        break;
                    case '3':
                        referralCode += letters[3].ToString();
                        break;
                    case '4':
                        referralCode += letters[4].ToString();
                        break;
                    case '5':
                        referralCode += letters[5].ToString();
                        break;
                    case '6':
                        referralCode += letters[6].ToString();
                        break;
                    case '7':
                        referralCode += letters[7].ToString();
                        break;
                    case '8':
                        referralCode += letters[8].ToString();
                        break;
                    case '9':
                        referralCode += letters[9].ToString();
                        break;
                }

            if (referralCode.Length == 1)
            {
                referralCode += "XXXX";
            }
            else if (referralCode.Length == 2)
            {
                referralCode += "XXX";
            }
            else if (referralCode.Length == 3)
            {
                referralCode += "XX";
            }
            else if (referralCode.Length == 4)
            {
                referralCode += "X";
            }
            return referralCode;
        }
        public int GetCustomerIdFromCode(string referralCode)
        {
            // if element = x, remove
            Regex.Replace(referralCode, "X", string.Empty);

            char[] numbers = "0123456789".ToArray();
            string customerid = "";

            for (int i = 0; i < referralCode.Length; i++)
            {
                switch (referralCode[i])
                {
                    case 'K':
                        customerid += numbers[0].ToString();
                        break;
                    case 'F':
                        customerid += numbers[1].ToString();
                        break;
                    case 'R':
                        customerid += numbers[2].ToString();
                        break;
                    case 'M':
                        customerid += numbers[3].ToString();
                        break;
                    case 'T':
                        customerid += numbers[4].ToString();
                        break;
                    case 'Y':
                        customerid += numbers[5].ToString();
                        break;
                    case 'U':
                        customerid += numbers[6].ToString();
                        break;
                    case 'H':
                        customerid += numbers[7].ToString();
                        break;
                    case 'W':
                        customerid += numbers[8].ToString();
                        break;
                    case 'E':
                        customerid += numbers[9].ToString();
                        break;
                }
            }
            int cid = int.Parse(customerid);
            return cid;
        }
    }
}