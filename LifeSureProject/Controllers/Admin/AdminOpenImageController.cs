using LifeSureProject.Models.DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers.Admin
{  
    public class AdminOpenImageController : Controller
    {
        
        private readonly string rapidApiKey = "1";
        private readonly string imageApiUrl = "https://open-ai21.p.rapidapi.com/texttoimage2";
        private readonly string paraphraserApiUrl = "https://paraphraser12.p.rapidapi.com";

        LifeSureDbEntities1 db = new LifeSureDbEntities1();

        public ActionResult AdminServiceBoxList()
        {
            var values = db.TblServiceBox.ToList();
            return View(values); ;
        }
        [HttpGet]
        public ActionResult UpdateAdminServiceBox(int id, string image)
        {
            var box = db.TblServiceBox.Find(id);
            if (box == null) return HttpNotFound();

            if (!string.IsNullOrEmpty(image))
                box.ImageUrl = image; // View'da inputa otomatik yazılır

            return View(box);
        }


        [HttpPost]
        public ActionResult UpdateAdminServiceBox(TblServiceBox tblServiceBox)
        {
            var value = db.TblServiceBox.Find(tblServiceBox.ServiceBoxID);
            value.Title = tblServiceBox.Title;
            value.Description = tblServiceBox.Description;
            value.ImageUrl = tblServiceBox.ImageUrl;

            db.SaveChanges();
            return RedirectToAction("AdminServiceBoxList");
        }

        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string konu, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (string.IsNullOrWhiteSpace(konu))
            {
                ViewBag.Error = "Lütfen bir konu giriniz.";
                return View();
            }

            System.Diagnostics.Debug.WriteLine("🔹 Girilen konu: " + konu);

            // 1. Türkçe konuyu İngilizceye çevir
            string englishPrompt = await TranslateToEnglish(konu);
            System.Diagnostics.Debug.WriteLine("🔹 İngilizce çeviri: " + englishPrompt);

            // 2. İngilizce prompt'u Paraphraser API ile genişlet
            string enhancedPrompt = await ParaphrasePrompt(englishPrompt);
            System.Diagnostics.Debug.WriteLine("🔹 Geliştirilmiş prompt: " + enhancedPrompt);

            // 3. Görsel üret
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", rapidApiKey);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "open-ai21.p.rapidapi.com");

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new { text = enhancedPrompt });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(imageApiUrl, content);
                var result = await response.Content.ReadAsStringAsync();

                JObject parsed;
                try
                {
                    parsed = JObject.Parse(result);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "JSON parse hatası: " + ex.Message;
                    return View();
                }

                if (parsed["generated_image"] != null)
                {
                    string imageUrl = parsed["generated_image"].ToString();
                    string fileName = "image_" + Guid.NewGuid().ToString("N");
                    string savePath = "";
                    string relativePath = "";

                    try
                    {
                        using (var imgClient = new HttpClient())
                        {
                            // Görseli indir
                            var imageResponse = await imgClient.GetAsync(imageUrl);
                            imageResponse.EnsureSuccessStatusCode();

                            var contentType = imageResponse.Content.Headers.ContentType?.MediaType;
                            string extension = ".jpg"; // varsayılan

                            // MIME tipine göre uzantıyı belirle
                            switch (contentType)
                            {
                                case "image/png":
                                    extension = ".png";
                                    break;
                                case "image/jpeg":
                                    extension = ".jpg";
                                    break;
                                case "image/gif":
                                    extension = ".gif";
                                    break;
                                    // Başka formatlar gerekiyorsa ekleyebilirsin
                            }

                            fileName += extension;
                            savePath = Server.MapPath("~/Content/Images/" + fileName);
                            relativePath = Url.Content("~/Content/Images/" + fileName);

                            // Baytları yaz
                            var imageBytes = await imageResponse.Content.ReadAsByteArrayAsync();
                            System.IO.File.WriteAllBytes(savePath, imageBytes);
                        }

                        ViewBag.ImageUrl = relativePath;
                        

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            // URL'ye image parametresi ekleyerek yönlendirme linki oluştur (ister View'da ister Controller'da kullan)
                            var redirectUri = new UriBuilder(Request.Url.Scheme + "://" + Request.Url.Authority + returnUrl);
                            var query = HttpUtility.ParseQueryString(redirectUri.Query);
                            query["image"] = relativePath;
                            redirectUri.Query = query.ToString();
                            ViewBag.RedirectUrl = redirectUri.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Resim kaydedilirken hata: " + ex.Message;
                    }
                }


                ViewBag.Konu = konu;
                ViewBag.RawJson = result;
            }

            return View();
        }
              
        private async Task<string> TranslateToEnglish(string text)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", rapidApiKey);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "google-translate113.p.rapidapi.com");

                var encodedText = HttpUtility.UrlEncode(text);
                var bodyContent = new StringContent($"from=auto&to=en&text={encodedText}", Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync("https://google-translate113.p.rapidapi.com/api/v1/translator/text", bodyContent);
                var result = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine("🔸 Translate yanıtı: " + result);

                try
                {
                    var json = JObject.Parse(result);
                    return json["trans"]?.ToString() ?? text;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("⚠️ JSON Parse Hatası: " + ex.Message);
                    return text;
                }
            }
        }

        private async Task<string> ParaphrasePrompt(string englishText)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", rapidApiKey);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "paraphraser1.p.rapidapi.com");

                var requestBody = new { input = englishText };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(paraphraserApiUrl, content);
                var result = await response.Content.ReadAsStringAsync();

                var parsed = JObject.Parse(result);
                return parsed["paraphrased"]?.ToString() ?? englishText;
            }
        }
    }
}

 

