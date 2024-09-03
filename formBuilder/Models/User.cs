using Microsoft.AspNetCore.Identity;

namespace formBuilder.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
    }
}
