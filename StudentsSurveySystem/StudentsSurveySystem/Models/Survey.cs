using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsSurveySystem.Models
{
    public class Survey
    {
        public int ID { get; set; }
        public int Caregory { get; set; } //Enum

        //Answers
        public int Answer1 { get; set; } //Enum
        public int Answer2 { get; set; } //Enum
        public int Answer3 { get; set; } //Enum
        public int Answer4 { get; set; } //Enum
        public int Answer5 { get; set; } //Enum
        public int Answer6 { get; set; } //Enum
        public int Answer7 { get; set; } //Enum
        public int Answer8 { get; set; } //Enum
        public int Answer9 { get; set; } //Enum
        public int Answer10 { get; set; } //Enum

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}