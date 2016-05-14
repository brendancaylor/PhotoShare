using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using PhotoShare.DataLayer.Models;

namespace PhotoShare.DataLayer
{
    public class GeneralDa
    {
        public List<Models.MtDbFolder> GetAllMtDbFolder()
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                var qry = db.MtDbFolders
                    .Include(x => x.MtDbPhotoes.Select(s => s.MtDbPhotoSales));
                return qry.ToList();
            }
        }

        public Models.MtDbFolder GetMtDbFolder(Guid id)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                var qry = db.MtDbFolders.Where(o => o.Id == id)
                    .Include(x => x.MtDbPhotoes.Select(s => s.MtDbPhotoSales));
                return qry.FirstOrDefault();
            }
        }

        public Models.MtDbPhoto GetMtDbPhotoInclude(Guid id)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                var qry = db.MtDbPhotoes.Where(o => o.Id == id)
                    .Include(x => x.MtDbFolder)
                    .Include(x => x.MtDbPhotoSales);
                var result = qry.FirstOrDefault();
                return result;
            }
        }


        public Models.MtDbPhoto GetMtDbPhotoIncludeByPayPalId(string payPalId)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                var qry = db.MtDbPhotoes.Where(o => o.PayPalId == payPalId)
                    .Include(x => x.MtDbFolder)
                    .Include(x => x.MtDbPhotoSales);
                var result = qry.FirstOrDefault();
                return result;
            }
        }

        public List<Models.MtDbPhoto> GetAllPhotosByFolderId(Guid id)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                var qry = db.MtDbPhotoes.Where(o => o.MtDbFolderId == id)
                    .Include(x => x.MtDbFolder);
                var result = qry.ToList();
                return result;
            }
        }

        public List<Models.MtDbPhoto> GetAllPhotosByFolderViewingCode(string viewingCode)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                var qry = db.MtDbPhotoes.Where(o => o.MtDbFolder.ViewingCode == viewingCode)
                    .Include(x => x.MtDbFolder)
                    .Include(x => x.MtDbPhotoSales);
                var result = qry.ToList();
                return result;
            }
        }

        public List<Models.vwPhotosAndSale> GetAllPhotosAndSalesByViewingCode(string viewingCode)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                var qry = db.vwPhotosAndSales.Where(o => o.ViewingCode == viewingCode || o.SaleCode == viewingCode);
                var result = qry.ToList();
                return result;
            }
        }

        public void UpdateMtDbFolderMin(MtDbFolder objc)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                db.MtDbFolders.Attach(objc);
                db.Entry(objc).Property(x => x.ViewingCode).IsModified = true;
                db.Entry(objc).Property(x => x.PricePerPhoto).IsModified = true;
                db.Entry(objc).Property(x => x.SetsOf).IsModified = true;
                db.SaveChanges();
            }
        }

        public void UpdateMtDbPhotoImages(MtDbPhoto objc)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                db.MtDbPhotoes.Attach(objc);
                db.Entry(objc).Property(x => x.SmallImage).IsModified = true;
                db.Entry(objc).Property(x => x.LargeImage).IsModified = true;
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    var test = "";
                    throw;
                }
                
            }
        }

        public void DeleteMtDbFolder(Guid id)
        {
            using (MtPhotosContext _context = new MtPhotosContext())
            {
                var rec = _context.MtDbFolders.FirstOrDefault(o => o.Id == id);
                
                /*
                foreach (var mtDbPhotoe in rec.MtDbPhotoes)
                {
                    mtDbPhotoe.MtDbPhotoSales.Clear();
                    _context.MtDbPhotoSales.RemoveRange(
                        _context.MtDbPhotoSales.Where(o => o.MtDbPhotoId == mtDbPhotoe.Id));
                }
                 */
                _context.MtDbPhotoes.RemoveRange(_context.MtDbPhotoes.Where(o => o.MtDbFolderId == id));
                _context.MtDbFolders.Remove(rec);
                _context.SaveChanges();
            }
        }

        public void UpdateMtDbPhotoMin(MtDbPhoto objc)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                db.MtDbPhotoes.Attach(objc);
                db.Entry(objc).Property(x => x.ViewingCode).IsModified = true;
                db.SaveChanges();
            }
        }

        public void UpdateMtDbPhotoTotals(MtDbPhoto objc)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                db.MtDbPhotoes.Attach(objc);
                db.Entry(objc).Property(x => x.TotalSales).IsModified = true;
                db.Entry(objc).Property(x => x.TotalSold).IsModified = true;
                db.SaveChanges();
            }
        }

        public void UpdateMtDbFolderTotals(MtDbFolder objc)
        {
            using (MtPhotosContext db = new MtPhotosContext())
            {
                db.MtDbFolders.Attach(objc);
                db.Entry(objc).Property(x => x.TotalSales).IsModified = true;
                db.Entry(objc).Property(x => x.TotalSold).IsModified = true;
                db.SaveChanges();
            }
        }
    }
}
