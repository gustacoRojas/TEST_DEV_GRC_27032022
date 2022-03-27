using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using static APIExamenTokaRest.Models.ConfigModel;

namespace APIExamenTokaRest
{
    public class IniConfig
    {
        public static void SetConfig(){
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            APIConfigModel.cadenaConexion = config["UserConfigurations:connectionString"];
            APIConfigModel.resultadoOk = config["UserConfigurations:msjResult"];
            APIConfigModel.resultadoVacio = config["UserConfigurations:msjListaVacia"];
        }

    }
}
