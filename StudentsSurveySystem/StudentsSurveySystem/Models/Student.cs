using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsSurveySystem.Models
{
    public enum Specialty
    {
        [Display(Name = "Компютърни науки")]
        ComputerScience,
        [Display(Name = "Компютърни системи и технологии")]
        ComputerSystemsAndTechnologies,
        [Display(Name ="Педагогика")]
        Pedagogy
    }

    public enum Gender
    {
        [Display(Name = "Мъж")]
        Male,
        [Display(Name = "Жена")]
        Female
    }

    public class Student 
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "The faculty number must be 6 digits long")]
        [Display(Name = "Faculty number")]
        public string FNumber { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Name
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public Specialty? Specialty { get; set; } 
        public int Age { get; set; }
        public Gender? Gender { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        //public virtual ApplicationUser User { get; set; }
        //[Required]
        //public string ApplicationUserId { get; set; }
    }
}