using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
   public class PostDetailService:IPostDetailService
    {

        AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;
        private INotificationService _notificationService;

        public PostDetailService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName, INotificationService notificationService)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;
            _notificationService = notificationService;

        }

        public List<PostDetailModel> GetAll(PageParameters pageParameters)
        {
            var errorResponseModel = new ErrorResponseModel();
            var PostDetailModelList = new List<PostDetailModel>();
            var postListEntity = (from post in _agriContext.PostDetails
                                  join category in _agriContext.PostCategories
                                  on post.CategoryId equals category.CategoryId
                                  where post.DeleteStatus == false

                                  select new
                                  {
                                      post.PostId,
                                      post.PostHeading,
                                      post.PostDate,
                                      post.PostDetail1,
                                      category.CategoryId,
                                      category.CategoryName,
                                      post.PostUrl
                                  }).Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
        .Take(pageParameters.PageSize)
.ToList();
            if (postListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in postListEntity)
            {
                var model = new PostDetailModel();
                model.PostId = item.PostId;
                model.CategoryId = item.CategoryId;
                model.CategoryName = item.CategoryName;
                model.PostHeading = item.PostHeading;
                model.PostDetail1 = item.PostDetail1;
                model.PostDate = item.PostDate;
                model.PostUrl = item.PostUrl;
                var CommentCount = _agriContext.PostCommentDetails.Where(x => x.PostId == item.PostId).Count();
                var LikeCount = _agriContext.PostLikeDetails.Where(x => x.PostId == item.PostId).Count();
                model.CommentCount = CommentCount;
                model.Likes = LikeCount;
                var PostImageList = _agriContext.PostImges
                                                  .Where(x => x.PostId == item.PostId).ToList();
                foreach (var itemImages in PostImageList)
                {
                    var imgModel = new PostImageModel();
                    imgModel.ImageId = itemImages.ImageId;
                    imgModel.ImageName = itemImages.ImageName;
                    imgModel.ImageUrl = _configuration.HostName + itemImages.ImageUrl;
                    imgModel.PostId = model.PostId;
                    model.PostImages.Add(imgModel);
                }

                foreach (var itemImages in PostImageList)
                {
                    var imgModel = new PostImageForMobileModel();
                    imgModel.Img = _configuration.HostName + itemImages.ImageUrl;
                    model.Images.Add(imgModel);
                }
                if (PostImageList.Count == 0)
                {
                    var imgModel = new PostImageForMobileModel();

                    imgModel.Img = _configuration.HostName + "/PostImages/no_image.png";
                    model.Images.Add(imgModel);

                }

                PostDetailModelList.Add(model);
            }
            return PostDetailModelList;
        }

        public List<PostDetailModel> GetAllPost(long CategoryId, PageParameters pageParameters)
        {
            var errorResponseModel = new ErrorResponseModel();
            var PostDetailModelList = new List<PostDetailModel>();
            var postListEntity = (from post in _agriContext.PostDetails
                                  join category in _agriContext.PostCategories
                                  on post.CategoryId equals category.CategoryId
                                  where post.DeleteStatus == false && post.CategoryId==CategoryId

                                  select new
                                  {
                                      post.PostId,
                                      post.PostHeading,
                                      post.PostDate,
                                      post.PostDetail1,
                                      category.CategoryId,
                                      category.CategoryName,
                                      post.PostUrl
                                  }).Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
        .Take(pageParameters.PageSize).ToList();
            if (postListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in postListEntity)
            {
                var model = new PostDetailModel();
                model.PostId = item.PostId;
                model.CategoryId = item.CategoryId;
                model.CategoryName = item.CategoryName;
                model.PostHeading = item.PostHeading;
                model.PostDetail1 = item.PostDetail1;
                model.PostDate = item.PostDate;
                model.PostUrl = item.PostUrl;
                var CommentCount = _agriContext.PostCommentDetails.Where(x => x.PostId == item.PostId).Count();
                var LikeCount = _agriContext.PostLikeDetails.Where(x => x.PostId == item.PostId).Count();
                model.CommentCount = CommentCount;
                model.Likes = LikeCount;
                var PostImageList = _agriContext.PostImges
                                                  .Where(x => x.PostId == item.PostId).ToList();
                foreach (var itemImages in PostImageList)
                {
                    var imgModel = new PostImageModel();
                    imgModel.ImageId = itemImages.ImageId;
                    imgModel.ImageName = itemImages.ImageName;
                    imgModel.ImageUrl = _configuration.HostName + itemImages.ImageUrl;
                    imgModel.PostId = model.PostId;
                    model.PostImages.Add(imgModel);
                }

                foreach (var itemImages in PostImageList)
                {
                    var imgModel = new PostImageForMobileModel();
                    imgModel.Img = _configuration.HostName + itemImages.ImageUrl;
                    model.Images.Add(imgModel);
                }
                if (PostImageList.Count == 0)
                {
                    var imgModel = new PostImageForMobileModel();

                    imgModel.Img = _configuration.HostName + "/PostImages/no_image.png";
                    model.Images.Add(imgModel);

                }

                PostDetailModelList.Add(model);
            }
            return PostDetailModelList;
        }
        public PostDetailModel GetById(long PostId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var postListEntity = (from post in _agriContext.PostDetails
                                  join category in _agriContext.PostCategories
                                  on post.CategoryId equals category.CategoryId
                                  where post.DeleteStatus == false && post.PostId==PostId   
                                  select new
                                  {
                                      post.PostId,
                                      post.PostHeading,
                                      post.PostDetail1,
                                      post.PostDate,
                                      category.CategoryId,
                                      category.CategoryName,
                                      post.PostUrl
                                  }).FirstOrDefault();

            if (postListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            var model = new PostDetailModel();

            model.PostId = postListEntity.PostId;
            model.CategoryId = postListEntity.CategoryId;
            model.CategoryName = postListEntity.CategoryName;
            model.PostHeading = postListEntity.PostHeading;
            model.PostDetail1 = postListEntity.PostDetail1;
            model.PostDate = postListEntity.PostDate;
            model.PostUrl = postListEntity.PostUrl;

                var PostImageList = _agriContext.PostImges
                                                  .Where(x => x.PostId == postListEntity.PostId).ToList();
            foreach (var itemImages in PostImageList)
            {
                var imgModel = new PostImageModel();
                imgModel.ImageId = itemImages.ImageId;
                imgModel.ImageName = itemImages.ImageName;
                imgModel.ImageUrl = _configuration.HostName + itemImages.ImageUrl;
                imgModel.PostId = postListEntity.PostId;
                model.PostImages.Add(imgModel);
            }
            if (PostImageList.Count == 0)
            {
                var imgModel = new PostImageModel();
                imgModel.ImageUrl = _configuration.HostName + "/PostImages/no_image.png"; ;
                imgModel.PostId = postListEntity.PostId;
                model.PostImages.Add(imgModel);
               
            }

            foreach (var itemImages in PostImageList)
            {
                var imgModel = new PostImageForMobileModel();
                imgModel.Img = _configuration.HostName + itemImages.ImageUrl;
                model.Images.Add(imgModel);
            }
            if (PostImageList.Count == 0)
            {
                var imgModel = new PostImageForMobileModel();

                imgModel.Img = _configuration.HostName + "/PostImages/no_image.png";
                model.Images.Add(imgModel);

            }

            if (postListEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }
      

        public string Add(PostDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
     
            var existing = _agriContext.PostDetails.Any(x => x.PostId == model.PostId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var postEntity = new PostDetail();
                postEntity.PostId = model.PostId;
                postEntity.CategoryId = model.CategoryId;
                postEntity.PostHeading = model.PostHeading;
                postEntity.PostDate = model.PostDate;
                postEntity.PostDetail1 = model.PostDetail1;
                postEntity.DeleteStatus = false;
                postEntity.PostUrl = model.PostUrl;
                _agriContext.PostDetails.Add(postEntity);
                _agriContext.SaveChanges();
                var ImageEntity = new PostImge();

                foreach (var item in model.PostImages)
                {
                  
                    ImageEntity.ImageId = item.ImageId;
                    ImageEntity.PostId = postEntity.PostId;
                    ImageEntity.ImageName = item.ImageName;
                    ImageEntity.ImageUrl = item.ImageUrl;
                    if (model.PostImages.Count == 0)
                    {
                        ImageEntity.PostId = postEntity.PostId;
                        ImageEntity.ImageUrl = "/PostImages/no_image.png";
                    }

                    _agriContext.Add(ImageEntity);
                    _agriContext.SaveChanges();
                    string Image = _configuration.HostName + ImageEntity.ImageUrl;
                    var farmerlist = _agriContext.FarmerMasters.Where(x => x.DeleteStatus == false).ToList();
                    foreach (var items in farmerlist)
                    {
                        _notificationService.SendNotification(items.DeviceToken,postEntity.PostId, model.PostHeading, true, model.PostDetail1, "Post");
                    }

                }
                message = "Added Successfully";
            }
            return message;                                         
        }

        public bool Put(PostDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var PostId = model.PostId;
            var postEntity = _agriContext.PostDetails.FirstOrDefault(x => x.PostId == PostId);
            if (postEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                postEntity.CategoryId = model.CategoryId;
                postEntity.PostHeading = model.PostHeading;
                postEntity.PostDetail1 = model.PostDetail1;
                postEntity.PostDate = model.PostDate;
                postEntity.DeleteStatus = false;
                postEntity.PostUrl = model.PostUrl;
                _agriContext.SaveChanges();
                var ImageEntity = new PostImge();
                if (model.PostImages.Count > 0)
                {
                    foreach (var item in model.PostImages)
                    {
                        ImageEntity.ImageId = item.ImageId;
                        ImageEntity.PostId = postEntity.PostId;
                        ImageEntity.ImageName = item.ImageName;
                        ImageEntity.ImageUrl = item.ImageUrl;
                        _agriContext.Add(ImageEntity);
                        _agriContext.SaveChanges();

                    }
                }
                if (model.PostImages.Count == 0)
                {
                    ImageEntity.PostId = postEntity.PostId;
                    ImageEntity.ImageUrl = "/PostImages/no_image.png";
                }

                string Image = _configuration.HostName + ImageEntity.ImageUrl;
                var farmerlist = _agriContext.FarmerMasters.Where(x => x.DeleteStatus == false && x.DeviceToken !=null).ToList();
                foreach (var items in farmerlist)
                    {
                        _notificationService.SendNotification(items.DeviceToken,postEntity.PostId, model.PostHeading, true, model.PostDetail1, "Post");
                    }
                
                return true;
            }
        }



        public string DeleteImageFromDB(PostDetailModel model)
        {
            var imageEntityList = _agriContext.PostImges.Where(x => x.PostId == model.PostId).ToList();

            foreach (var item in imageEntityList)
            {
                var imgEntity = _agriContext.PostImges.FirstOrDefault(x => x.ImageId == item.ImageId);
                _agriContext.Remove(imgEntity);
            }
            _agriContext.SaveChanges();

            return "deleted.";
        }

        public string Delete(long PostId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            PostDetailModel model = new PostDetailModel();
            var postEntity = _agriContext.PostDetails.FirstOrDefault(x => x.PostId == PostId);
            if (postEntity != null)
            {
                postEntity.DeleteStatus = true;
                postEntity.ChangedBy = model.ChangedBy;
                postEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


    }
}


