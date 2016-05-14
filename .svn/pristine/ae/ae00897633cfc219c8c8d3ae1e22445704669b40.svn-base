using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProtoTypeController : Controller
    {
        // GET: ProtoType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Folders()
        {
            var model = new List<DbFolder>();
            model.Add(new DbFolder()
            {
                DbName = "Chagford 2015",
                IsIncluded = true,
                ViewToken = "ABC123",
                Actions = "Re Sync",
                TotalSales = 230,
                TotalSold = 23
            });

            model.Add(new DbFolder()
            {
                DbName = "Stoke School 2015",
                IsIncluded = false,
                ViewToken = "",
                Actions = "Add",
                TotalSales = 0,
                TotalSold = 0
            });


            model.Add(new DbFolder()
            {
                DbName = "Bill and Bens Wedding",
                IsIncluded = true,
                ViewToken = "EFG567",
                Actions = "",
                TotalSales = 0,
                TotalSold = 0
            });

            return View(model);
        }

        public ActionResult Settings()
        {
            var model = new DbFolder()
            {
                DbName = "Chagford 2015",
                IsIncluded = true,
                ViewToken = "ABC123",
                Actions = "Re Sync",
                TotalSales = 230,
                TotalSold = 23
            };

            return View(model);
        }

        public ActionResult Photos()
        {
            var model = new List<DbPhoto>();
            model.Add(new DbPhoto()
            {
                DbName = "randomPic1",
                IsIncluded = true,
                ViewToken = "ABC123",
                DbShareUrl = "http://db.com/1234",
                TotalSales = 10,
                TotalSold = 1
            });
            model.Add(new DbPhoto()
            {
                DbName = "randomPic2",
                IsIncluded = true,
                ViewToken = "ABC123",
                DbShareUrl = "http://db.com/1234",
                TotalSales = 10,
                TotalSold = 1
            });
            model.Add(new DbPhoto()
            {
                DbName = "randomPic3",
                IsIncluded = true,
                ViewToken = "ABC123",
                DbShareUrl = "http://db.com/1234",
                TotalSales = 10,
                TotalSold = 1
            });
            model.Add(new DbPhoto()
            {
                DbName = "randomPic4",
                IsIncluded = true,
                ViewToken = "EFG567",
                DbShareUrl = "http://db.com/1234",
                TotalSales = 10,
                TotalSold = 1
            });
            model.Add(new DbPhoto()
            {
                DbName = "randomPic5",
                IsIncluded = true,
                ViewToken = "EFG567",
                DbShareUrl = "http://db.com/1234",
                TotalSales = 10,
                TotalSold = 1
            });
            model.Add(new DbPhoto()
            {
                DbName = "randomPic6",
                IsIncluded = true,
                ViewToken = "EFG567",
                DbShareUrl = "http://db.com/1234",
                TotalSales = 10,
                TotalSold = 1
            });
            return View(model);
        }

        public ActionResult Purchases()
        {
            var model = new List<DbPhotoSale>();
            model.Add(new DbPhotoSale()
            {
                BuyersEmail = "brendan.caylor@gmail.com",
                PricePaid = 10,
                SaleToken = "EFG567"
            });


            return View(model);
        }
    }
}