using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsSurveySystem.Models
{
    public class Survey
    {
        public int ID { get; set; }
        public int Category { get; set; } // 1 - Оценка на преподаватели; 2 - Качество на учебния процес; 3 - Условия за професионална реализация; 4 - Оценка за престижа на университета сред студентите  

        public ICollection<Question> Questions { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}