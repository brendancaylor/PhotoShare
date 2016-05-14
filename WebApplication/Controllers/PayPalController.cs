using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using PhotoShare.BusinessLogic;
using PhotoShare.Dto;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class PayPalController : Controller
    {

        private GeneralLogic logic = new GeneralLogic();

        // GET: 
        public ActionResult TestIpn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TestIpn(string postedJson)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Deserialize<PayPalCheckoutInfo>(postedJson);
            return View();
        }

        // GET: PayPal
        public ActionResult TestButton()
        {
            return View();
        }

        public async Task<ActionResult> ipntest(string codes)
        {

            PayPalCheckoutInfo model = new PayPalCheckoutInfo();
            model.item_number = codes;
            model.mc_currency = "GBP";
            model.mc_gross = "4.00";
            model.payer_email = "brendan.caylor@gmail.com";
            model.payment_status = "completed";
            await ipn(model);
            return View();
        }


        private string ProcessPhoto(PayPalListenerModel model, string json, string payPalPhotoId, decimal payPalPhotoPrice)
        {

            var photo = new PhotoShare.Dto.MtDbPhoto();

            var mtIpnTestnew = new MtIpnTest()
            {
                Id = Guid.NewGuid(),
                IpnMessage = ""
            };

            try
            {
                photo = logic.GetMtDbPhotoIncludeByPayPalId(payPalPhotoId);
            }
            catch (Exception ex)
            {
                mtIpnTestnew = new MtIpnTest()
                {
                    Id = Guid.NewGuid(),
                    IpnMessage = string.Format("Exception :{0}"
                    , ex.Message)
                };
                logic.AddMtIpnTest(mtIpnTestnew);
            }

            if (photo != null)
            {
                try
                {
                    // id equals a valid photo
                    if (photo.MtDbFolder.PricePerPhoto.HasValue
                        && photo.MtDbFolder.PricePerPhoto.Value == payPalPhotoPrice
                        && model._PayPalCheckoutInfo.mc_currency == "GBP"
                        )
                    {

                        mtIpnTestnew.IpnMessage = string.Format("COMPLETED :-) json {0}", json);
                        logic.AddMtIpnTest(mtIpnTestnew);
                        var code = GeneralLogic.RandomString(8);
                        // they've paid the correct amount
                        var sale = new PhotoShare.Dto.MtDbPhotoSale()
                        {
                            Id = Guid.NewGuid(),
                            BuyersEmail = model._PayPalCheckoutInfo.payer_email,
                            DatePaid = DateTime.Now,
                            IpnMessage = json,
                            MtDbPhotoId = photo.Id,
                            PricePaid = photo.MtDbFolder.PricePerPhoto.Value,
                            SaleCode = code,
                            Txnid = model._PayPalCheckoutInfo.txn_id
                        };
                        logic.AddSale(sale);

                        new GeneralLogic().UpdateTotalSales(photo.MtDbFolderId);
                        return code;
                    }
                    else
                    {

                        mtIpnTestnew = new MtIpnTest()
                        {
                            Id = Guid.NewGuid(),
                            IpnMessage = string.Format("PricePerPhoto :{0}, mc_gross: {1}, mc_currency: {2}"
                                , photo.MtDbFolder.PricePerPhoto.Value
                                , model._PayPalCheckoutInfo.mc_gross
                                , model._PayPalCheckoutInfo.mc_currency)
                        };

                        logic.AddMtIpnTest(mtIpnTestnew);
                    }
                }
                catch
                    (Exception ex)
                {
                    mtIpnTestnew = new MtIpnTest()
                    {
                        Id = Guid.NewGuid(),
                        IpnMessage = string.Format("Exception :{0}"
                            , ex.Message)
                    };
                    logic.AddMtIpnTest(mtIpnTestnew);
                }
            }

            else
            {
                mtIpnTestnew = new MtIpnTest()
                {
                    Id = Guid.NewGuid(),
                    IpnMessage = string.Format("photo is null item_number: {0}",
                    model._PayPalCheckoutInfo.item_number)
                };
                logic.AddMtIpnTest(mtIpnTestnew);
            }
            return null;
        }

        private async Task sendEmail(string emailAddress, string emailBody, string emailSubject)
        {
            EmailService emailService = new EmailService();
            var identityMessage = new IdentityMessage();
            identityMessage.Destination = emailAddress;
            identityMessage.Body = emailBody;
            identityMessage.Subject = emailSubject;
            await emailService.SendAsync(identityMessage);
        }

        public async Task<ActionResult> ipn(PayPalCheckoutInfo payPalCheckoutInfo)
        {
            PayPalListenerModel model = new PayPalListenerModel();
            model._PayPalCheckoutInfo = payPalCheckoutInfo;
            byte[] parameters = Request.BinaryRead(Request.ContentLength);

            string data = Encoding.UTF8.GetString(parameters);
            
            if (parameters != null)
            {
                //if Debug 
                //model.Status = "VERIFIED";
                //end if Debug 
                
                model.GetStatus(parameters);
            }

            var mtIpnTestnew = new MtIpnTest()
            {
                Id = Guid.NewGuid(),
                IpnMessage = string.Format("IPN STARTED. parameters:{0} ", data)
            };

            logic.AddMtIpnTest(mtIpnTestnew);

            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(model._PayPalCheckoutInfo);

            mtIpnTestnew = new MtIpnTest()
            {
                Id = Guid.NewGuid(),
                IpnMessage = string.Format("PayPalCheckoutInfo. Status:{0}, json:{1} ", model.Status.ToLower(), json)
            };

            logic.AddMtIpnTest(mtIpnTestnew);

            if (model.Status.ToLower() == "verified")
            {
                //check that the payment_status is Completed                 
                if (model._PayPalCheckoutInfo.payment_status.ToLower() == "completed")
                {

                    //check that txn_id has not been previously processed to prevent duplicates                      
                    if (logic.DoesTxnidExist(model._PayPalCheckoutInfo.txn_id))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }

                    //check that receiver_email is your Primary PayPal email
                    // NOT IMPIMENTED !

                    var codesList = new List<string>();
                    var codes = "";
                    var urls = "";

                    var arrCodes = model._PayPalCheckoutInfo.item_number.Split(',');
                    var pricePerPhoto = Convert.ToDecimal(model._PayPalCheckoutInfo.mc_gross)/arrCodes.Length;

                    foreach (var payPalPhotoId in arrCodes)
                    {
                        var salesCode = ProcessPhoto(model, json, payPalPhotoId, pricePerPhoto);

                        if (!string.IsNullOrEmpty(salesCode))
                        {
                            codesList.Add(salesCode);
                            if (codes.Length > 0)
                            {
                                codes += @"
";
                                urls += @"
";
                            }
                            codes += salesCode;
                            urls += string.Format(" http://www.mi-photoshare.com/photoshare/Public/ViewCode?viewingcode={0}", salesCode);
                        }
                        
                    }
                    
                    
                    string pluralText1 = "";
                    string pluralText2 = "this";
                    if (codesList.Count > 1)
                    {
                        pluralText1 = "s";
                        pluralText2 = "these";
                    }

                    if (codesList.Count > 0)
                    {
                        var emailBody = string.Format(@"On behalf of Mi-True Image, thank you for purchasing your photo{0}.
You may download your photo{0} using {1} code{0} :

{2}

or by using {1} link{0} :

{3}

Your purchases will be made available to you through a dropbox account so you can receive your image in high resolution for you to print as you wish.

Best wishes Karen

karenmitrueimage@gmail.com

"
                            , pluralText1, pluralText2, codes, urls);

                        var emailSubject = "Mi-True Image purchase";

                        await sendEmail(model._PayPalCheckoutInfo.payer_email, emailBody, emailSubject);

                        mtIpnTestnew = new MtIpnTest()
                        {
                            Id = Guid.NewGuid(),
                            IpnMessage = string.Format("Email sent to :{0}", model._PayPalCheckoutInfo.payer_email)
                        };
                        logic.AddMtIpnTest(mtIpnTestnew);

                    }

                }
                
                else
                {
                    mtIpnTestnew = new MtIpnTest()
                    {
                        Id = Guid.NewGuid(),
                        IpnMessage = string.Format("SOMETHING ELSE :-( | payment_status {0} | json {1}", model._PayPalCheckoutInfo.payment_status.ToLower(), json)
                    };

                    //log response/ipn data for manual investigation             
                    logic.AddMtIpnTest(mtIpnTestnew);

                    await sendEmail(
                        "brendan.caylor@gmail.com"
                        , string.Format("mi-photoshare problems :-( | {0}", mtIpnTestnew.IpnMessage)
                        , "mi-photoshare problems");
                }

            }
            else
            {
                mtIpnTestnew = new MtIpnTest()
                {
                    Id = Guid.NewGuid(),
                    IpnMessage = string.Format("Not verified! Status {0} | json {1}",model.Status.ToLower(), json)
                };
                logic.AddMtIpnTest(mtIpnTestnew);
                await sendEmail(
                        "brendan.caylor@gmail.com"
                        , string.Format("mi-photoshare problems :-( | {0}", mtIpnTestnew.IpnMessage)
                        , "mi-photoshare problems");
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        } 

    }
}