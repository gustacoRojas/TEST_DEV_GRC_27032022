using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIExamenTokaRest.Models;

namespace APIExamenTokaRest.Business
{
    public interface IPersonaBusiness
    {
        public IList<persona> ObtenerListaPersonas();

        public string AgregaPersona(persona personaFiscal);

        public string ModificaPersona(persona personaFiscal);

        public string EliminaPersona(int idpersona);

        

    }
}
