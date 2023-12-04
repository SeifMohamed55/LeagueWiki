using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeagueWiki.Models
{
    public class Country
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Country name!")]
        [DisplayName("State")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Description field is required")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ValidateNever]
        [DisplayName("Image")]
        public string ImagePath { get; set; }


        [ValidateNever]
        public List<Champion> Champions { get; set; }
    }
}
