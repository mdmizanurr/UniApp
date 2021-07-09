using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniApp.Models
{
    public class Instructor
    {
        public int ID { get; set; }

        [Required, Display(Name = "First Name"), StringLength(20)]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name"), StringLength(20)]
        public string LastName { get; set; }

      
        [Display(Name = "Hire Date"), DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }
     
        [Display(Name ="Full Name")]
        public string FullName 
        { 
            get { return FirstName + ", " + LastName; } 
        }

        public ICollection<CourseAssignment> CourseAssignments { get; set; }
        public OfficeAssignment OfficeAssignment { get; set; }





    }
}
