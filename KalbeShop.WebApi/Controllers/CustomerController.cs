using KalbeShop.DataModels;
using KalbeShop.Repo;
using KalbeShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KalbeShop.WebApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CustomerController : Controller
   {
       private readonly KalbeShopContext db;
       private readonly CustomerRepo customerRepo;
       public CustomerController(KalbeShopContext _db)
       {
           db = _db;
           customerRepo = new CustomerRepo(db);
       }

       [HttpGet("[action]")]
       public VMResponse GetAll()
       {
           return customerRepo.GetAll();
       }

       [HttpGet("[action]")]
       public VMResponse Get(int customerid)
       {
           return customerRepo.GetByID(customerid);
       }

       [HttpPut("[action]")]
       public VMResponse Edit(VMCustomer dataView)
       {
           return customerRepo.Edit(dataView);
       }

       [HttpPost("[action]")]
       public VMResponse Add(VMCustomer dataView)
       {
           return customerRepo.Add(dataView);
       }

       [HttpDelete("[action]")]
       public VMResponse Delete(int customerid)
       {
           return customerRepo.Delete(customerid);
       }
   }
}
