using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WEBExamenTokaMVC.Models.ConfigModel;

namespace WEBExamenTokaMVC
{
    public class IniConfig
    {
        public static void SetConfig()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            WEBConfigModel.uriBaseAPI = config["UserConfigurations:uriBaseApi"];
            WEBConfigModel.uriEspeAPI = config["UserConfigurations:uriEspecifictApi"];
            WEBConfigModel.urlTokenToka = config["UserConfigurations:urlTokenToka"];
            WEBConfigModel.urlPersonasToka = config["UserConfigurations:urlPersonasToka"];
            WEBConfigModel.userApiToken = config["UserConfigurations:userTokenAPI"];
            WEBConfigModel.passApiToken = config["UserConfigurations:passTokenAPI"];

        }
    }
}
