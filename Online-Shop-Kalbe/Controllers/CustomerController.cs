using KalbeShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xpos307.ViewModels;

namespace Online_Shop_Kalbe.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private string errMsg;

        public CustomerController(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public async Task<IActionResult> Index(VMPage page)
        {
            VMResponse apiResponse = new VMResponse();

            if (string.IsNullOrEmpty(page.Name))
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + "api/Customer/GetAll"));
            else
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + "api/Customer/GetByName?Name=" + page.Name));

            List<VMCustomer> data = JsonConvert.DeserializeObject<List<VMCustomer>>(apiResponse.entity.ToString());

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
        public async Task<IActionResult> Add(VMCustomer dataView)
        {
            string jsonData = JsonConvert.SerializeObject(dataView);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var apiResponse = JsonConvert.DeserializeObject<VMResponse>(await (
                                   await httpClient.PostAsync(apiUrl + "api/Customer/Add", content)
                             ).Content.ReadAsStringAsync());

            if (!apiResponse.Success)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int customerid)
        {
            VMResponse apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                                    await httpClient.GetStringAsync(apiUrl + "api/Customer/Get?customerid=" + customerid));

            VMCustomer data = JsonConvert.DeserializeObject<VMCustomer>(apiResponse.entity.ToString());

            if (data == null || apiResponse.Success == false)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Edit Produk";
            return View(data);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMCustomer dataView)
        {
            string jsonData = JsonConvert.SerializeObject(dataView);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var apiResponse = JsonConvert.DeserializeObject<VMResponse>(await (
                                   await httpClient.PutAsync(apiUrl + "api/Customer/Edit", content)
                             ).Content.ReadAsStringAsync());

            if (!apiResponse.Success)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int customerid)
        {
            VMResponse apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                                    await httpClient.GetStringAsync(apiUrl + "api/Customer/Get?customerid=" + customerid));

            VMCustomer data = JsonConvert.DeserializeObject<VMCustomer>(apiResponse.entity.ToString());

            if (data == null || apiResponse.Success == false)
            {
                string errorMag = apiResponse.message;
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Delete Variant";
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VMCustomer dataView)
        {

            VMResponse apiResponse = JsonConvert.DeserializeObject<VMResponse>(await (
                                    await httpClient.DeleteAsync(apiUrl + "api/Customer/Delete?customerid=" + dataView.IntCustomerId)
                              ).Content.ReadAsStringAsync());

            if (!apiResponse.Success)
            {
                string errorMag = apiResponse.message;
            }

            return RedirectToAction("Index");
        }
    }
}
