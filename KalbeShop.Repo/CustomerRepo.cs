using KalbeShop.DataModels;
using KalbeShop.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace KalbeShop.Repo
{
    public class CustomerRepo
    {
        private readonly KalbeShopContext db;
        private readonly VMResponse response = new VMResponse();

        public CustomerRepo(KalbeShopContext _db)
        {
            db = _db;
        }

        public VMResponse GetAll()
        {
            try
            {
                List<VMCustomer> DataView = (
                from c in db.Customer
                select new VMCustomer
                {
                    IntCustomerId = c.IntCustomerId,
                    TxtCustomerName= c.TxtCustomerName,
                    TxtCustomerAddress= c.TxtCustomerAddress,
                    BitGender= c.BitGender,
                    DtmBirthDate= c.DtmBirthDate,
                    DtInserted= c.DtInserted

                }).ToList();
                response.message = (DataView.Count > 0)
                    ? $"Customer data Successfully fetched!"
                    : $"Customer data is not available!";
                response.entity = DataView;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.message = "Error Get Data!" + e.Message;
            }
            return response;
        }

        public VMResponse GetByID(int customerid)
        {
            try
            {
                VMCustomer DataView = (
                from c in db.Customer
                where c.IntCustomerId == customerid
                select new VMCustomer
                {
                    IntCustomerId = c.IntCustomerId,
                    TxtCustomerName = c.TxtCustomerName,
                    TxtCustomerAddress = c.TxtCustomerAddress,
                    BitGender = c.BitGender,
                    DtmBirthDate = c.DtmBirthDate,
                    DtInserted = c.DtInserted

                }).SingleOrDefault();
                response.message = (DataView != null)
                    ? $"Customer data Successfully fetched!"
                    : $"Customer data is not available!";
                response.entity = DataView;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.message = "Error Get Data!" + e.Message;
            }
            return response;
        }

        public VMResponse Edit(VMCustomer dataView)
        {
            try
            {
                Customer DataModel = db.Customer.Find(dataView.IntCustomerId);
                if (DataModel == null)
                {
                    response.Success = false;
                    response.message = $"Produk with ID = {dataView.IntCustomerId} is not available!";
                }
                else
                {
                    DataModel.IntCustomerId = dataView.IntCustomerId;
                    DataModel.TxtCustomerName = dataView.TxtCustomerName;
                    DataModel.TxtCustomerAddress = dataView.TxtCustomerAddress;
                    DataModel.BitGender = dataView.BitGender;
                    DataModel.DtmBirthDate = dataView.DtmBirthDate;

                    DataModel.DtInserted = DateTime.Now;

                    db.Update(DataModel);
                    db.SaveChanges();

                    response.message = "Data Succesfully Update!";
                    response.entity = dataView;
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.message = "Save Failed!" + e.Message;
            }
            return response;
        }

        public VMResponse Add(VMCustomer dataView)
        {
            try
            {
                Customer DataModel = new Customer();

                DataModel.IntCustomerId = dataView.IntCustomerId;
                DataModel.TxtCustomerName = dataView.TxtCustomerName;
                DataModel.TxtCustomerAddress = dataView.TxtCustomerAddress;
                DataModel.BitGender = dataView.BitGender;
                DataModel.DtmBirthDate = dataView.DtmBirthDate;

                DataModel.DtInserted = DateTime.Now;

                db.Add(DataModel);
                db.SaveChanges();

                response.message = "Data Baru Berhasil Ditambahkan!";
                response.entity = dataView;

            }
            catch (Exception e)
            {
                response.Success = false;
                response.message = "Data Ditambahkan Error" + e.Message;
            }
            return response;
        }

        public VMResponse Delete(int customerid)
        {
            try
            {
                Customer DataModel = db.Customer.Find(customerid);
                if (DataModel == null)
                {
                    response.Success = false;
                    response.message = $"Customer with ID = {customerid} is not available!";
                }
                else
                {
                    db.Remove(DataModel);
                    db.SaveChanges();

                    response.message = "Data Succesfully Delete!";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.message = "Error Delete" + e.Message;
            }
            return response;
        }
    }
}
