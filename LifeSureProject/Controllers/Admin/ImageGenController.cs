using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class ImageGenController : Controller
    {
        private static readonly string apiKey = "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"; // Hugging Face API Key        
        private readonly string modelUrl = "https://api-inference.huggingface.co/models/stabilityai/stable-diffusion-2-1";
        //private readonly string modelUrl = "https://api-inference.huggingface.co/models/black-forest-labs/FLUX.1-schnell";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                ViewBag.Error = "Lütfen bir açıklama (prompt) girin.";
                return View();
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                var json = JsonConvert.SerializeObject(new { inputs = prompt });
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(modelUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var imageBytes = await response.Content.ReadAsByteArrayAsync();

                    // 1. Base64 verisi oluştur (görüntülemek için)
                    string base64Image = Convert.ToBase64String(imageBytes);
                    ViewBag.ImageData = $"data:image/png;base64,{base64Image}";
                    ViewBag.Prompt = prompt;

                    // 2. Sunucuya kaydet
                    string fileName = $"image_{DateTime.Now.Ticks}.png";
                    string folderPath = Server.MapPath("~/Content/Images/Generated/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string fullPath = Path.Combine(folderPath, fileName);

                    // 3. Fiziksel dosya yaz
                    System.IO.File.WriteAllBytes(fullPath, imageBytes);

                    // 4. View'da kullanmak için yol hazırla
                    ViewBag.ImagePath = Url.Content("~/Content/Images/Generated/" + fileName);
                }

                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = $"Hata oluştu: {response.StatusCode} - {error}";
                }
            }

            return View();
        }
    }
}
