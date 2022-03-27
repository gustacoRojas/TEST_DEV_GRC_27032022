using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIExamenTokaRest.Models;
using APIExamenTokaRest.Data;



namespace APIExamenTokaRest.Business
{
    public class PersonaBusiness : IPersonaBusiness
    {

        IConexionBD operacionesBD = new ConexionBD();
        public IList<persona> ObtenerListaPersonas() {
            IList<persona> arregloListaPersonas = new List<persona>();
            arregloListaPersonas = operacionesBD.ObtenerListaPersonasBD();
            return arregloListaPersonas;
        }

        public string AgregaPersona(persona personaFiscal) {

            return operacionesBD.AgregaPersonaBD(personaFiscal);
        }

        public string ModificaPersona(persona personaFiscal) {
            return operacionesBD.ModificaPersonaBD(personaFiscal);
        }

        public string EliminaPersona(int idpersona)
        {
           return operacionesBD.EliminaPersonaBD(idpersona);
        }

    }
}
