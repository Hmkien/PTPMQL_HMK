using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Student
    {
        [Key]
        public string StduentID { get; set; }
        public string FulLname { get; set; }
        public string Address { get; set; }
    }
}