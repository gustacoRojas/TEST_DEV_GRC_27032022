using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIExamenTokaRest.Models;
using APIExamenTokaRest.Business;
using static APIExamenTokaRest.Models.ConfigModel;

namespace APIExamenTokaRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasFisicasController : ControllerBase
    {
        IPersonaBusiness operacionesPersonas = new PersonaBusiness();

        [HttpPost]
        public IActionResult AgregarPersona(persona datosPersona)
        {
            var res = operacionesPersonas.AgregaPersona(datosPersona);
            if (res.Length.Equals(APIConfigModel.resultadoOk))
                return Ok(res); ;

            return BadRequest(res);
        }

        [HttpGet]
        public IActionResult EnlistarPersona()
        {
            var list = operacionesPersonas.ObtenerListaPersonas();
            if (list.Count == 0)
                return NotFound(APIConfigModel.resultadoVacio);

            return Ok(list);
            
        }


        [HttpPut]
        public IActionResult ModificarPersona(persona datosPersona)
        {
            var res = operacionesPersonas.ModificaPersona(datosPersona);
            if (res.Length.Equals(APIConfigModel.resultadoOk))
                return Ok(res); ;

            return BadRequest(res);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPersona(int id)
        {
            var res = operacionesPersonas.EliminaPersona(id);
            if (res.Length.Equals(APIConfigModel.resultadoOk))
                return Ok(res); ;

            return BadRequest(res);
        }

        

    }
}
