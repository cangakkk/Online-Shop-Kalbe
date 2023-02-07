using KalbeShop.DataModels;
using KalbeShop.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace KalbeShop.Repo
{
    public class PenjualanRepo
    {
        private readonly KalbeShopContext db;
        private readonly VMResponse response = new VMResponse();

        public PenjualanRepo(KalbeShopContext _db)
        {
            db = _db;
        }

        public VMResponse GetAll()
        {
            try
            {
                List<VMPenjualan> DataView = (
                from p in db.Penjualan
                join c in db.Customer
                 on p.IntCustomerId equals c.IntCustomerId
                join prod in db.Produk
                 on p.IntProductId equals prod.IntProductId
                select new VMPenjualan
                {
                    IntSalesOrderId= p.IntSalesOrderId,
                    TxtCustomerName= c.TxtCustomerName,
                    TxtProductName = prod.TxtProductName,
                    DtSalesOrder = p.DtSalesOrder,
                    IntQty= p.IntQty

                }).ToList();
                response.message = (DataView.Count > 0)
                    ? $"Penjualan data Successfully fetched!"
                    : $"Penjualan data is not available!";
                response.entity = DataView;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.message = "Error Get Data!" + e.Message;
            }
            return response;
        }

        public VMResponse GetByID(int penjualanId)
        {
            try
            {
                VMPenjualan DataView = (
                from p in db.Penjualan
                where p.IntSalesOrderId == penjualanId
                select new VMPenjualan
                {
                    IntSalesOrderId = p.IntSalesOrderId,
                    IntCustomerId = p.IntCustomerId,
                    IntProductId = p.IntProductId,
                    DtSalesOrder = p.DtSalesOrder,
                    IntQty = p.IntQty

                }).SingleOrDefault();
                response.message = (DataView != null)
                    ? $"Penjualan data Successfully fetched!"
                    : $"Penjualan data is not available!";
                response.entity = DataView;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.message = "Error Get Data!" + e.Message;
            }
            return response;
        }

        public VMResponse Edit(VMPenjualan dataView)
        {
            try
            {
                Penjualan DataModel = db.Penjualan.Find(dataView.IntSalesOrderId);
                if (DataModel == null)
                {
                    response.Success = false;
                    response.message = $"Produk with ID = {dataView.IntProductId} is not available!";
                }
                else
                {
                    DataModel.IntProductId = dataView.IntProductId;
                    DataModel.IntCustomerId = dataView.IntCustomerId;
                    DataModel.IntSalesOrderId = dataView.IntSalesOrderId;
                    DataModel.IntQty = dataView.IntQty;

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

        public VMResponse Add(VMPenjualan dataView)
        {
            try
            {
                Penjualan DataModel = new Penjualan();

                DataModel.IntProductId = dataView.IntProductId;
                DataModel.IntCustomerId = dataView.IntCustomerId;
                DataModel.IntQty = dataView.IntQty;

                DataModel.DtSalesOrder= DateTime.Now;

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

        public VMResponse Delete(int penjualanId)
        {
            try
            {
                Penjualan DataModel = db.Penjualan.Find(penjualanId);
                if (DataModel == null)
                {
                    response.Success = false;
                    response.message = $"Produk with ID = {DataModel.IntSalesOrderId} is not available!";
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
