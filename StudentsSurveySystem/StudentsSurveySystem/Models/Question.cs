using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsSurveySystem.Models
{
    public class Question
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public int SurveyID { get; set; }

        public virtual Survey Survey { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}