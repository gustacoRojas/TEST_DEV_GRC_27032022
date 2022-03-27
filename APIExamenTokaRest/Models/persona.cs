using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIExamenTokaRest.Models
{


   
    public class personasFisicas {
        [JsonPropertyName("PersonasFisicas")]
        public IList<persona> listaPersonas { get; set; }
    }
    public class persona
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("nombre")]
        public string nombre { get; set; }

        [JsonPropertyName("apellidoPa")]
        public string apellidoPa { get; set; }

        [JsonPropertyName("apellidoMa")]
        public string apellidoMa { get; set; }

        [JsonPropertyName("rfc")]
        public string rfc { get; set; }

        [JsonPropertyName("usuarioAgrega")]
        public int usuarioAgrega { get; set; }

        [JsonPropertyName("fechaNacimiento")]
        public DateTime fechaNacimiento { get; set; }
    }

}
