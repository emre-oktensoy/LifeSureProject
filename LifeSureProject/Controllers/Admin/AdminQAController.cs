using LifeSureProject.Models.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace LifeSureProject.Controllers.Admin
{
    public class AdminQAController : Controller
    {
        private readonly string apiKey = "3"; 
        private readonly string apiHost = "open-ai21.p.rapidapi.com";
        private readonly string apiUrl = "https://open-ai21.p.rapidapi.com/chatgbt";

        LifeSureDbEntities1 db = new LifeSureDbEntities1();

        public ActionResult AdminQAList()
        {
            var values = db.TblFAQ.ToList();
            return View(values); ;
        }
        [HttpGet]
        public ActionResult UpdateAdminQAList(int id)
        {
            var value = db.TblFAQ.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateAdminQAList(TblFAQ tblFAQ)
        {
            var value = db.TblFAQ.Find(tblFAQ.FAQId);
            value.Question = tblFAQ.Question;
            value.Answer = tblFAQ.Answer;
            db.SaveChanges();
            return RedirectToAction("AdminQAList");
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Index(string konu)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", apiHost);

                string prompt = $"'{konu}' hakkında en sık sorulan 1 soruyu ve kısa cevabını ver. Format: **Soru:** ... **Cevap:** ...";

                var requestBody = new
                {
                    messages = new[] {
                new { role = "user", content = prompt }
            }
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // DÜZELTİLMİŞ endpoint:
                var response = await client.PostAsync("https://open-ai21.p.rapidapi.com/chatgpt", content);
                var result = await response.Content.ReadAsStringAsync();

                dynamic parsed = JsonConvert.DeserializeObject(result);

                if (parsed != null && parsed.result != null)
                {
                    string fullText = parsed.result.ToString();
                    fullText = fullText.Replace("\n", "<br/>");

                    string[] parts = fullText.Split(new[] { "**Cevap:**" }, StringSplitOptions.None);
                    string soru = parts[0].Replace("**Soru:**", "").Trim();
                    string cevap = parts.Length > 1 ? parts[1].Trim() : "";

                    ViewBag.Soru = soru;
                    ViewBag.Cevap = cevap;
                }
                else
                {
                    ViewBag.Hata = "API'den anlamlı bir yanıt alınamadı.";
                }

                ViewBag.RawJson = result;
            }

            return View();
        }


    }
}



