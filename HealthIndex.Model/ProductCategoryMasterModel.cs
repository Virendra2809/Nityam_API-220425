using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class ProductCategoryMasterModel
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }

    }

    public class MenuProductCategory
    {
        public MenuProductCategory()
        {
            this.subCategoryList = new List<MenuProductSubCategory>();
           
        }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
       
        public List<MenuProductSubCategory> subCategoryList { get; set; }
       
    }
    public class MenuProductSubCategory
    {
        public MenuProductSubCategory()
        {
            this.subChildCategoryList = new List<MenuProductSubChildCategory>();
        }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public List<MenuProductSubChildCategory> subChildCategoryList { get; set; }
       
    }
    public class MenuProductSubChildCategory
    {
        public MenuProductSubChildCategory()
            {
            this.ChildCategoryList = new List<MenuProductSubChildschildCategory>();

        }

        public int SubChildCategoryId { get; set; }
        public string SubChildCategoryName { get; set; }
        public bool hasChildcategory { get; set; }
        public List<MenuProductSubChildschildCategory> ChildCategoryList { get; set; }

    }


    public class MenuProductSubChildschildCategory
    {
        public int SubChildCategoryId { get; set; }
        public string SubChildCategoryName { get; set; }
    }

    public class MenuProduct
    {
        public int ProductId { get; set; }
       
    }

}