using KalbeShop.DataModels;
using KalbeShop.Repo;
using KalbeShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KalbeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdukController : Controller
    {
        private readonly KalbeShopContext db;
        private readonly ProdukRepo produkRepo;
        public ProdukController(KalbeShopContext _db)
        {
            db = _db;
            produkRepo = new ProdukRepo(db);
        }

        [HttpGet("[action]")]
        public VMResponse GetAll()
        {
            return produkRepo.GetAll();
        }

        [HttpGet("[action]")]
        public VMResponse Get(int produkid)
        {
            return produkRepo.Get(produkid);
        }

        [HttpPut("[action]")]
        public VMResponse Edit(VMProduk dataView)
        {
            return produkRepo.Edit(dataView);
        }

        [HttpPost("[action]")]
        public VMResponse Add(VMProduk dataView)
        {
            return produkRepo.Add(dataView);
        }

        [HttpDelete("[action]")]
        public VMResponse Delete(int produkid)
        {
            return produkRepo.Delete(produkid);
        }
    }
}
