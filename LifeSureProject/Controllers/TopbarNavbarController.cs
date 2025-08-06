using LifeSureProject.Models;
using LifeSureProject.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class TopbarNavbarController : Controller
    {

        public ActionResult TopbarNavbarPartialDefault()
        {
            string apiKey = "2";
            string apiHost = "instagram-statistics-api.p.rapidapi.com";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", apiHost);

            string instagramUrl = "https://www.instagram.com/turkiyesigorta/";
            string twitterUrl = "https://twitter.com/Turkiye_Sigorta";

            string instagramApiUrl = $"https://instagram-statistics-api.p.rapidapi.com/community?url={instagramUrl}";
            string twitterApiUrl = $"https://instagram-statistics-api.p.rapidapi.com/community?url={twitterUrl}";

            var instagramResponse = client.GetAsync(instagramApiUrl).Result;
            var twitterResponse = client.GetAsync(twitterApiUrl).Result;

            var model = new CombinedSocialData();

            if (instagramResponse.IsSuccessStatusCode)
            {
                var json = instagramResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<InstagramApiResponse>(json);
                model.Instagram = result.Data;
            }

            if (twitterResponse.IsSuccessStatusCode)
            {
                var json = twitterResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<InstagramApiResponse>(json);
                model.Twitter = result.Data;
            }

            return PartialView("_TopbarNavbarPartial", model);
        }


        public JsonResult GetSocialMediaStats()
        {
            string apiKey = "e1a5d17523msh0ca2ad180b79317p1740c3jsna7e56514cda4";
            string apiHost = "instagram-statistics-api.p.rapidapi.com";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", apiHost);

            string instagramUrl = "https://www.instagram.com/turkiyesigorta/";
            string twitterUrl = "https://twitter.com/Turkiye_Sigorta";

            string instagramApiUrl = $"https://instagram-statistics-api.p.rapidapi.com/community?url={instagramUrl}";
            string twitterApiUrl = $"https://instagram-statistics-api.p.rapidapi.com/community?url={twitterUrl}";

            var model = new CombinedSocialData();

            var instagramResponse = client.GetAsync(instagramApiUrl).Result;
            var twitterResponse = client.GetAsync(twitterApiUrl).Result;

            if (instagramResponse.IsSuccessStatusCode)
            {
                var json = instagramResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<InstagramApiResponse>(json);
                model.Instagram = result.Data;
            }

            if (twitterResponse.IsSuccessStatusCode)
            {
                var json = twitterResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<InstagramApiResponse>(json);
                model.Twitter = result.Data;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }


    }
}


    
