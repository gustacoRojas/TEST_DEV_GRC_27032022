using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WEBExamenTokaMVC.Models;
using System.Net.Http;
using static WEBExamenTokaMVC.Models.ConfigModel;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using Nancy.Json;
using Microsoft.AspNetCore.Authorization;

namespace WEBExamenTokaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<ActionResult> Index()
        {
            List<Persona> personasFisicas = new List<Persona>();
            try {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(WEBConfigModel.uriBaseAPI);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(WEBConfigModel.uriEspeAPI);
                    if (response.IsSuccessStatusCode)
                    {
                        var personaResponse = response.Content.ReadAsStringAsync().Result;

                        personasFisicas = JsonConvert.DeserializeObject<List<Persona>>(personaResponse);

                    }
                }

            } catch (Exception ex) {

                EventLog.WriteEntry("WEBExamenTokaMVC", ex.Message, EventLogEntryType.Information, 999);
            }


            return View(personasFisicas);
        }

       [HttpGet]
        public async Task<ActionResult> EliminaPersonaFisica(int idPersona)
        {

            string resultado = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(WEBConfigModel.uriBaseAPI);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.DeleteAsync(WEBConfigModel.uriEspeAPI+idPersona);
                    
                    resultado = response.Content.ReadAsStringAsync().Result;
                    
                }

            }
            catch (Exception ex)
            {

                EventLog.WriteEntry("WEBExamenTokaMVC", ex.Message, EventLogEntryType.Information, 999);
            }


            var jsonSerialiser = new JavaScriptSerializer();
            var msjJson = jsonSerialiser.Serialize(resultado);
            return Json(msjJson);
        }


        [HttpGet]
        public async Task<ActionResult> AgregaPersonaFisica(int idPersona, string nombre, string app, string apm, string rfc, int agregaUsr, string fecha)
        {
            DateTime date = Convert.ToDateTime(fecha);
            string resultado = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {

                    var nuevaPersona = new Persona();
                    nuevaPersona.id = idPersona;
                    nuevaPersona.nombre = nombre;
                    nuevaPersona.apellidoPa = app;
                    nuevaPersona.apellidoMa = apm;
                    nuevaPersona.rfc = rfc;
                    nuevaPersona.usuarioAgrega = agregaUsr;
                    nuevaPersona.fechaNacimiento = date;

                    var json = JsonConvert.SerializeObject(nuevaPersona);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    client.BaseAddress = new Uri(WEBConfigModel.uriBaseAPI);
                    var response = await client.PostAsync(WEBConfigModel.uriEspeAPI, data);

                    resultado = response.Content.ReadAsStringAsync().Result;

                }

            }
            catch (Exception ex)
            {

                EventLog.WriteEntry("WEBExamenTokaMVC", ex.Message, EventLogEntryType.Information, 999);
            }


            var jsonSerialiser = new JavaScriptSerializer();
            var msjJson = jsonSerialiser.Serialize(resultado);
            return Json(msjJson);
        }

        [HttpGet]
        public async Task<ActionResult> EditaPersonaFisica(int idPersona, string nombre, string app, string apm, string rfc, int agregaUsr, string fecha)
        {
            DateTime date = Convert.ToDateTime(fecha);
      
            string resultado = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {

                    var nuevaPersona = new Persona();
                    nuevaPersona.id = idPersona;
                    nuevaPersona.nombre = nombre;
                    nuevaPersona.apellidoPa = app;
                    nuevaPersona.apellidoMa = apm;
                    nuevaPersona.rfc = rfc;
                    nuevaPersona.usuarioAgrega = agregaUsr;
                    nuevaPersona.fechaNacimiento = date;

                    var json = JsonConvert.SerializeObject(nuevaPersona);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    client.BaseAddress = new Uri(WEBConfigModel.uriBaseAPI);
                    var response = await client.PutAsync(WEBConfigModel.uriEspeAPI, data);

                    resultado = response.Content.ReadAsStringAsync().Result;

                }

            }
            catch (Exception ex)
            {

                EventLog.WriteEntry("WEBExamenTokaMVC", ex.Message, EventLogEntryType.Information, 999);
            }


            var jsonSerialiser = new JavaScriptSerializer();
            var msjJson = jsonSerialiser.Serialize(resultado);
            return Json(msjJson);
        }


        public async Task<ActionResult> Privacy()
        {
            List<personasToka> listPersonasTokas = new List<personasToka>();
            JObject jsonToken = new JObject();

            try {
                using (var client = new HttpClient())
                {

                    var logginToken = new userTokenToka();
                    logginToken.Username = WEBConfigModel.userApiToken;
                    logginToken.Password = WEBConfigModel.passApiToken;

                    var json = JsonConvert.SerializeObject(logginToken);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(WEBConfigModel.urlTokenToka, data);


                    if (response.IsSuccessStatusCode)
                    {
                        string content = response.Content.ReadAsStringAsync().Result;
                        jsonToken = JObject.Parse(content);

                    }
                }
                tokenToka deserializedToken = JsonConvert.DeserializeObject<tokenToka>(jsonToken.ToString()); ;



                ResulPersonasToka resListaPersonasToka = new ResulPersonasToka();



                using (var client = new HttpClient())
                {


                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", deserializedToken.Data);

                    HttpResponseMessage response = await client.GetAsync(WEBConfigModel.urlPersonasToka);
                    if (response.IsSuccessStatusCode)
                    {
                        var personaResponse = response.Content.ReadAsStringAsync().Result;
                        JObject jsonPersonas = JObject.Parse(personaResponse);
                        resListaPersonasToka = JsonConvert.DeserializeObject<ResulPersonasToka>(jsonPersonas.ToString());

                        listPersonasTokas = resListaPersonasToka.Data;

                    }
                }
            }
            catch (Exception ex) {
                EventLog.WriteEntry("WEBExamenTokaMVC", ex.Message, EventLogEntryType.Information, 999);
            }

            return View(listPersonasTokas);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
