using Microsoft.AspNetCore.Identity;

namespace DietCalculatorSystem.Data.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

    }
}
