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

            using(var client = new HttpClient()) {

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

            return View(personasFisicas);
        }

        public async Task<ActionResult> Privacy()
        {
                        
            JObject jsonToken = new JObject();



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

            List<personasToka> listPersonasTokas = new List<personasToka>();

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

            return View(listPersonasTokas);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
