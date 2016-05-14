using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Dropbox.Api;
using Dropbox.Api.Files;
using PhotoShare.BusinessLogic;
using PhotoShare.Dto;
using WebApplication.Models;
using System.Threading;

namespace WebApplication.Controllers
{
    public class DropboxController : Controller
    {
        private static List<ProgressModel> tasks = new List<ProgressModel>();

        private string dropboxAccessToken = ConfigurationManager.AppSettings["DropboxAccessToken"];
        private string dropboxRootFolder = ConfigurationManager.AppSettings["DropboxRootFolder"];
        private GeneralLogic logic = new GeneralLogic();

        public async Task<ActionResult> Folders()
        {
            List<Folder> model = new List<Folder>();
            using (var dbx = new DropboxClient(dropboxAccessToken))
            {
                var dbxFolders = await dbx.Files.ListFolderAsync(dropboxRootFolder);
                var dbFolders = logic.GetAllMtDbFolder();

                foreach (var dbxFolder in dbxFolders.Entries.Where(i => i.IsFolder))
                {
                    var modelFolder = new Folder()
                    {
                        Id = Guid.Empty,
                        IsIncluded = false,
                        IsOnlyInDb = false,
                        Path = dbxFolder.PathLower,
                        Name = dbxFolder.Name
                    };
                    var dbFolder = dbFolders.FirstOrDefault(o => o.DbPath == dbxFolder.PathLower);
                    if (dbFolder != null)
                    {
                        // exists in dbx and db
                        modelFolder.Id = dbFolder.Id;
                        modelFolder.IsIncluded = true;
                        modelFolder.ViewingCode = dbFolder.ViewingCode;
                        modelFolder.SetsOf = dbFolder.SetsOf;
                        modelFolder.TotalSales = dbFolder.TotalSales.HasValue? dbFolder.TotalSales.Value : 0;
                        modelFolder.TotalSold = dbFolder.TotalSold.HasValue ? dbFolder.TotalSold.Value : 0;
                    }
                    model.Add(modelFolder);
                }

                foreach (var dbFolder in dbFolders)
                {
                    var dbxFolder = dbxFolders.Entries.FirstOrDefault(o => o.PathLower == dbFolder.DbPath);
                    if (dbxFolder == null)
                    {
                        model.Add(new Folder()
                        {
                            Id = dbFolder.Id,
                            IsIncluded = true,
                            IsOnlyInDb = true,
                            Path = dbFolder.DbPath,
                            Name = dbFolder.DbName,
                            ViewingCode = dbFolder.ViewingCode,
                            SetsOf = dbFolder.SetsOf,
                            TotalSales = dbFolder.TotalSales.HasValue ? dbFolder.TotalSales.Value : 0,
                            TotalSold = dbFolder.TotalSold.HasValue ? dbFolder.TotalSold.Value : 0
                        });
                    }
                }
            }

            model = model.OrderBy(o => o.Name).ToList();
            return View(model);
        }

        public ActionResult Photos(Guid id)
        {
            var data = logic.GetAllMtDbPhotoByFolder(id);
            return View(data);
        }

        public ActionResult PhotoLettersTest()
        {
            var model = new List<Letter>();
            for (int i = 1; i <= 12; i++)
            {
                var photoIds = new List<Guid>();

                for (int j = 1; j <= i; j++)
                {
                    //http://localhost:49316/Dropbox/DownloadSmallImage/9b75a5e2-ef79-4950-852d-6cd9e34b5ca7
                    photoIds.Add(new Guid("9b75a5e2-ef79-4950-852d-6cd9e34b5ca7"));
                }
                model.Add(new Letter()
                {
                    PricePerPhoto = 6,
                    ViewingCode = "abc123",
                    PhotoIds = photoIds
                });
            }
            
            return View("PhotoLetters", model);
        }

        public ActionResult PhotoLetters(Guid id)
        {
            var data = logic.GetAllMtDbPhotoByFolder(id);
            data = data.OrderBy(o => o.ViewingCode).ToList();
            var result = new List<Letter>();

            foreach (var mtDbPhoto in data)
            {
                var letter = result.FirstOrDefault(o => o.ViewingCode == mtDbPhoto.ViewingCode);

                if (letter != null)
                {
                    letter.PhotoIds.Add(mtDbPhoto.Id);
                }
                else
                {
                    var r = new Letter()
                    {
                        PricePerPhoto = mtDbPhoto.MtDbFolder.PricePerPhoto.Value,
                        ViewingCode = mtDbPhoto.ViewingCode
                    };
                    r.PhotoIds.Add(mtDbPhoto.Id);
                    result.Add(r);
                }
            }
            return View(result);
        }

        public ActionResult DownloadSmallImage(Guid id)
        {
            var data = new GeneralLogic().GetMtDbPhoto(id);
            return File(data.SmallImage, "image/jpeg");
        }

        public ActionResult DownloadLargeImage(Guid id)
        {
            var data = new GeneralLogic().GetMtDbPhoto(id);
            WebImage image = new WebImage(data.LargeImage);
            image.AddImageWatermark(Server.MapPath("~/Images/watermark.png"),329, 300, "right", "top", 60, 10);
            var result = image.GetBytes("image/jpeg");
            return File(result, "image/jpeg");
        }

        public ActionResult Settings(Guid id)
        {
            var dto = new GeneralLogic().GetMtDbFolder(id);
            Folder model = new Folder()
            {
                Id = dto.Id,
                IsIncluded = dto.IsIncluded,
                Path = dto.DbPath,
                Name = dto.DbName,
                ViewingCode = dto.ViewingCode,
                SetsOf = dto.SetsOf,
                PricePerPhoto = dto.PricePerPhoto.HasValue ? dto.PricePerPhoto.Value : 0,
                TotalSales = dto.TotalSales.HasValue ? dto.TotalSales.Value : 0,
                TotalSold = dto.TotalSold.HasValue ? dto.TotalSold.Value : 0,

            };

            SetOptions(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(Folder model)
        {
            var dto = new GeneralLogic().GetMtDbFolderIncludes(model.Id);
            // viewing price sets

            if (dto.SetsOf != model.SetsOf)
            {
                int iLoop = 0;
                string viewingCode = GeneralLogic.RandomString(5);
                foreach (var mtDbPhotoe in dto.MtDbPhotoes.OrderBy(o => o.DbName))
                {
                    iLoop++;
                    if (model.SetsOf != 0 && iLoop > model.SetsOf)
                    {
                        var newviewingCode = GeneralLogic.RandomString(5);
                        while (newviewingCode == viewingCode)
                        {
                            newviewingCode = GeneralLogic.RandomString(5);
                        }
                        viewingCode = newviewingCode;
                        iLoop = 1;
                    }
                    mtDbPhotoe.ViewingCode = viewingCode;
                    logic.UpdateMtDbPhotoMin(mtDbPhotoe);
                }
            }

            dto.ViewingCode = model.ViewingCode;
            dto.PricePerPhoto = model.PricePerPhoto;
            dto.SetsOf = model.SetsOf;

            logic.UpdateMtDbFolderMin(dto);
            SetOptions(model);
            return View(model);
        }


        public ActionResult Purchases(Guid id)
        {
            var data = logic.GetAllMtDbPhotoSaleByPhoto(id);
            return View(data);
        }

        private void SetOptions(Folder model)
        {
            model.SetOptions.Add(new SelectListItem()
            {
                Text = "Entire folder",
                Value = "0"
            });

            for (int i = 1; i <= 10; i++)
            {
                model.SetOptions.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            var selectedSetOption = model.SetOptions.Where(o => o.Value == model.SetsOf.ToString()).FirstOrDefault();
            if (selectedSetOption != null)
            {
                selectedSetOption.Selected = true;
            }
        }

        public async Task<ActionResult> AddNewPhotos(Guid id)
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(1,0,0,0);

            using (var dbx = new DropboxClient(dropboxAccessToken, 1000, null, httpClient))
            {
                var mtDbFolder = logic.GetMtDbFolder(id);
                var files = await dbx.Files.ListFolderAsync(mtDbFolder.DbPath);

                int iLoop = 0;
                
                string viewingCode = GeneralLogic.RandomString(5);

                foreach (var file in files.Entries.Where(i => i.IsFile).OrderBy(o => o.Name))
                {
                    iLoop++;
                    var mtDbPhoto = logic.GetMtDbPhotoByPath(file.PathLower);

                    if (mtDbPhoto != null)
                    {
                        viewingCode = mtDbPhoto.ViewingCode;
                        if (mtDbFolder.SetsOf != 0 && iLoop > mtDbFolder.SetsOf)
                        {
                            iLoop = 1;
                        }
                    }
                    else
                    {
                        if (mtDbFolder.SetsOf != 0 && iLoop > mtDbFolder.SetsOf)
                        {
                            viewingCode = GeneralLogic.RandomString(5);
                            iLoop = 1;
                        }
                        
                        var link = await dbx.Sharing.CreateSharedLinkAsync(file.PathLower, true);
                        var fileDownload = await dbx.Files.DownloadAsync(file.PathLower);
                        var contentAsStream = await fileDownload.GetContentAsByteArrayAsync();

                        int newWidthLargeImage = 0;
                        int newWidthSmallImage = 0;
                        var largeImage = new Utilities.ImageManipulation().Resize(contentAsStream, 900, out newWidthLargeImage);
                        var smallImage = new Utilities.ImageManipulation().Resize(contentAsStream, 220, out newWidthSmallImage);

                        mtDbPhoto = new MtDbPhoto()
                        {
                            Id = Guid.NewGuid(),
                            MtDbFolderId = mtDbFolder.Id,
                            DbName = file.Name,
                            DbPath = file.PathLower,
                            DbShareUrl = link.Url,
                            ViewingCode = viewingCode,
                            TotalSold = 0,
                            TotalSales = 0,
                            LargeImage = largeImage,
                            SmallImage = smallImage,
                            Width = newWidthSmallImage,
                            PayPalId = GeneralLogic.RandomString(5)
                        };

                        logic.AddMtDbPhoto(mtDbPhoto);
                    }

                }

            }
            return RedirectToAction("Folders", "Dropbox");
        }

        public async Task<ActionResult> StartAddNewPhotos(Guid progressId, Guid folderId)
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(1, 0, 0, 0);
            
            var progressModel = tasks.FirstOrDefault(o => o.Id == progressId);
            if (progressModel == null)
            {
                progressModel = new ProgressModel { Id = progressId, Message = "started again", Percentage = 0 }; 
            }

            using (var dbx = new DropboxClient(dropboxAccessToken, 1000, null, httpClient))
            {
                var mtDbFolder = logic.GetMtDbFolder(folderId);
                var files = await dbx.Files.ListFolderAsync(mtDbFolder.DbPath);

                int iLoop = 0;
                string viewingCode = GeneralLogic.RandomString(5);

                int countCompleted = 0;
                var count = files.Entries.Count;

                progressModel.Message = string.Format("0 of {0} files processed.", count);

                progressModel.Percentage = (countCompleted/count*100);

                foreach (var file in files.Entries.Where(i => i.IsFile).OrderBy(o => o.Name))
                {
                    iLoop++;
                    var mtDbPhoto = logic.GetMtDbPhotoByPath(file.PathLower);

                    if (mtDbPhoto != null)
                    {
                        viewingCode = mtDbPhoto.ViewingCode;
                        if (mtDbFolder.SetsOf != 0 && iLoop > mtDbFolder.SetsOf)
                        {
                            iLoop = 1;
                        }
                    }
                    else
                    {
                        if (mtDbFolder.SetsOf != 0 && iLoop > mtDbFolder.SetsOf)
                        {
                            viewingCode = GeneralLogic.RandomString(5);
                            iLoop = 1;
                        }

                        var link = await dbx.Sharing.CreateSharedLinkAsync(file.PathLower, true);
                        var fileDownload = await dbx.Files.DownloadAsync(file.PathLower);
                        var contentAsStream = await fileDownload.GetContentAsByteArrayAsync();

                        int newWidthLargeImage = 0;
                        int newWidthSmallImage = 0;
                        var largeImage = new Utilities.ImageManipulation().Resize(contentAsStream, 900,
                            out newWidthLargeImage);
                        var smallImage = new Utilities.ImageManipulation().Resize(contentAsStream, 220,
                            out newWidthSmallImage);

                        mtDbPhoto = new MtDbPhoto()
                        {
                            Id = Guid.NewGuid(),
                            MtDbFolderId = mtDbFolder.Id,
                            DbName = file.Name,
                            DbPath = file.PathLower,
                            DbShareUrl = link.Url,
                            ViewingCode = viewingCode,
                            TotalSold = 0,
                            TotalSales = 0,
                            LargeImage = largeImage,
                            SmallImage = smallImage,
                            Width = newWidthSmallImage,
                            PayPalId = GeneralLogic.RandomString(5)
                        };

                        logic.AddMtDbPhoto(mtDbPhoto);

                        countCompleted++;
                        progressModel.Message = string.Format("{0} of {1} files processed.", countCompleted,
                            count);

                        progressModel.Percentage =
                            (int) Math.Ceiling(Convert.ToDecimal(countCompleted)/Convert.ToDecimal(count)*100);

                    }

                    if (countCompleted == count)
                    {
                        tasks.Remove(progressModel);
                    }
                }
            }

            return Json(progressModel);
        }

        public async Task<ActionResult> AddFolder(string path, int sets)
        {

            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(1, 0, 0, 0);

            using (var dbx = new DropboxClient(dropboxAccessToken, 10, null, httpClient))
            {
                var folders = await dbx.Files.ListFolderAsync(dropboxRootFolder);

                foreach (var folder in folders.Entries.Where(i => i.IsFolder && i.PathLower == path))
                {
                    var mtDbFolder = logic.GetMtDbFolderByPath(folder.PathLower);

                    if (mtDbFolder == null)
                    {
                        mtDbFolder = new MtDbFolder()
                        {
                            Id = Guid.NewGuid(),
                            AppId = 1,
                            DbName = folder.Name,
                            DbPath = folder.PathLower,
                            IsIncluded = true,
                            ViewingCode = GeneralLogic.RandomString(5),
                            PricePerPhoto = 10,
                            TotalSold = 0,
                            TotalSales = 0,
                            SetsOf = sets
                        };

                        logic.AddMtDbFolder(mtDbFolder);
                    }

                    var files = await dbx.Files.ListFolderAsync(folder.PathLower);

                    int iLoop = 0;
                    string viewingCode = GeneralLogic.RandomString(5);
                    foreach (var file in files.Entries.Where(i => i.IsFile).OrderBy(o => o.Name))
                    {
                        iLoop++;
                        if (sets != 0 && iLoop > sets)
                        {
                            viewingCode = GeneralLogic.RandomString(5);
                            iLoop = 1;
                        }

                        var mtDbPhoto = logic.GetMtDbPhotoByPath(file.PathLower);
                        
                        if (mtDbPhoto == null)
                        {
                            var link = await dbx.Sharing.CreateSharedLinkAsync(file.PathLower, true);
                            var fileDownload = await dbx.Files.DownloadAsync(file.PathLower);
                            var contentAsStream = await fileDownload.GetContentAsByteArrayAsync();

                            int newWidthLargeImage = 0;
                            int newWidthSmallImage = 0;
                            var largeImage = new Utilities.ImageManipulation().Resize(contentAsStream, 900, out newWidthLargeImage);
                            var smallImage = new Utilities.ImageManipulation().Resize(contentAsStream, 220, out newWidthSmallImage);

                            mtDbPhoto = new MtDbPhoto()
                            {
                                Id = Guid.NewGuid(),
                                MtDbFolderId = mtDbFolder.Id,
                                DbName = file.Name,
                                DbPath = file.PathLower,
                                DbShareUrl = link.Url,
                                ViewingCode = viewingCode,
                                TotalSold = 0,
                                TotalSales = 0,
                                LargeImage = largeImage,
                                SmallImage = smallImage,
                                Width = newWidthSmallImage,
                                PayPalId = GeneralLogic.RandomString(5)
                            };

                            logic.AddMtDbPhoto(mtDbPhoto);
                        }

                    }

                }

            }
            return RedirectToAction("Folders", "Dropbox");
        }

        public ActionResult GetTaskIdAddFolder()
        {
            var progressModel = new ProgressModel { Id = Guid.NewGuid(), Message = "Started", Percentage = 0 };
            tasks.Add(progressModel);
            return Json(progressModel);
        }

        public async Task<ActionResult> StartAddFolder(Guid id, string path, int sets)
        {

            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(1, 0, 0, 0);
            
            var progressModel = tasks.FirstOrDefault(o => o.Id == id);
            if (progressModel == null)
            {
                progressModel = new ProgressModel { Id = id, Message = "completed", Percentage = 0 }; 
            }

            
            using (var dbx = new DropboxClient(dropboxAccessToken, 1000, null, httpClient))
            {
                var folders = await dbx.Files.ListFolderAsync(dropboxRootFolder);

                foreach (var folder in folders.Entries.Where(i => i.IsFolder && i.PathLower == path))
                {
                    var mtDbFolder = logic.GetMtDbFolderByPath(folder.PathLower);

                    if (mtDbFolder == null)
                    {
                        mtDbFolder = new MtDbFolder()
                        {
                            Id = Guid.NewGuid(),
                            AppId = 1,
                            DbName = folder.Name,
                            DbPath = folder.PathLower,
                            IsIncluded = true,
                            ViewingCode = GeneralLogic.RandomString(5),
                            PricePerPhoto = 10,
                            TotalSold = 0,
                            TotalSales = 0,
                            SetsOf = sets
                        };

                        logic.AddMtDbFolder(mtDbFolder);
                    }

                    var files = await dbx.Files.ListFolderAsync(folder.PathLower);
                    int iLoop = 0;
                    string viewingCode = GeneralLogic.RandomString(5);
                    int countCompleted = 0;
                    var count = files.Entries.Count;

                    progressModel.Message = string.Format("0 of {0} files processed.", count);

                    progressModel.Percentage = (countCompleted/count*100);

                    foreach (var file in files.Entries.Where(i => i.IsFile).OrderBy(o => o.Name))
                    {
                        iLoop++;
                        if (sets != 0 && iLoop > sets)
                        {
                            viewingCode = GeneralLogic.RandomString(5);
                            iLoop = 1;
                        }

                        var mtDbPhoto = logic.GetMtDbPhotoByPath(file.PathLower);

                        if (mtDbPhoto == null)
                        {

                            var link = await dbx.Sharing.CreateSharedLinkAsync(file.PathLower, true);
                            var fileDownload = await dbx.Files.DownloadAsync(file.PathLower);
                            var contentAsStream = await fileDownload.GetContentAsByteArrayAsync();

                            int newWidthLargeImage = 0;
                            int newWidthSmallImage = 0;
                            var largeImage = new Utilities.ImageManipulation().Resize(contentAsStream,
                                900,
                                out newWidthLargeImage);
                            var smallImage = new Utilities.ImageManipulation().Resize(contentAsStream,
                                220,
                                out newWidthSmallImage);

                            mtDbPhoto = new MtDbPhoto()
                            {
                                Id = Guid.NewGuid(),
                                MtDbFolderId = mtDbFolder.Id,
                                DbName = file.Name,
                                DbPath = file.PathLower,
                                DbShareUrl = link.Url,
                                ViewingCode = viewingCode,
                                TotalSold = 0,
                                TotalSales = 0,
                                LargeImage = largeImage,
                                SmallImage = smallImage,
                                Width = newWidthSmallImage,
                                PayPalId = GeneralLogic.RandomString(5)
                            };

                            logic.AddMtDbPhoto(mtDbPhoto);
                            countCompleted++;
                            progressModel.Message = string.Format("{0} of {1} files processed.", countCompleted,
                                count);

                            progressModel.Percentage = (int)Math.Ceiling(Convert.ToDecimal(countCompleted) / Convert.ToDecimal(count) * 100);

                        }
                        if (countCompleted == count)
                        {
                            tasks.Remove(progressModel);
                        }
                    }

                }

            }

            return Json(progressModel);

        }

        public ActionResult ProgressAddFolder(Guid id)
        {
            var progress = tasks.FirstOrDefault(o => o.Id == id);
            if (progress == null)
            {
                progress = new ProgressModel { Id = id, Message = "completed" }; 
            }
            return Json(progress);
        }

        public async Task<ActionResult> DeleteFolder(Guid id)
        {
            logic.DeleteMtDbFolder(id);
            return RedirectToAction("Folders", "Dropbox");
        }

        public async Task<ActionResult> UpdatePhotoImages(string path, Guid id)
        {
            using (var dbx = new DropboxClient(dropboxAccessToken))
            {
                var fileDownload = await dbx.Files.DownloadAsync(path);
                var contentAsStream = await fileDownload.GetContentAsByteArrayAsync();

                int newWidthLargeImage = 0;
                int newWidthSmallImage = 0;
                var largeImage = new Utilities.ImageManipulation().Resize(contentAsStream, 900, out newWidthLargeImage);
                var smallImage = new Utilities.ImageManipulation().Resize(contentAsStream, 220, out newWidthSmallImage);

                var mtDbPhoto = new MtDbPhoto()
                {
                    Id = id,
                    DbName = "",
                    DbPath = "",
                    DbShareUrl = "",
                    ViewingCode = "",
                    PayPalId = "",
                    LargeImage = largeImage,
                    SmallImage = smallImage,
                    Width = newWidthSmallImage
                };

                logic.UpdateMtDbPhotoImages(mtDbPhoto);
            }
            return View();
        }
    }
}