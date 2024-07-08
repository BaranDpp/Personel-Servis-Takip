using Microsoft.AspNetCore.Identity;

namespace PersonelServisTakip.Models.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string FullName { get; set; }
    }
}
