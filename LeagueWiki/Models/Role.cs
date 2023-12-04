using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeagueWiki.Models
{
    public class Role
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage ="Role Name cannot be empty")]
        [DisplayName("Role")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Role Description cannot be empty")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ValidateNever]
        [DisplayName("Image")]
        public string ImagePath { get; set; }

        [ValidateNever]
        public List<Champion> Champions { get; set; }
    }
}
