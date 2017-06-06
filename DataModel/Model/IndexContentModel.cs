using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DataModel;

namespace DataModel
{
    public class IndexContentModel
    {
        public List<BIG_Banners> BannerList { get; set; }
        public BIG_Policy Policy { get; set; }

        public BIG_About AboutUS { get; set; }
        public List<ChooseUsModel> ChooseUsList { get; set; }
        public List<ExecutiveInfoModel> ExeInfoList { get; set; }

        public List<BIG_Personnel> PersonnelList { get; set; }

        public List<BIG_Gallery> GalleryList { get; set; }

        public List<BIG_Services> ServicesList { get; set; }

        public List<BIG_Customer> CustomerList { get; set; }
    }
}
