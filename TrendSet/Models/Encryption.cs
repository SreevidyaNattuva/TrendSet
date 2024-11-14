using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrendSet.Models
{
    public class Encryption
    {
        public static string Encrypt(string Password)
        {
            byte[] datainByte = new byte[Password.Length];
            datainByte = Encoding.UTF8.GetBytes(Password);
            return Convert.ToBase64String(datainByte);
            //return Password;
        }
    }
}