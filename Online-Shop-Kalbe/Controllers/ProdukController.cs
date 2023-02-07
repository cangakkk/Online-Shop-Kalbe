using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Net.Http;
using KalbeShop.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using KalbeShop.ViewModels;
using Xpos307.ViewModels;
using System.Linq;
using System.Text;
using KalbeShop.DataModels;

namespace Online_Shop_Kalbe.Controllers
{
    public class ProdukController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private string errMsg;

        public ProdukController(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public async Task<IActionResult> Index(VMPage page)
        {
            VMResponse apiResponse = new VMResponse();

            if (string.IsNullOrEmpty(page.Name))
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + "api/Produk/GetAll"));
            else
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + "api/Produk/GetByName?Name=" + page.Name));

            List<VMProduk> data = JsonConvert.DeserializeObject<List<VMProduk>>(apiResponse.entity.ToString());

            if (data == null || apiResponse.Success == false)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }

            return View(data);
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.Title = "Produk Add";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(VMProduk dataView)
        {
            string jsonData = JsonConvert.SerializeObject(dataView);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var apiResponse = JsonConvert.DeserializeObject<VMResponse>(await (
                                   await httpClient.PostAsync(apiUrl + "api/Produk/Add", content)
                             ).Content.ReadAsStringAsync());
            if (!apiResponse.Success)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int produkid)
        {
            VMResponse apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                                    await httpClient.GetStringAsync(apiUrl + "api/produk/Get?produkid=" + produkid));

            VMProduk data = JsonConvert.DeserializeObject<VMProduk>(apiResponse.entity.ToString());

            if (data == null || apiResponse.Success == false)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Edit Produk";
            return View(data);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMProduk dataView)
        {
            string jsonData = JsonConvert.SerializeObject(dataView);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var apiResponse = JsonConvert.DeserializeObject<VMResponse>(await (
                                   await httpClient.PutAsync(apiUrl + "api/Produk/Edit", content)
                             ).Content.ReadAsStringAsync());

            if (!apiResponse.Success)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int produkid)
        {
            VMResponse apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                                    await httpClient.GetStringAsync(apiUrl + "api/Produk/Get?produkid=" + produkid));

            VMProduk data = JsonConvert.DeserializeObject<VMProduk>(apiResponse.entity.ToString());

            if (data == null || apiResponse.Success == false)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Delete Variant";
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VMProduk dataView)
        {

            VMResponse apiResponse = JsonConvert.DeserializeObject<VMResponse>(await (
                                    await httpClient.DeleteAsync(apiUrl + "api/Produk/Delete?produkid=" + dataView.IntProductId)
                              ).Content.ReadAsStringAsync());

            if (!apiResponse.Success)
            {
                string errorMag = apiResponse.message;
            }

            return RedirectToAction("Index");
        }
    }
}
