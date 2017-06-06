
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DataModel;
using System.IO;


namespace DataModel
{
    public class EntityAdminServices
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

        public BIG_AdminAccount SelectEmail()
        {
            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_AdminAccount.Where(w => w.STATUS == "Email") select r).ToList().FirstOrDefault();

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
                    WHY_US = ptx.WHY_US

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
        public List<ExecutiveInfoModel> SelectExeInfo()
        {

            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_About.Where(x=>x.IS_EXECUTIVE_INFO == "Y")
                           select new ExecutiveInfoModel
                           {
                               ID = r.ID,
                                TEXT_INFO = r.EXECUTIVE_INFOMATION,
                                 AUTHOR = r.AUTHOR
                           }
                           ).ToList();
                
             


                return ptx;

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
        public List<BIG_Personnel> SelectPersonnel()
        {

            using (var context = new BannerEntities())
            {
                var ptx = (from r in context.BIG_Personnel select r).ToList();




                return ptx;

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




                return ptx.Count > 0 ? true : false;

            }


        }
        public ResultTransactionModel AddBanner(BIG_Banners dataInput)
        {
            try
            {
                var banners = SelectBanners();
             
                if (banners.Count > 0)
                {
                    int maxId = banners.Max(t => t.ID);
                    dataInput.ID = maxId + 1;
                }
                else
                {
                    dataInput.ID = 1;
                }
             
                using (var context = new BannerEntities())
                {
                    context.BIG_Banners.Add(dataInput);
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE="Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }
        


        }
        public ResultTransactionModel UpdateBanner(BIG_Banners dataInput)
        {
            try
            {
            

                using (var context = new BannerEntities())
                {
                    var update = context.BIG_Banners.Where(x => x.ID == dataInput.ID).FirstOrDefault();
                    if(update != null)
                    {
                        update.BANNER_DESCRIPTION = dataInput.BANNER_DESCRIPTION;
                        update.BANNER_HEADER = dataInput.BANNER_HEADER;
                        update.BANNER_IMG = dataInput.BANNER_IMG == null ? update.BANNER_IMG : dataInput.BANNER_IMG;
                        update.BANNER_SUB_HEADER = dataInput.BANNER_SUB_HEADER;
                        update.IS_ACTIVE = "Y";
                                              
                    }
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }
       
        public ResultTransactionModel DeleteBanner(BIG_Banners dataInput)
        {
            try
            {
                using (var context = new BannerEntities())
                {
                    context.BIG_Banners.Attach(dataInput);
                    context.BIG_Banners.Remove(dataInput);
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }


        public ResultTransactionModel AddPerson(BIG_Personnel dataInput)
        {
            try
            {
                var persons = SelectPersonnel();
                if (persons.Count > 0)
                {
                    int maxId = persons.Max(t => t.ID);
                    dataInput.ID = maxId + 1;
                }
                else
                {
                    dataInput.ID = 1;
                }
                 

                using (var context = new BannerEntities())
                {
                    context.BIG_Personnel.Add(dataInput);
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }

        public ResultTransactionModel UpdatePerson(BIG_Personnel dataInput)
        {
            try
            {


                using (var context = new BannerEntities())
                {
                    var update = context.BIG_Personnel.Where(x => x.ID == dataInput.ID).FirstOrDefault();
                    if (update != null)
                    {
                      
                        update.PERSONNEL_NAME = dataInput.PERSONNEL_NAME;
                        update.PERSONNEL_POSITION = dataInput.PERSONNEL_POSITION;
                        update.PERSONNEL_IMG = dataInput.PERSONNEL_IMG == null ? update.PERSONNEL_IMG : dataInput.PERSONNEL_IMG;
                        update.IS_ACTIVE = dataInput.IS_ACTIVE; ;

                    }
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }

        public ResultTransactionModel DeletePerson(BIG_Personnel dataInput)
        {
            try
            {
                using (var context = new BannerEntities())
                {
                    context.BIG_Personnel.Attach(dataInput);
                    context.BIG_Personnel.Remove(dataInput);
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }


        public ResultTransactionModel AddGallery(BIG_Gallery dataInput)
        {
            try
            {
                var gallery = SelectGallery();
               
                if (gallery.Count > 0)
                {
                    int maxId = gallery.Max(t => t.ID);
                    dataInput.ID = maxId + 1;
                }
                else
                {
                    dataInput.ID = 1;
                }
             

                using (var context = new BannerEntities())
                {
                    context.BIG_Gallery.Add(dataInput);
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }

        public ResultTransactionModel UpdateGallery(BIG_Gallery dataInput)
        {
            try
            {


                using (var context = new BannerEntities())
                {
                    var update = context.BIG_Gallery.Where(x => x.ID == dataInput.ID).FirstOrDefault();
                    if (update != null)
                    {

                        update.IMG_NAME = dataInput.IMG_NAME;
                        update.IMG_DESC = dataInput.IMG_DESC;
                        update.GALLERY_IMG = dataInput.GALLERY_IMG == null ? update.GALLERY_IMG : dataInput.GALLERY_IMG;
                        update.IS_ACTIVE = dataInput.IS_ACTIVE; ;

                    }
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }

        public ResultTransactionModel DeleteGallery(BIG_Gallery dataInput)
        {
            try
            {
                using (var context = new BannerEntities())
                {
                    context.BIG_Gallery.Attach(dataInput);
                    context.BIG_Gallery.Remove(dataInput);
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }

        public ResultTransactionModel PolicyEdit(BIG_Policy dataInput)
        {
            try
            {
                using (var context = new BannerEntities())
                {
                    var update = context.BIG_Policy.FirstOrDefault();
                    if (update != null)
                    {
                        update.POLICY = dataInput.POLICY;
                        update.OBLIGATION = dataInput.OBLIGATION;
                        update.VISION = dataInput.VISION;
                        update.ACTIVITY = dataInput.ACTIVITY; ;

                    }
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }
        }

        public ResultTransactionModel AboutUsEdit(BIG_About dataInput)
        {

            try
            {
                using (var context = new BannerEntities())
                {
                    var update = context.BIG_About.Where(x=>x.IS_EXECUTIVE_INFO == "N").FirstOrDefault();
                    if (update != null)
                    {
                        update.WHO_WE_ARE = dataInput.WHO_WE_ARE;
                        update.WHY_US = dataInput.WHY_US;
   
                    }
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }
        }

        public ResultTransactionModel ExeInfoAdd(BIG_About dataInput)
        {

            try
            {
                var info = SelectExeInfo();
               
                if (info.Count > 0)
                {
                    int maxId = info.Max(t => t.ID);
                    dataInput.ID = maxId + 1;
                }
                else
                {
                    dataInput.ID = 1;
                }
            

                using (var context = new BannerEntities())
                {
                    context.BIG_About.Add(dataInput);
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }
        }
        public ResultTransactionModel ExeInfoEdit(BIG_About dataInput)
        {

            try
            {
                using (var context = new BannerEntities())
                {
                    var update = context.BIG_About.Where(x => x.ID == dataInput.ID).FirstOrDefault();
                    if (update != null)
                    {
                        update.IS_EXECUTIVE_INFO = dataInput.IS_EXECUTIVE_INFO;
                        update.EXECUTIVE_INFOMATION = dataInput.EXECUTIVE_INFOMATION;
                        update.AUTHOR = dataInput.AUTHOR;

                    }
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }
        }

        public ResultTransactionModel ExeInfoDelete(BIG_About dataInput)
        {
            try
            {
                using (var context = new BannerEntities())
                {
                    context.BIG_About.Attach(dataInput);
                    context.BIG_About.Remove(dataInput);
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }
        public ResultTransactionModel ServicesEdit(List<BIG_Services> dataInput)
        {

            try
            {
                using (var context = new BannerEntities())
                {
                    foreach (var item in dataInput)
                    {
                        var update = context.BIG_Services.Where(x => x.ID == item.ID).FirstOrDefault();
                        if (update != null)
                        {
                            update.SERVICES_DESC = item.SERVICES_DESC;
                            update.SERVICES_HEADER = item.SERVICES_HEADER;
                        }
                        context.SaveChanges();
                    }
              

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }
        }



        public ResultTransactionModel AddCustomer(BIG_Customer dataInput)
        {
            try
            {
                var customer = SelectCustomer();

                if (customer.Count > 0)
                {
                    int maxId = customer.Max(t => t.ID);
                    dataInput.ID = maxId + 1;
                }
                else
                {
                    dataInput.ID = 1;
                }


                using (var context = new BannerEntities())
                {
                    context.BIG_Customer.Add(dataInput);
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }

        public ResultTransactionModel UpdateCustomer(BIG_Customer dataInput)
        {
            try
            {


                using (var context = new BannerEntities())
                {
                    var update = context.BIG_Customer.Where(x => x.ID == dataInput.ID).FirstOrDefault();
                    if (update != null)
                    {

                        update.CUSTOMER_NAME = dataInput.CUSTOMER_NAME;
                      
                        update.CUSTOMER_IMG = dataInput.CUSTOMER_IMG == null ? update.CUSTOMER_IMG : dataInput.CUSTOMER_IMG;
                        update.IS_ACTIVE = dataInput.IS_ACTIVE; ;

                    }
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }

        public ResultTransactionModel DeleteCustomer(BIG_Customer dataInput)
        {
            try
            {
                using (var context = new BannerEntities())
                {
                    context.BIG_Customer.Attach(dataInput);
                    context.BIG_Customer.Remove(dataInput);
                    context.SaveChanges();

                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }
        public ResultTransactionModel SaveEmail(EmailFormModel  dataInput)
        {
            try
            {
                using (var context = new BannerEntities())
                {
                    var update = context.BIG_AdminAccount.Where(x => x.STATUS == "Email").FirstOrDefault();
                    if (update != null)
                    {
                        update.ACCOUNT = dataInput.EMAIL;
                    }
                    context.SaveChanges();
 
                    return new ResultTransactionModel() { MESSAGE = "Success", STATUS = true };

                }

            }
            catch (Exception ex)
            {
                return new ResultTransactionModel() { MESSAGE = ex.Message.ToString(), STATUS = false };
            }



        }
    }
}
