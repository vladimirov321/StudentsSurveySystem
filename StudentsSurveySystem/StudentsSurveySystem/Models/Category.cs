using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsSurveySystem.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Survey> Surveys { get; set; }
    }
}