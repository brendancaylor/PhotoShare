using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoShare.BusinessLogic;
using PhotoShare.Dto;

namespace WebApplication.Controllers
{
    public class PublicController : Controller
    {
        // GET: Public
        public ActionResult Index()
        {
            return View();
        }

        // GET: Public
        public ActionResult ViewCode(string viewingcode)
        {
            var model = new List<PhotoShare.Dto.vwPhotosAndSale>();
            var logic = new GeneralLogic();
            if(string.IsNullOrEmpty(viewingcode))
            {
                foreach (var o in logic.GetAllPhotosByFolderViewingCode("home"))
                {
                    var photo = new vwPhotosAndSale()
                    {
                        Id = o.Id,
                        MtDbPhotoId = o.Id,
                        DbName = o.DbName,
                        ViewingCode = o.ViewingCode,
                        LargeImage = o.LargeImage,
                        SmallImage = o.SmallImage,
                        PayPalId = o.PayPalId,
                        Width = o.Width,
                        MtDbFolderId = o.MtDbFolderId,
                        DbPath = o.DbPath,
                        DbShareUrl = o.DbShareUrl,
                        TotalSold = o.TotalSold,
                        TotalSales = o.TotalSales,
                        PricePerPhoto = o.MtDbFolder.PricePerPhoto
                    };
                    model.Add(photo);
                }

            }
            else
            {
                var data = logic.GetAllPhotosAndSalesByViewingCode(viewingcode).OrderBy(o => o.MtDbPhotoId);
                var lastMtDbPhotoId = Guid.Empty;

                foreach (var o in data)
                {
                    if (lastMtDbPhotoId != o.MtDbPhotoId)
                    {
                        var photo = new vwPhotosAndSale()
                        {
                            Id = o.Id,
                            MtDbPhotoId = o.MtDbPhotoId,
                            DbName = o.DbName,
                            ViewingCode = o.ViewingCode,
                            LargeImage = o.LargeImage,
                            SmallImage = o.SmallImage,
                            PayPalId = o.PayPalId,
                            Width = o.Width,
                            MtDbFolderId = o.MtDbFolderId,
                            DbPath = o.DbPath,
                            DbShareUrl = o.DbShareUrl,
                            TotalSold = o.TotalSold,
                            TotalSales = o.TotalSales,
                            PricePerPhoto = o.PricePerPhoto
                        };
                        model.Add(photo);
                        lastMtDbPhotoId = o.MtDbPhotoId;
                    }
                }

                var boughtPhoto = data.FirstOrDefault(o => o.SaleCode == viewingcode);

                if (boughtPhoto != null)
                {
                    var modelBoughtPhoto = model.FirstOrDefault(o => o.MtDbPhotoId == boughtPhoto.MtDbPhotoId);
                    if (modelBoughtPhoto != null)
                    {
                        modelBoughtPhoto.SaleCode = boughtPhoto.SaleCode;
                    }
                }

            }


            return View(model);
        }
    }
}