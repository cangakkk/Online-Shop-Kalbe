using KalbeShop.DataModels;
using KalbeShop.Repo;
using KalbeShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KalbeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenjualanController : Controller
    {
        private readonly KalbeShopContext db;
        private readonly PenjualanRepo penjualanRepo;
        public PenjualanController(KalbeShopContext _db)
        {
            db = _db;
            penjualanRepo = new PenjualanRepo(db);
        }

        [HttpGet("[action]")]
        public VMResponse GetAll()
        {
            return penjualanRepo.GetAll();
        }

        [HttpGet("[action]")]
        public VMResponse Get(int penjualanId)
        {
            return penjualanRepo.GetByID(penjualanId);
        }

        [HttpPut("[action]")]
        public VMResponse Edit(VMPenjualan dataView)
        {
            return penjualanRepo.Edit(dataView);
        }

        [HttpPost("[action]")]
        public VMResponse Add(VMPenjualan dataView)
        {
            return penjualanRepo.Add(dataView);
        }

        [HttpDelete("[action]")]
        public VMResponse Delete(int penjualanId)
        {
            return penjualanRepo.Delete(penjualanId);
        }
    }
}
