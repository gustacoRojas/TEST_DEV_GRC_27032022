using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIExamenTokaRest.Models;

namespace APIExamenTokaRest.Data
{
    public interface IConexionBD
    {
        public IList<persona> ObtenerListaPersonasBD();

        public string AgregaPersonaBD(persona personaFiscal);

        public string ModificaPersonaBD(persona personaFiscal);

        public string EliminaPersonaBD(int idpersona);


    }
}
