using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int? QuestionMasterId { get; set; }
        public string Question1 { get; set; }
        public int? SeqNo { get; set; }
        public decimal? Weightage { get; set; }
        public bool? DeleteStatus { get; set; }
        public string QuestionImageName { get; set; }
        public string QuestionImageUrl { get; set; }

        public virtual QuestionMaster QuestionMaster { get; set; }
    }
}
