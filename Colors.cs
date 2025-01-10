using System;
using System.Collections.Generic;
namespace LogsProtocolAdvanced
{
    public class Colors
    {
        public static int FromHex(string hex)
        {
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            return Convert.ToInt32(hex, 16);
        }
    }
}
