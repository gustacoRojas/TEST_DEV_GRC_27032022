using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WEBExamenTokaMVC.Models
{
    public class ConfigModel
    {

        public class WEBConfigModel
        {
            public static string uriBaseAPI { get; set; }
            public static string uriEspeAPI { get; set; }
            public static string urlTokenToka { get; set; }
            public static string urlPersonasToka { get; set; }
            public static string userApiToken { get; set; }
            public static string passApiToken { get; set; }
        }

    }
}
