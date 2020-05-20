using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<Product> ListRelatedProducts(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }


        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            //var model = db.Products.Where(x => x.CategoryID == categoryID)
            //.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize)
            //.Take(pageSize).ToList();
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            
            //Join 2 bảng Product và ProductCategory
            var model = from a in db.Products
                        join b in db.ProductCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == categoryID
                        select new ProductViewModel()
                        {
                            CateMetaTitle = b.MetaTitle,
                            CateName = b.Name,
                            CreatedDate = a.CreateDate,
                            ID = a.ID,
                            Images = a.Image,
                            MetaTitle = a.MetaTitle,
                            Price = a.Price
                        };
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model.ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
    }
}
