using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBExamenTokaMVC.Models
{


    public class ResulPersonasToka
    {
        public List<personasToka> Data { get; set; }
    }

    public class personasToka
    {
        public int idCliente { get; set; }

        public DateTime fechaRegistro { get; set; }

        public string razonSocial { get; set; }

        public string rfc { get; set; }

        public string sucursal { get; set; }

        public int idEmpleado { get; set; }

        public string nombre { get; set; }

        public string Paterno { get; set; }

        public string Materno { get; set; }

        public int idViaje { get; set; }


    }

    public class userTokenToka
    { 
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class tokenToka
    {
        public string Data { get; set; }
    }

}