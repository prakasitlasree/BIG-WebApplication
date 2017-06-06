using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DataModel;
using System.Web;

namespace DataModel
{
   public class AdminContentModel
    {
        public List<BIG_Banners> BannerList { get; set; }
        public BIG_Policy Policy { get; set; }
        public BIG_About AboutUS { get; set; }
        public List<ChooseUsModel> ChooseUsList { get; set; }
        public List<ExecutiveInfoModel> ExeInfoList { get; set; }

        public List<BIG_Personnel> PersonnelList { get; set; }

        public List<BIG_Gallery> GalleryList { get; set; }

        //Edit Model Part
        public BIG_Banners SingleBanner { get; set; }

        public HttpPostedFileBase BannerFile {get;set;}

        public BIG_Personnel SinglePerson { get; set; }

        public HttpPostedFileBase PersonFile { get; set; }

        public BIG_Gallery SingleGallery { get; set; }

        public HttpPostedFileBase GalleryFile { get; set; }

        public List<BIG_Services> ServicesList { get; set; }

        public List<BIG_Customer> CustomerList { get; set; }

        public BIG_Customer SingleCustomer { get; set; }

        public HttpPostedFileBase CustomerFile { get; set; }

        public BIG_AdminAccount Email { get; set; }

    }
}
