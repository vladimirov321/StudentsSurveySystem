using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsSurveySystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

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

        public string Name
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public int Specialty { get; set; } //Enum
        public int Age { get; set; }
        public int Gender { get; set; } //Enum
        //Here List of Surveys

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
    }
}