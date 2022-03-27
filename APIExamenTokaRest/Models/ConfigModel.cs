using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIExamenTokaRest.Models
{
    public class ConfigModel
    {

        public class APIConfigModel { 
            public static string cadenaConexion { get; set; }
            public static string resultadoOk { get; set; }
            public static string resultadoVacio { get; set; }
        }

    }
}
