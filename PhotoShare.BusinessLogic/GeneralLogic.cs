using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoShare.Dto;
using PhotoShare.DataLayer;
using PhotoShare.DataLayer.Models;
using MtDbFolder = PhotoShare.Dto.MtDbFolder;

namespace PhotoShare.BusinessLogic
{
    public class GeneralLogic
    {
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ2346789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #region MtIpnTest

        public Dto.MtIpnTest AddMtIpnTest(Dto.MtIpnTest o)
        {
            var bda = new BaseService<DataLayer.Models.MtIpnTest>();
            var dbObject = bda.Add(MapMtIpnTestToDl(o));
            return MapMtIpnTestToDto(dbObject);
        }

        private DataLayer.Models.MtIpnTest MapMtIpnTestToDl(Dto.MtIpnTest o)
        {
            return new DataLayer.Models.MtIpnTest()
            {
                Id = o.Id,
                IpnMessage = o.IpnMessage
            };
        }

        private Dto.MtIpnTest MapMtIpnTestToDto(DataLayer.Models.MtIpnTest o)
        {
            return new Dto.MtIpnTest()
            {
                Id = o.Id,
                IpnMessage = o.IpnMessage
            };
        }

        #endregion

        #region MtDbFolder

        public List<Dto.MtDbFolder> GetAllMtDbFolderIncluded()
        {
            List<Dto.MtDbFolder> result = new List<MtDbFolder>();
            var data = new GeneralDa().GetAllMtDbFolder();
            //data.OrderBy(o => o.IpnMessage);
            foreach (var mtDbFolder in data)
            {
                result.Add(MapMtDbFolderToDto(mtDbFolder, true));
            }
            return result;
        }

        public List<Dto.MtDbFolder> GetAllMtDbFolder()
        {
            List<Dto.MtDbFolder> result = new List<MtDbFolder>();
            var bda = new BaseService<DataLayer.Models.MtDbFolder>();
            var data = bda.GetAll();
            //data.OrderBy(o => o.IpnMessage);
            foreach (var mtDbFolder in data)
            {
                result.Add(MapMtDbFolderToDto(mtDbFolder));
            }
            return result;
        }

        public Dto.MtDbFolder GetMtDbFolder(Guid id)
        {
            var bda = new BaseService<DataLayer.Models.MtDbFolder>();
            return MapMtDbFolderToDto(bda.Get(id));
        }

        public Dto.MtDbFolder GetMtDbFolderIncludes(Guid id)
        {
            var bda = new GeneralDa().GetMtDbFolder(id);
            return MapMtDbFolderToDto(bda, true);
        }

        public Dto.MtDbFolder AddMtDbFolder(Dto.MtDbFolder o)
        {
            var bda = new BaseService<DataLayer.Models.MtDbFolder>();
            var dbObject = bda.Add(MapMtDbFolderToDl(o));
            return MapMtDbFolderToDto(dbObject);
        }

        public Dto.MtDbFolder GetMtDbFolderByPath(string path)
        {
            var bda = new BaseService<DataLayer.Models.MtDbFolder>();
            var dbmodel = bda.Find(o => o.DbPath == path);
            if (dbmodel == null)
            {
                return null;
            }
            return MapMtDbFolderToDto(dbmodel);
        }

        public void UpdateMtDbFolderMin(Dto.MtDbFolder dto)
        {
            var objc = MapMtDbFolderToDl(dto);
            new GeneralDa().UpdateMtDbFolderMin(objc);
        }

        public void UpdateMtDbPhotoMin(Dto.MtDbPhoto dto)
        {
            var objc = MapMtDbPhotoToDl(dto);
            new GeneralDa().UpdateMtDbPhotoMin(objc);
        }

        public void DeleteMtDbFolder(Guid id)
        {
            new GeneralDa().DeleteMtDbFolder(id);
        }

        private DataLayer.Models.MtDbFolder MapMtDbFolderToDl(Dto.MtDbFolder o)
        {
            var mtDbFolder = new DataLayer.Models.MtDbFolder()
            {
                Id = o.Id,
                AppId = o.AppId,
                DbName = o.DbName,
                DbPath = o.DbPath,
                IsIncluded = o.IsIncluded,
                ViewingCode = o.ViewingCode,
                PricePerPhoto = o.PricePerPhoto,
                TotalSold = o.TotalSold,
                TotalSales = o.TotalSales,
                SetsOf = o.SetsOf
            };

            foreach (var mtDbPhotoe in o.MtDbPhotoes)
            {
                var mtDbPhoto = MapMtDbPhotoToDl(mtDbPhotoe);

                mtDbFolder.MtDbPhotoes.Add(mtDbPhoto);

                foreach (var mtDbPhotoSale in mtDbPhotoe.MtDbPhotoSales)
                {
                    mtDbPhoto.MtDbPhotoSales.Add(MapMtDbPhotoSaleToDl(mtDbPhotoSale));
                }
                
            }
            return mtDbFolder;
        }

        private Dto.MtDbFolder MapMtDbFolderToDto(DataLayer.Models.MtDbFolder o)
        {
            return MapMtDbFolderToDto(o, false);
        }

        private Dto.MtDbFolder MapMtDbFolderToDto(DataLayer.Models.MtDbFolder o, bool useIncludes)
        {
            var dtoFolder = new Dto.MtDbFolder()
            {
                Id = o.Id,
                AppId = o.AppId,
                DbName = o.DbName,
                DbPath = o.DbPath,
                IsIncluded = o.IsIncluded,
                ViewingCode = o.ViewingCode,
                PricePerPhoto = o.PricePerPhoto,
                TotalSold = o.TotalSold,
                TotalSales = o.TotalSales,
                SetsOf = o.SetsOf
            };

            if (useIncludes)
            {
                foreach (var mtDbPhotoe in o.MtDbPhotoes)
                {
                    var mtDbPhoto = MapMtDbPhotoToDto(mtDbPhotoe);

                    dtoFolder.MtDbPhotoes.Add(mtDbPhoto);

                    foreach (var mtDbPhotoSale in mtDbPhotoe.MtDbPhotoSales)
                    {
                        mtDbPhoto.MtDbPhotoSales.Add(MapMtDbPhotoSaleToDto(mtDbPhotoSale));
                    }

                }
            }
            return dtoFolder;
        }

        #endregion

        #region MtDbPhoto

        public List<Dto.MtDbPhoto> GetAllMtDbPhoto()
        {
            List<Dto.MtDbPhoto> result = new List<Dto.MtDbPhoto>();
            var bda = new BaseService<DataLayer.Models.MtDbPhoto>();
            var data = bda.GetAll();
            foreach (var mtDbPhoto in data.OrderBy(o => o.DbName))
            {
                result.Add(MapMtDbPhotoToDto(mtDbPhoto));
            }
            return result;
        }

        public List<Dto.MtDbPhoto> GetAllMtDbPhotoByFolder(Guid id)
        {
            List<Dto.MtDbPhoto> result = new List<Dto.MtDbPhoto>();

            var data = new GeneralDa().GetAllPhotosByFolderId(id);
            foreach (var mtDbPhoto in data.OrderBy(o => o.DbName))
            {
                var o = MapMtDbPhotoToDto(mtDbPhoto);
                o.MtDbFolder = MapMtDbFolderToDto(mtDbPhoto.MtDbFolder);
                result.Add(o);
            }
            return result;
        }

        public List<Dto.MtDbPhoto> GetAllPhotosByFolderViewingCode(string viewingCode)
        {
            List<Dto.MtDbPhoto> result = new List<Dto.MtDbPhoto>();
            var da = new GeneralDa();
            var data = da.GetAllPhotosByFolderViewingCode(viewingCode);
            foreach (var mtDbPhoto in data.OrderBy(o => o.DbName))
            {
                var o = MapMtDbPhotoToDto(mtDbPhoto);
                o.MtDbFolder = MapMtDbFolderToDto(mtDbPhoto.MtDbFolder);
                result.Add(o);
            }
            return result;
        }

        public List<Dto.vwPhotosAndSale> GetAllPhotosAndSalesByViewingCode(string viewingCode)
        {
            List<Dto.vwPhotosAndSale> result = new List<Dto.vwPhotosAndSale>();
            var da = new GeneralDa();
            var data = da.GetAllPhotosAndSalesByViewingCode(viewingCode);
            foreach (var mtDbPhoto in data.OrderBy(o => o.DbName))
            {
                result.Add(MapVwPhotosAndSaleToDto(mtDbPhoto));
            }
            return result;
        }

        

        public Dto.MtDbPhoto AddMtDbPhoto(Dto.MtDbPhoto o)
        {
            var bda = new BaseService<DataLayer.Models.MtDbPhoto>();
            var dbObject = bda.Add(MapMtDbPhotoToDl(o));
            return MapMtDbPhotoToDto(dbObject);
        }

        public void UpdateMtDbPhotoImages(Dto.MtDbPhoto o)
        {
            new GeneralDa().UpdateMtDbPhotoImages(MapMtDbPhotoToDl(o));
        }

        public Dto.MtDbPhoto GetMtDbPhotoByPath(string path)
        {
            var bda = new BaseService<DataLayer.Models.MtDbPhoto>();
            var dbmodel = bda.Find(o => o.DbPath.ToLower() == path.ToLower());
            if (dbmodel == null)
            {
                return null;
            }
            return MapMtDbPhotoToDto(dbmodel);
        }

        public Dto.MtDbPhoto GetMtDbPhoto(Guid id)
        {
            var bda = new BaseService<DataLayer.Models.MtDbPhoto>();
            return MapMtDbPhotoToDto(bda.Get(id));
        }

        public Dto.MtDbPhoto GetMtDbPhotoIncludeByPayPalId(string payPalId)
        {
            var o = new GeneralDa().GetMtDbPhotoIncludeByPayPalId(payPalId);
            var result = MapMtDbPhotoToDto(o);
            result.MtDbFolder = MapMtDbFolderToDto(o.MtDbFolder);
            return result;
        }

        public Dto.MtDbPhoto GetMtDbPhotoInclude(Guid id)
        {
            var dbmodel = new GeneralDa().GetMtDbPhotoInclude(id);
            return MapMtDbPhotoToDto(dbmodel);
        }

        private DataLayer.Models.MtDbPhoto MapMtDbPhotoToDl(Dto.MtDbPhoto o)
        {
            return new DataLayer.Models.MtDbPhoto()
            {
                Id = o.Id,
                MtDbFolderId = o.MtDbFolderId,
                DbName = o.DbName,
                DbPath = o.DbPath,
                DbShareUrl = o.DbShareUrl,
                ViewingCode = o.ViewingCode,
                TotalSold = o.TotalSold,
                TotalSales = o.TotalSales,
                LargeImage = o.LargeImage,
                SmallImage = o.SmallImage,
                PayPalId = o.PayPalId,
                Width = o.Width
            };
        }

        private Dto.MtDbPhoto MapMtDbPhotoToDto(DataLayer.Models.MtDbPhoto o)
        {
            return new Dto.MtDbPhoto()
            {
                Id = o.Id,
                MtDbFolderId = o.MtDbFolderId,
                DbName = o.DbName,
                DbPath = o.DbPath,
                DbShareUrl = o.DbShareUrl,
                ViewingCode = o.ViewingCode,
                TotalSold = o.TotalSold,
                TotalSales = o.TotalSales,
                LargeImage = o.LargeImage,
                SmallImage = o.SmallImage,
                PayPalId = o.PayPalId,
                Width = o.Width
            };
        }

        private Dto.vwPhotosAndSale MapVwPhotosAndSaleToDto(DataLayer.Models.vwPhotosAndSale o)
        {
            return new Dto.vwPhotosAndSale()
            {
                Id = o.Id.Value,
                MtDbFolderId = o.MtDbFolderId,
                PricePerPhoto = o.PricePerPhoto,
                DbName = o.DbName,
                DbPath = o.DbPath,
                DbShareUrl = o.DbShareUrl,
                ViewingCode = o.ViewingCode,
                TotalSold = o.TotalSold,
                TotalSales = o.TotalSales,
                LargeImage = o.LargeImage,
                SmallImage = o.SmallImage,
                PayPalId = o.PayPalId,
                Width = o.Width,
                MtDbPhotoId = o.MtDbPhotoId,
                SaleCode = o.SaleCode
            };
        }

        #endregion

        #region MtDbPhotoSales

        public List<Dto.MtDbPhotoSale> GetAllMtDbPhotoSaleByPhoto(Guid id)
        {
            List<Dto.MtDbPhotoSale> result = new List<Dto.MtDbPhotoSale>();
            var bda = new BaseService<DataLayer.Models.MtDbPhotoSale>();
            var data = bda.FindAll(o => o.MtDbPhotoId == id);
            foreach (var mtDbPhotoSale in data.OrderBy(o => o.SaleCode))
            {
                result.Add(MapMtDbPhotoSaleToDto(mtDbPhotoSale));
            }
            return result;
        }

        public void AddSale(Dto.MtDbPhotoSale sale)
        {
            var newSale = new DataLayer.Models.MtDbPhotoSale()
            {
                Id = sale.Id,
                BuyersEmail = sale.BuyersEmail,
                DatePaid = sale.DatePaid,
                IpnMessage = sale.IpnMessage,
                MtDbPhotoId = sale.MtDbPhotoId,
                PricePaid = sale.PricePaid,
                SaleCode = sale.SaleCode,
                Txnid = sale.Txnid
            };
            var bda = new BaseService<DataLayer.Models.MtDbPhotoSale>();
            bda.Add(newSale);

            /*var doesSalseExist = bda.Find(o => o.BuyersEmail.ToLower() == sale.BuyersEmail.ToLower()
                && o.MtDbPhotoId == sale.MtDbPhotoId
                );
            if (doesSalseExist == null)
            {
                
            }*/

        }

        public bool DoesTxnidExist(string txnid)
        {
            var bda = new BaseService<DataLayer.Models.MtDbPhotoSale>();
            var doesSalseExist = bda.Find(o => o.Txnid.ToLower() == txnid);
            return doesSalseExist != null;
        }

        private DataLayer.Models.MtDbPhotoSale MapMtDbPhotoSaleToDl(Dto.MtDbPhotoSale o)
        {
            return new DataLayer.Models.MtDbPhotoSale()
            {
                Id = o.Id,
                MtDbPhotoId = o.MtDbPhotoId,
                SaleCode = o.SaleCode,
                PricePaid = o.PricePaid,
                DatePaid = o.DatePaid,
                IpnMessage = o.IpnMessage,
                BuyersEmail = o.BuyersEmail
            };
        }

        private Dto.MtDbPhotoSale MapMtDbPhotoSaleToDto(DataLayer.Models.MtDbPhotoSale o)
        {
            return new Dto.MtDbPhotoSale()
            {
                Id = o.Id,
                MtDbPhotoId = o.MtDbPhotoId,
                SaleCode = o.SaleCode,
                PricePaid = o.PricePaid,
                DatePaid = o.DatePaid,
                IpnMessage = o.IpnMessage,
                BuyersEmail = o.BuyersEmail
            };
        }

        #endregion

        /*
        public List<MtIpnTest> GetAllMtIpnTest()
        {
            var bda = new BaseService<MtIpnTest>();
            var data = bda.GetAll();
            //data.OrderBy(o => o.IpnMessage);
            return data.ToList();
        }

        public MtIpnTest GetMtIpnTest(Guid id)
        {
            var bda = new BaseService<MtIpnTest>();
            return bda.Get(id);
        }

        

        public MtIpnTest UpdateMtIpnTest(MtIpnTest o)
        {
            var bda = new BaseService<MtIpnTest>();
            return bda.Update(o);
        }

         */

        #region totalSales

        public void UpdateTotalSales(Guid folderId)
        {
            var da = new GeneralDa();
            
            

            var folder = GetMtDbFolderIncludes(folderId);

            folder.TotalSales = 0;
            folder.TotalSold = 0;

            foreach (var photo in folder.MtDbPhotoes)
            {
                photo.TotalSales = 0;
                photo.TotalSold = 0;
                foreach (var sale in photo.MtDbPhotoSales)
                {
                    folder.TotalSold++;
                    photo.TotalSold++;

                    folder.TotalSales+= sale.PricePaid;
                    photo.TotalSales += sale.PricePaid;
                }

                var photoDo = MapMtDbPhotoToDl(photo);
                da.UpdateMtDbPhotoTotals(photoDo);
            }

            var folderDo = MapMtDbFolderToDl(folder);
            da.UpdateMtDbFolderTotals(folderDo);

        }

        #endregion

    }
}
