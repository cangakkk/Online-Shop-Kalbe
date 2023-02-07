using KalbeShop.DataModels;
using KalbeShop.ViewModels;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KalbeShop.Repo
{ 
    public class ProdukRepo
    {
        private readonly KalbeShopContext db;
        private readonly VMResponse response = new VMResponse();

        public ProdukRepo(KalbeShopContext _db)
        {
            db = _db;
        }

        public VMResponse GetAll()
        {
            try
            {
                List<VMProduk> DataView = (
                from p in db.Produk
                select new VMProduk
                {
                   IntProductId= p.IntProductId,
                   TxtProductCode= p.TxtProductCode,
                   TxtProductName= p.TxtProductName,
                   IntQty= p.IntQty,
                   DecPrice= p.DecPrice,
                   DtInserted= p.DtInserted

                }).ToList();
                response.message = (DataView.Count > 0)
                    ? $"Produk data Successfully fetched!"
                    : $"Produk data is not available!";
                response.entity = DataView;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.message = "Error Get Data!" + e.Message;
            }
            return response;
        }

        public VMResponse Get(int produkid)
        {
            try
            {
                VMProduk DataView = (
                from p in db.Produk
                where p.IntProductId == produkid
                select new VMProduk
                {
                    IntProductId = p.IntProductId,
                    TxtProductCode = p.TxtProductCode,
                    TxtProductName = p.TxtProductName,
                    IntQty = p.IntQty,
                    DecPrice = p.DecPrice,
                    DtInserted = p.DtInserted

                }).SingleOrDefault();
                response.message = (DataView != null)
                    ? $"Produk data Successfully fetched!"
                    : $"Produk data is not available!";
                response.entity = DataView;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.message = "Error Get Data!" + e.Message;
            }
            return response;
        }

        public VMResponse Edit(VMProduk dataView)
        {
            try
            {
                Produk DataModel = db.Produk.Find(dataView.IntProductId);
                if (DataModel == null)
                {
                    response.Success = false;
                    response.message = $"Produk with ID = {dataView.IntProductId} is not available!";
                }
                else
                {
                    DataModel.IntProductId = dataView.IntProductId;
                    DataModel.TxtProductName = dataView.TxtProductName;
                    DataModel.TxtProductCode = dataView.TxtProductCode;
                    DataModel.DecPrice = dataView.DecPrice;
                    DataModel.IntQty = dataView.IntQty;

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

        public VMResponse Add(VMProduk dataView)
        {
            try
            {
                Produk dataModel = new Produk();

                dataModel.TxtProductName = dataView.TxtProductName;
                dataModel.TxtProductCode = dataView.TxtProductCode;

                dataModel.IntQty = dataView.IntQty;
                dataModel.DecPrice = dataView.DecPrice;

                dataModel.DtInserted = DateTime.Now;

                db.Add(dataModel);
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

        public VMResponse Delete(int produkid)
        {
            try
            {
                Produk DataModel = db.Produk.Find(produkid);
                if (DataModel == null)
                {
                    response.Success = false;
                    response.message = $"Produk with ID = {produkid} is not available!";
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
