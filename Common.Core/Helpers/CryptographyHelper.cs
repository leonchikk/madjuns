﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.Core.Helpers
{
    public static class CryptographyHelper
    {
        public static string EncryptString(string input)
        {
            var buffer = Encoding.UTF8.GetBytes(input);
            string output = BitConverter.ToString(new SHA512Managed().ComputeHash(buffer)).Replace("-", "");
            return output;
        }
    }
}