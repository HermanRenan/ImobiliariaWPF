using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationAppresentation.Helper
{
    public static class ExtensionHelper
    {
        static public string getDateNow()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}
