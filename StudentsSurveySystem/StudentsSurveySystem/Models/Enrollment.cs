using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsSurveySystem.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int SurveyID { get; set; }

        public virtual Student Student { get; set; }
        public virtual Survey Survey { get; set; }
    }
}