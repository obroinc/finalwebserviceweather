using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebService.Model;

namespace WebService.Pages
{
    public class DisplayPostsModel : PageModel
    {
        

        private readonly IHttpClientFactory _clientFactory;

        public DisplayPostsModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }



         public int? Id { get; set; }
        public string Name { get; set; }
        public Rootobject Rootobject { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            return Page();

        }


        public async Task<IActionResult> OnPostAsync(string Name)
        {
            var client = _clientFactory.CreateClient();

            try
            {
                client.BaseAddress = new Uri("https://api.openweathermap.org");
                HttpResponseMessage response = await client.GetAsync($"/data/2.5/weather?q={Name},ire&APPID=2fa6123cf4b9294239a28ef16033c218&units=metric");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                Rootobject = JsonConvert.DeserializeObject<Rootobject>(stringResult);
                return Page();
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error getting data from jsonplaceholder {httpRequestException.Message}");
            }
            

        }
    }
}