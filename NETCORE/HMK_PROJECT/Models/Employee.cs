using System.ComponentModel.DataAnnotations;

namespace HMK_PROJECT.Models
{
    public class Employee : Person
    {

        public string EmployeeId { get; set; }
        public int Age { get; set; }
    }
}