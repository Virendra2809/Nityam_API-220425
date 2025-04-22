using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
   public class PostCommentService: IPostCommentService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;

        public PostCommentService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;

        }

        public List<PostCommentDetailModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var PostCommentModelList = new List<PostCommentDetailModel>();
            var commentListEntity = (from comment in _agriContext.PostCommentDetails
                                      join post in _agriContext.PostDetails
                                      on comment.PostId equals post.PostId
                                      join farmer in _agriContext.FarmerMasters
                                      on comment.FarmerId equals farmer.FarmerId
                                      where post.DeleteStatus == false
                                  select new
                                  {
                                      post.PostId,
                                      post.PostHeading,
                                      farmer.FarmerId,
                                      farmer.FirstName,
                                      farmer.LastName,
                                      comment.CommentId,
                                      comment.CommentDetails,
                                      comment.CommentDate
                                  }).ToList();
            if (commentListEntity == null)
            {
                return null;
            }
            foreach (var item in commentListEntity)
            {
                var model = new PostCommentDetailModel();
                model.PostId = item.PostId;
                model.CommentId = item.CommentId;
                model.CommentDetails = item.CommentDetails;
                model.PostHeading = item.PostHeading;
                model.CommentDate = item.CommentDate;
                model.FarmerId = item.FarmerId;
                model.FarmerName = item.FirstName + " " + item.LastName;

                PostCommentModelList.Add(model);
            }
            return PostCommentModelList;
        }
        
        public PostCommentDetailModel GetById(long CommentId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var commentEntity = (from comment in _agriContext.PostCommentDetails
                                  join post in _agriContext.PostDetails
                                  on comment.PostId equals post.PostId
                                  join farmer in _agriContext.FarmerMasters
                                  on comment.FarmerId equals farmer.FarmerId
                                  where post.DeleteStatus == false && comment.CommentId==CommentId
                                  select new
                                  {
                                      post.PostId,
                                      post.PostHeading,
                                      farmer.FarmerId,
                                      farmer.FirstName,
                                      farmer.LastName,
                                      comment.CommentId,
                                      comment.CommentDetails,
                                      comment.CommentDate
                                  }).FirstOrDefault();
            

            if (commentEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            
            var model = new PostCommentDetailModel();
            model.PostId = commentEntity.PostId;
            model.CommentId = commentEntity.CommentId;
            model.CommentDetails = commentEntity.CommentDetails;
            model.PostHeading = commentEntity.PostHeading;
            model.CommentDate = commentEntity.CommentDate;
            model.FarmerId = commentEntity.FarmerId;
            model.FarmerName = commentEntity.FirstName + " " + commentEntity.LastName;

            if (commentEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }

        public string Add(PostCommentDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.PostCommentDetails.Any(x => x.CommentId == model.CommentId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                PostCommentDetail commentEntity = new PostCommentDetail();
                commentEntity.CommentId = model.CommentId;
                commentEntity.PostId = model.PostId;
                commentEntity.FarmerId = model.FarmerId;
                commentEntity.CommentDate = model.CommentDate;
                commentEntity.CommentDetails = model.CommentDetails;
                commentEntity.EnteredBy = model.EnteredBy;
                commentEntity.EnteredDate = model.EnteredDate;
                _agriContext.PostCommentDetails.Add(commentEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool Put(PostCommentDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CommentId = model.CommentId;
            var commentEntity = _agriContext.PostCommentDetails.FirstOrDefault(x => x.CommentId == CommentId);
            if (commentEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                commentEntity.CommentId = model.CommentId;
                commentEntity.PostId = model.PostId;
                commentEntity.FarmerId = model.FarmerId;
                commentEntity.CommentDate = model.CommentDate;
                commentEntity.CommentDetails = model.CommentDetails;
                commentEntity.EnteredBy = model.EnteredBy;
                commentEntity.EnteredDate = model.EnteredDate;

                _agriContext.SaveChanges();

             
             }
                return true;
            
        }




        public string Delete(long CommentId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            PostDetailModel model = new PostDetailModel();
            var postEntity = _agriContext.PostCommentDetails.FirstOrDefault(x => x.CommentId == CommentId);
            if (postEntity != null)
            {
                _agriContext.Remove(postEntity);
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

        public List<PostCommentDetailModel> GetAllComments(long PostId)
        {
            var errorResponseModel = new ErrorResponseModel();
            var PostCommentModelList = new List<PostCommentDetailModel>();
            var commentListEntity = (from comment in _agriContext.PostCommentDetails
                                     join post in _agriContext.PostDetails
                                     on comment.PostId equals post.PostId

                                     join farmer in _agriContext.FarmerMasters
                                     on comment.FarmerId equals farmer.FarmerId

                                     where post.DeleteStatus == false && comment.PostId==PostId
                                     select new
                                     {
                                         post.PostId,
                                         post.PostHeading,
                                         farmer.FarmerId,
                                         farmer.FirstName,
                                         farmer.LastName,
                                         comment.CommentId,
                                         comment.CommentDetails,
                                         comment.CommentDate
                                     }).ToList();
            if (commentListEntity == null)
            {
                //errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                //errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in commentListEntity)
            {
                var model = new PostCommentDetailModel();
                model.PostId = item.PostId;
                model.CommentId = item.CommentId;
                model.CommentDetails = item.CommentDetails;
                model.PostHeading = item.PostHeading;
                model.CommentDate = item.CommentDate;
                model.FarmerId = item.FarmerId;
                model.FarmerName = item.FirstName + " " + item.LastName;

                PostCommentModelList.Add(model);
            }
            return PostCommentModelList;
        }

    }
}

