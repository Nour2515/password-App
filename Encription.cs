using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace passwordmanager
{
    internal class Encription
    {
        // shoud be same length
        private static readonly String original = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly String encrypt_one = "kN2pFqX1z8aYcR7bM5tJvL6oUeH3W4gVZ0yilSdxCrBnAOhwPDT9fKuGjEs";


        public static string encrypt(string password)
        {
            var sb = new StringBuilder();
            foreach (var ch in password)
            {
                var index = original.IndexOf(ch);
                if (index >= 0 && index < encrypt_one.Length)
                {
                    sb.Append(encrypt_one[index]);
                }
                else
                {
                    sb.Append(ch); 
                }
            }
            return sb.ToString();
        }

        public static string decrypt(string password)
        {
            var sb = new StringBuilder();
            foreach (var ch in password)
            {
                var index = encrypt_one.IndexOf(ch);
                if (index >= 0 && index < original.Length)
                {
                    sb.Append(original[index]);
                }
                else
                {
                    sb.Append(ch);
                }

            }
            return sb.ToString();
        }
    }
}