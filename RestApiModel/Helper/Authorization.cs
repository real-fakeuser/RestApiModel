﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace RestApiModel.Helper
{
    public class Authorization
    {
        public bool Check(string HeaderAuthString)
        {
            string AuthString = _ConvertBaseToString(_GetStringFromBase(HeaderAuthString));
            string[] UserData = new string[2];          //Separating username and password string
            int i = 0;
            foreach (var part in AuthString.Split(":"))
            {
                UserData[i] = part;
                i++;
            }
            //Console.WriteLine(_ComputeSha256Hash(UserData[1]));
            if (_CheckPermission(UserData[0], _ComputeSha256Hash(UserData[1])))
            {
                return true;
            }
            else
            {
                return _CheckPermission(UserData[0], UserData[1]);
            }
        }

        private bool _CheckPermission(string Username, string PasswordHash)
        {
            string UserHash = "C1ED8AA69E43CA9175EE1C1079B326787710A7323C4183DE39C9DDC7D600095D";
            if(Username == "Tim")
            {
                PasswordHash = PasswordHash.ToUpper();
                if (PasswordHash.Equals(UserHash))
                {
                    return true;
                }
                
            }
            
            
                return false;
            

        }
        static string _ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string _GetStringFromBase(string _AuthRaw, string StringSeparators = " ")
        {
            string[] _AuthParts = _AuthRaw.Split(StringSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (_AuthParts.Length < 2)
            {
                throw new RepoException(EnumResultTypes.ARGUMENTNOTSPECIFIED);
            }
            else if(_AuthParts.Length > 2)
            {
                throw new RepoException(EnumResultTypes.TOOMANYARGUMENTS);
            }
            switch (_AuthParts[0].ToLower())
            {
                case "basic":
                    return _AuthParts[1];
                default:
                    throw new RepoException(EnumResultTypes.ARGUMENTNOTSPECIFIED);
            }
        }

        private static string _ConvertBaseToString(string inputStr)
        {
            byte[] decodedByteArray =
              Convert.FromBase64CharArray(inputStr.ToCharArray(),
                                            0, inputStr.Length);
            var str = System.Text.Encoding.Default.GetString(decodedByteArray);
            return Convert.ToString(str);
        }


    }
}
