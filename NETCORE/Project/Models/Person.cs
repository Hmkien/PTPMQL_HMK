using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Person
    {
        [Key]
        public string IdentityNumber { get; set; }
        public string FulLname { get; set; }
        public string Address { get; set; }
    }
}