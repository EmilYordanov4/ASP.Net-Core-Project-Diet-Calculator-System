using DietCalculatorSystem.Data.Models.OneToOneRelationships;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietCalculatorSystem.Data.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public string BalancedDietId { get; set; }
        public BalanceDiet BalancedDiet { get; set; }

        public string DeficitDietId { get; set; }
        public DeficitDiet DeficitDiet { get; set; }

        public string SurplusDietId { get; set; }
        public SurplusDiet SurplusDiet { get; set; }
    }
}
