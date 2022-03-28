using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBExamenTokaMVC.Models
{
    public class Persona
    {
         
            public int id { get; set; }
   
            public string nombre { get; set; }
   
            public string apellidoPa { get; set; }
   
            public string apellidoMa { get; set; }

            public string rfc { get; set; }

            public int usuarioAgrega { get; set; }

            public DateTime fechaNacimiento { get; set; }
        
    }
}
