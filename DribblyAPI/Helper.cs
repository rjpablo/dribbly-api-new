using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace DribblyAPI
{
    public class Helper
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
       
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string validateCity(City city)
        {
            if (city.longName == "" || city.shortName == "")
            {
                return "invalid city";
            }

            if (city.country != null)
            {
                if (city.country.longName == "" || city.country.shortName == "")
                {
                    return "city has invalid country details";
                }
            }
            else
            {
                return "country is missing";
            }
            return "";
        }
    }
}