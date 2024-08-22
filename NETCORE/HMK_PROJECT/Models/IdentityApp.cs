using Microsoft.AspNetCore.Identity;

namespace HMK_PROJECT.Models
{
    public class IdentityApp : IdentityUser
    {
        public string FullName { get; set; }
    }
}