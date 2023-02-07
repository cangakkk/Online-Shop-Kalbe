using KalbeShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xpos307.ViewModels;

namespace Online_Shop_Kalbe.Controllers
{
    public class PenjualanController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private string errMsg;

        public PenjualanController(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public async Task<IActionResult> Index(VMPage page)
        {
            VMResponse apiResponse = new VMResponse();

            if (string.IsNullOrEmpty(page.Name))
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + "api/Penjualan/GetAll"));
            else
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + "api/Penjualan/GetByName?Name=" + page.Name));

            List<VMPenjualan> data = JsonConvert.DeserializeObject<List<VMPenjualan>>(apiResponse.entity.ToString());

            if (data == null || apiResponse.Success == false)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }

            return View(data);
        }

        public async Task<IActionResult> Add()
        {
            VMResponse apiCustomer = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + "api/Customer/GetAll"));

            List<VMCustomer> dataCust = JsonConvert.DeserializeObject<List<VMCustomer>>(apiCustomer.entity.ToString());

            VMResponse apiProduk = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + "api/Produk/GetAll"));

            List<VMProduk> dataProd = JsonConvert.DeserializeObject<List<VMProduk>>(apiProduk.entity.ToString());

            ViewBag.Title = "Produk Add";
            ViewBag.Produk = dataProd;
            ViewBag.Customer = dataCust;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(VMProduk dataView)
        {
            string jsonData = JsonConvert.SerializeObject(dataView);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var apiResponse = JsonConvert.DeserializeObject<VMResponse>(await (
                                   await httpClient.PostAsync(apiUrl + "api/Penjualan/Add", content)
                             ).Content.ReadAsStringAsync());
            if (!apiResponse.Success)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
