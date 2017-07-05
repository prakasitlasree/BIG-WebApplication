using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace BIG_Interguard.Controllers
{
    public class HomeController : Controller
    {
        EntityServices entityService = new EntityServices();
        EntityAdminServices entityAdminService = new EntityAdminServices();
        public ActionResult Index()
        {

            IndexContentModel indexContent = new IndexContentModel()
            {
                BannerList = entityService.SelectBanners(),
                Policy = entityService.SelectPolicy(),
                AboutUS = entityService.SelectAbout(),
                ChooseUsList = entityService.SelectChooseUs(),
                ExeInfoList = entityService.SelectExeInfo(),
                PersonnelList = entityService.SelectPersonnel(),
                GalleryList = entityService.SelectGallery(),
                ServicesList = entityService.SelectServices(),
                CustomerList = entityService.SelectCustomer()


            };

            return View(indexContent);
        }

        public ActionResult Admin()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Account = Session["Admin"].ToString();
                return View();

            }

        }

        public ActionResult Login(LoginModel account)
        {
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Admin");
            }

            LoginModel resultLogin = new LoginModel();
            if (Session["Admin"] == null && account.ADMIN_ID != null && account.PASSWORD != null)
            {

                if (entityService.SelectAdminAccount(account.ADMIN_ID, account.PASSWORD))
                {
                    Session["Admin"] = account.ADMIN_ID;
                    return RedirectToAction("Admin");
                }
                else
                {
                    resultLogin.STATUS = "ID or Password incorrect.";
                    return View(resultLogin);

                }
            }
            else
            {
                return View(resultLogin);
            }



        }
        public ActionResult LogOut()
        {

            Session["Admin"] = null;

            return RedirectToAction("Admin");

        }

        public JsonResult SendEmailCustomer(EmailFormModel dataInput)
        {
            ResultTransactionModel result = new ResultTransactionModel();
            try
            {
                SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
                mailServer.EnableSsl = true;
                mailServer.UseDefaultCredentials = false;
                mailServer.Credentials = new System.Net.NetworkCredential("biginterguard.website@gmail.com", "bigadmin");
                var mailTo = entityAdminService.SelectEmail();

                string from = "biginterguard.website@gmail.com";

                string to = mailTo.ACCOUNT;

                MailMessage msg = new MailMessage(from, to);
                string[] monthTH = new string[] { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
                int day = DateTime.Now.Day;
                int monthIndex = DateTime.Now.Month - 1;
                int year = DateTime.Now.Year + 543;

                msg.Subject = "ขอใบเสนอราคา ณ วันที่ " + day + " " + monthTH[monthIndex] + " " + year;

                StringBuilder detail = new StringBuilder();
                detail.AppendLine("ชื่อ : " + dataInput.NAME);
                detail.AppendLine("ตำแหน่ง : " + dataInput.POSITION);
                detail.AppendLine("บริษัท : " + dataInput.COMPANY);
                detail.AppendLine("เบอร์ติดต่อ : " + dataInput.NUMBER);
                detail.AppendLine("อีเมล์ : " + dataInput.EMAIL);
                detail.AppendLine("รายละเอียด : " + dataInput.DESC);
                detail.AppendLine("");
                detail.AppendLine("ส่งจาก : " + GetIPAddress()); 

                msg.Body = detail.ToString();

                mailServer.Send(msg);

                result.STATUS = true;
                result.MESSAGE = "ส่งคำร้องเรียบร้อยแล้ว ขอบคุณที่ใช้บริการ";

            }
            catch (Exception ex)
            {
                result.STATUS = false;
                result.MESSAGE = "ไม่สามารถส่งคำร้องได้";
            }


            return Json(result);
        }

        private string GetIPAddress()
        {
            var result = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    result = Convert.ToString(IP);
                }
            }
            return result;

        }


    }
}