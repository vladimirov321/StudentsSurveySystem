using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsSurveySystem.Models
{
    public enum ChosenAnswer
    {
        A, B, C, D, E
    }

    public class Answer
    {
            public int ID { get; set; }
            public ChosenAnswer? ChosenAnswer { get; set; }
            public Specialty? StudentSpecialty { get; set; }
            public int StudentAge { get; set; }
            public Gender? StudentGender { get; set; }
            public int YearOfStudy { get; set; }
            public int QuestionID { get; set; }

            public virtual Question Question { get; set; }
    }
}