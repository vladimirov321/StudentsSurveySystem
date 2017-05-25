using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsSurveySystem.Models
{
    public class Survey
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }
        public ICollection<Question> Questions { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}