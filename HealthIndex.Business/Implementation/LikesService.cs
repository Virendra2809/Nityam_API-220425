using StartUpX.Business.Interface;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
   public  class LikesService:ILikesService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;

        public LikesService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;

        }

        
        public string Add(LikeModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existing = _agriContext.PostLikeDetails.Where(x => x.PostId == model.PostId && x.LikedBy==model.LikedBy).FirstOrDefault();

            if (existing != null)
            {

                _agriContext.Remove(existing);
                _agriContext.SaveChanges();
                    message = "Unlike ";
            }
            else
            {
                var likeEntity = new PostLikeDetail();
                likeEntity.PostId = model.PostId;
                likeEntity.PostLikeId = model.PostLikeId;
                likeEntity.LikedBy = model.LikedBy;
                likeEntity.LikedDate = model.LikedDate;
                _agriContext.PostLikeDetails.Add(likeEntity);
                _agriContext.SaveChanges();
                message = "Like";
            }
            return message;
        }

    }
}
