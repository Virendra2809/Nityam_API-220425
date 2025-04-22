using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class PostCategoryService: IPostCategoryService
    {

        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public PostCategoryService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public List<PostCategoryModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var postCategoryModelList = new List<PostCategoryModel>();
            var postCategoryListEntity = _agriContext.PostCategories.Where(x => x.CategoryId==x.CategoryId).ToList();
            if (postCategoryListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in postCategoryListEntity)
            {
                var model = new PostCategoryModel();
                // var MonthNameStr = (item.SeasonStartMonth).ToString(MMMM);
                model.CategoryId = item.CategoryId;
                model.CategoryName = item.CategoryName;
                model.Description = item.Description;   //DateTime.ParseExact(MonthNameStr, "MMMM", CultureInfo.CurrentCulture).Month;
                postCategoryModelList.Add(model);
            }
            return postCategoryModelList;
        }

        public PostCategoryModel GetById(long CategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var postcategoryEntity = _agriContext.PostCategories.FirstOrDefault(x => x.CategoryId == CategoryId);
            if (postcategoryEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new PostCategoryModel
            {
                CategoryId = postcategoryEntity.CategoryId,
                CategoryName = postcategoryEntity.CategoryName,
                Description = postcategoryEntity.Description,
             
            };
        }

        public string Add(PostCategoryModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.PostCategories.Any(x => x.CategoryName == model.CategoryName);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                PostCategory postEntity = new PostCategory();
                postEntity.CategoryId = model.CategoryId;
                postEntity.CategoryName = model.CategoryName;
                postEntity.Description = model.Description;
                _agriContext.PostCategories.Add(postEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool Put(PostCategoryModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CategoryId = model.CategoryId;
            var categoryEntity = _agriContext.PostCategories.FirstOrDefault(x => x.CategoryId == CategoryId);
            if (categoryEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                categoryEntity.CategoryId = model.CategoryId;
                categoryEntity.CategoryName = model.CategoryName;
                categoryEntity.Description = model.Description;
                _agriContext.SaveChanges();
                return true;
            }
        }


        public string Delete(long CategoryId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            PostCategoryModel model = new PostCategoryModel();
            var categoryEntity = _agriContext.PostCategories.FirstOrDefault(x => x.CategoryId == CategoryId);
            if (categoryEntity != null)
            {
                _agriContext.Remove(categoryEntity);
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


    }
}

