using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DataModel.DataModel;
using System.Web.Script.Serialization;
using System.IO;

namespace BIG_Interguard.Controllers
{
    public class AdminPanelController : Controller
    {
        EntityAdminServices entityAdminService = new EntityAdminServices();
        public ActionResult BannerConfig()
        {
            if(Session["Admin"] == null)
            {
               return RedirectToAction("Login", "Home");
            }

            AdminContentModel adminContent = new AdminContentModel()
            {
                 BannerList = entityAdminService.SelectBanners()

            };
            return View(adminContent);
        }
        public ActionResult PersonalConfig()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            AdminContentModel adminContent = new AdminContentModel()
            {
                PersonnelList = entityAdminService.SelectPersonnel()

            };
            return View(adminContent);
        }

        public ActionResult GalleryConfig()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            AdminContentModel adminContent = new AdminContentModel()
            {
                GalleryList = entityAdminService.SelectGallery()

            };
            return View(adminContent);
        }

        public ActionResult TextConfig()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            AdminContentModel adminContent = new AdminContentModel()
            {
                Policy = entityAdminService.SelectPolicy(),
                ExeInfoList = entityAdminService.SelectExeInfo(),
                ServicesList = entityAdminService.SelectServices(),
                AboutUS = entityAdminService.SelectAbout()

            };
            return View(adminContent);
        }

        public ActionResult CustomerConfig()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            AdminContentModel adminContent = new AdminContentModel()
            {
                CustomerList = entityAdminService.SelectCustomer()

            };
            return View(adminContent);
        }

        public ActionResult EmailConfig()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            AdminContentModel adminContent = new AdminContentModel()
            {
                Email = entityAdminService.SelectEmail()

            };
            return View(adminContent);

        }

        public JsonResult DeleteBanner(AdminContentModel dataInput)
        {
         
            var resultTrans = entityAdminService.DeleteBanner(dataInput.SingleBanner);


            return Json(resultTrans,JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddBanner(AdminContentModel dataInput)
        {

            var fileBanner = dataInput.BannerFile;
            var fileName = Path.GetFileName(fileBanner.FileName);
            var files = Directory.GetFiles(Server.MapPath("~/Content/Banner"));

            if (!Directory.Exists(Server.MapPath("~/Content/Banner")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Content/Banner"));
            }
            var exist = (from p in files
                         where p.Contains(fileName)
                         select p).ToList();
            if(exist.Count > 0)
            {
                fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + exist.Count + exist.Count + Path.GetExtension(fileName);
            }
            if (fileBanner != null && fileBanner.ContentLength > 0)
            {
                
                var path = Path.Combine(Server.MapPath("~/Content/Banner"), fileName);
                fileBanner.SaveAs(path);
            }
            dataInput.SingleBanner.BANNER_IMG = "/Content/Banner/"+ fileName;

            var resultTrans = entityAdminService.AddBanner(dataInput.SingleBanner);

            return resultTrans.STATUS == true ? RedirectToAction("BannerConfig", "AdminPanel"):null;
        }
        public ActionResult EditBanner(AdminContentModel dataInput)
        {

            var fileBanner = dataInput.BannerFile;
        


            if (fileBanner != null && fileBanner.ContentLength > 0)
            {
                var fileName = Path.GetFileName(fileBanner.FileName);
                var listBanner = entityAdminService.SelectBanners();
                var exist = (from p in listBanner
                             where p.BANNER_IMG.Contains(fileName)
                             select p).ToList();
                var Banner = listBanner.Where(x => x.ID == dataInput.SingleBanner.ID).FirstOrDefault();
                try
                {
                    if(exist.Count == 0)
                    {
                        System.IO.File.Delete(Server.MapPath("~" + Banner.BANNER_IMG));
                    }
                
                }
                catch { }
                var path = Path.Combine(Server.MapPath("~/Content/Banner"), fileName);
                dataInput.SingleBanner.BANNER_IMG = "/Content/Banner/" + fileName;
                fileBanner.SaveAs(path);
           
            }
            var resultTrans = entityAdminService.UpdateBanner(dataInput.SingleBanner);

            return resultTrans.STATUS == true ? RedirectToAction("BannerConfig", "AdminPanel") : null;
        }

        public ActionResult AddPerson(AdminContentModel dataInput)
        {

            var filePerson = dataInput.PersonFile;
            var fileName = Path.GetFileName(filePerson.FileName);
            var files = Directory.GetFiles(Server.MapPath("~/Content/Personnel"));
            if (!Directory.Exists(Server.MapPath("~/Content/Personnel")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Content/Personnel"));
            }
            var exist = (from p in files
                         where p.Contains(fileName)
                         select p).ToList();
            if (exist.Count > 0)
            {

                fileName = Path.GetFileNameWithoutExtension(fileName) + "_"+ exist.Count + exist.Count+Path.GetExtension(fileName);
            }
            if (filePerson != null && filePerson.ContentLength > 0)
            {

                var path = Path.Combine(Server.MapPath("~/Content/Personnel"), fileName);
                filePerson.SaveAs(path);
            }
            dataInput.SinglePerson.PERSONNEL_IMG = "/Content/Personnel/" + fileName;

            var resultTrans = entityAdminService.AddPerson(dataInput.SinglePerson);

            return resultTrans.STATUS == true ? RedirectToAction("PersonalConfig", "AdminPanel") : null;
        }
        public ActionResult EditPerson(AdminContentModel dataInput)
        {

            var filePerson = dataInput.PersonFile;



            if (filePerson != null && filePerson.ContentLength > 0)
            {
                var fileName = Path.GetFileName(filePerson.FileName);
                var listPersons = entityAdminService.SelectPersonnel();
                var exist = (from p in listPersons
                             where p.PERSONNEL_IMG.Contains(fileName)
                             select p).ToList();
                var person = listPersons.Where(x => x.ID == dataInput.SinglePerson.ID).FirstOrDefault();
                try
                {
                    if(exist.Count == 0)
                    {
                        System.IO.File.Delete(Server.MapPath("~" + person.PERSONNEL_IMG));
                    }
                  
                }
                catch { }
                var path = Path.Combine(Server.MapPath("~/Content/Personnel"), fileName);
                dataInput.SinglePerson.PERSONNEL_IMG = "/Content/Personnel/" + fileName;
                filePerson.SaveAs(path);

            }



            var resultTrans = entityAdminService.UpdatePerson(dataInput.SinglePerson);

            return resultTrans.STATUS == true ? RedirectToAction("PersonalConfig", "AdminPanel") : null;
        }
        public JsonResult DeletePerson(AdminContentModel dataInput)
        {

            var resultTrans = entityAdminService.DeletePerson(dataInput.SinglePerson);


            return Json(resultTrans, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddGallery(AdminContentModel dataInput)
        {

            var fileGallery = dataInput.GalleryFile;
            var fileName = Path.GetFileName(fileGallery.FileName);
            var files = Directory.GetFiles(Server.MapPath("~/Content/Gallery"));
            if (!Directory.Exists(Server.MapPath("~/Content/Gallery")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Content/Gallery"));
            }
            var exist = (from p in files
                         where p.Contains(fileName)
                         select p).ToList();
            if (exist.Count > 0)
            {
                fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + exist.Count + exist.Count + Path.GetExtension(fileName);
            }
            if (fileGallery != null && fileGallery.ContentLength > 0)
            {

                var path = Path.Combine(Server.MapPath("~/Content/Gallery"), fileName);
                fileGallery.SaveAs(path);
            }
            dataInput.SingleGallery.GALLERY_IMG = "/Content/Gallery/" + fileName;

            var resultTrans = entityAdminService.AddGallery(dataInput.SingleGallery);

            return resultTrans.STATUS == true ? RedirectToAction("GalleryConfig", "AdminPanel") : null;
        }
        public ActionResult EditGallery(AdminContentModel dataInput)
        {

            var fileGallery = dataInput.GalleryFile;

            if (fileGallery != null && fileGallery.ContentLength > 0)
            {
                var fileName = Path.GetFileName(fileGallery.FileName);
                var listGallery = entityAdminService.SelectGallery();
                var exist = (from p in listGallery
                             where p.GALLERY_IMG.Contains(fileName)
                             select p).ToList();
                var gallery = listGallery.Where(x => x.ID == dataInput.SingleGallery.ID).FirstOrDefault();
                try
                {
                    if (exist.Count == 0)
                    {
                        System.IO.File.Delete(Server.MapPath("~" + gallery.GALLERY_IMG));
                    }

                }
                catch { }
                var path = Path.Combine(Server.MapPath("~/Content/Gallery"), fileName);
                dataInput.SingleGallery.GALLERY_IMG = "/Content/Gallery/" + fileName;
                fileGallery.SaveAs(path);

            }

            var resultTrans = entityAdminService.UpdateGallery(dataInput.SingleGallery);

            return resultTrans.STATUS == true ? RedirectToAction("GalleryConfig", "AdminPanel") : null;
        }
        public JsonResult DeleteGallery(AdminContentModel dataInput)
        {

            var resultTrans = entityAdminService.DeleteGallery(dataInput.SingleGallery);


            return Json(resultTrans, JsonRequestBehavior.AllowGet);
        }


        public JsonResult PolicyEdit(AdminContentModel dataInput)
        {
            var resultTrans = entityAdminService.PolicyEdit(dataInput.Policy);

            return Json(resultTrans, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AboutEdit(AdminContentModel dataInput)
        {
            var resultTrans = entityAdminService.AboutUsEdit(dataInput.AboutUS);

            return Json(resultTrans, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExeInfoEdit(AdminContentModel dataInput)
        {
            var resultTrans = entityAdminService.ExeInfoEdit(dataInput.AboutUS);

            return Json(resultTrans, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExeInfoDelete(AdminContentModel dataInput)
        {
            var resultTrans = entityAdminService.ExeInfoDelete(dataInput.AboutUS);

            return Json(resultTrans, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExeInfoAdd(AdminContentModel dataInput)
        {
            var resultTrans = entityAdminService.ExeInfoAdd(dataInput.AboutUS);

            return Json(resultTrans, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ServicesEdit(List<BIG_Services> dataInput)
        {
            var resultTrans = entityAdminService.ServicesEdit(dataInput);

            return Json(resultTrans, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddCustomer(AdminContentModel dataInput)
        {

            var fileCustomer = dataInput.CustomerFile;
            var fileName = Path.GetFileName(fileCustomer.FileName);
            var files = Directory.GetFiles(Server.MapPath("~/Content/Customer"));
            if (!Directory.Exists(Server.MapPath("~/Content/Customer")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Content/Customer"));
            }
            var exist = (from p in files
                         where p.Contains(fileName)
                         select p).ToList();
            if (exist.Count > 0)
            {
                fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + exist.Count + exist.Count + Path.GetExtension(fileName);
            }
            if (fileCustomer != null && fileCustomer.ContentLength > 0)
            {

                var path = Path.Combine(Server.MapPath("~/Content/Customer"), fileName);
                fileCustomer.SaveAs(path);
            }
            dataInput.SingleCustomer.CUSTOMER_IMG = "/Content/Customer/" + fileName;

            var resultTrans = entityAdminService.AddCustomer(dataInput.SingleCustomer);

            return resultTrans.STATUS == true ? RedirectToAction("CustomerConfig", "AdminPanel") : null;
        }
        public ActionResult EditCustomer(AdminContentModel dataInput)
        {

            var fileCustomer = dataInput.CustomerFile;

            if (fileCustomer != null && fileCustomer.ContentLength > 0)
            {
                var fileName = Path.GetFileName(fileCustomer.FileName);
                var listCustomer = entityAdminService.SelectCustomer();
                var exist = (from p in listCustomer
                             where p.CUSTOMER_IMG.Contains(fileName)
                             select p).ToList();
                var customer = listCustomer.Where(x => x.ID == dataInput.SingleCustomer.ID).FirstOrDefault();
                try
                {
                    if (exist.Count == 0)
                    {
                        System.IO.File.Delete(Server.MapPath("~" + customer.CUSTOMER_IMG));
                    }

                }
                catch { }
                var path = Path.Combine(Server.MapPath("~/Content/Customer"), fileName);
                dataInput.SingleCustomer.CUSTOMER_IMG = "/Content/Customer/" + fileName;
                fileCustomer.SaveAs(path);

            }

            var resultTrans = entityAdminService.UpdateCustomer(dataInput.SingleCustomer);

            return resultTrans.STATUS == true ? RedirectToAction("CustomerConfig", "AdminPanel") : null;
        }
        public JsonResult DeleteCustomer(AdminContentModel dataInput)
        {

            var resultTrans = entityAdminService.DeleteCustomer(dataInput.SingleCustomer);


            return Json(resultTrans, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEmailEdit(EmailFormModel dataInput)
        {

            var resultTrans = entityAdminService.SaveEmail(dataInput);


            return Json(resultTrans, JsonRequestBehavior.AllowGet);
        }
      


    }
}