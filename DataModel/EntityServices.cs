
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DataModel;


namespace DataModel
{
    public class EntityServices
    {
        public List<BIG_Banners> SelectBanners()
        {
            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_Banners select r).ToList();

                return ptx;

            }


        }
        public BIG_Policy SelectPolicy()
        {
         
            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_Policy.Where(w => w.ID == 1) select r).ToList()[0];
              
                return ptx;

            }


        }

        public BIG_About SelectAbout()
        {

            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_About.Where(w => w.IS_EXECUTIVE_INFO == "N") select r).ToList().FirstOrDefault();

                BIG_About dataItem = new BIG_About()
                {
                    WHO_WE_ARE = ptx.WHO_WE_ARE,

                };

                return dataItem;

            }


        }
        public List<ChooseUsModel> SelectChooseUs()
        {

            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_About.Where(w => w.IS_EXECUTIVE_INFO == "N") select r).ToList().FirstOrDefault();
                string[] list = ptx.WHY_US.Split(',');
                var listChoose = new List<ChooseUsModel>();  
                foreach(var item in list)
                {
                    ChooseUsModel dataItem = new ChooseUsModel()
                    {
                        TEXT = item

                    };
                    listChoose.Add(dataItem);
                }
           

                return listChoose;

            }


        }
        public List<BIG_Services> SelectServices()
        {

            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_Services select r).ToList();

                return ptx;

            }


        }
        public List<ExecutiveInfoModel> SelectExeInfo()
        {

            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_About.Where(w => w.IS_EXECUTIVE_INFO == "Y")
                           select new ExecutiveInfoModel
                           {
                                TEXT_INFO = r.EXECUTIVE_INFOMATION,
                                 AUTHOR = r.AUTHOR
                           }
                           ).ToList();
                
             


                return ptx;

            }


        }
        public List<BIG_Personnel> SelectPersonnel()
        {

            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_Personnel.Where(w => w.IS_ACTIVE == "Y")
                          select r).ToList();




                return ptx;

            }


        }

        public bool SelectAdminAccount(string id, string password)
        {

            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_AdminAccount.Where(w => w.IS_ACTIVE == "Y" && w.ACCOUNT == id && w.PASSWORD == password)
                           select new AccountModel
                           {
                               ACCOUNT_ID  = r.ACCOUNT,
                               PASSWORD = r.PASSWORD,

                           }
                           ).ToList();




                return ptx.Count == 1 ? true : false;

            }


        }
        public List<BIG_Gallery> SelectGallery()
        {

            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_Gallery.Where(w => w.IS_ACTIVE == "Y") select r).ToList();

                return ptx;

            }


        }
        public List<BIG_Customer> SelectCustomer()
        {

            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_Customer.Where(w => w.IS_ACTIVE == "Y")
                           select r).ToList();
                return ptx;

            }


        }



    }
}
