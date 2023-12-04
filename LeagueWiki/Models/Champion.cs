using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeagueWiki.Models
{
    public class Champion
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name cannot be empty!")]
        [MaxLength(50,ErrorMessage ="Length must be between 2 and 50")]
        [DisplayName("Champion")]
        [MinLength(2, ErrorMessage ="Length must be between 2 and 50")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description can't be Empty!")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Choose a difficulty between 1 and 5")]
        [Range(1,5,ErrorMessage = "Choose a difficulty between 1 and 5")]
        [DisplayName("Difficulty")]
        public int Difficulty { get; set; }


        [DisplayName("Release Date")]
        public DateTime AddDate {get; set;}

        [Required(ErrorMessage = "Choose a gender")]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [ValidateNever]
        [DisplayName("Image")]
        public string ImagePath { get; set; }

        [Range(1,double.MaxValue,ErrorMessage="Choose a valid Role!")]
        [DisplayName("Select a Role:")]
        public int RoleId { get; set; }

        [ValidateNever]
        public Role Role { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Choose a valid Country!")]
        [DisplayName("Select a State:")]
        public int CountryId { get; set; }

        [ValidateNever]
        [DisplayName("State")]
        public Country Country { get; set; }
        
    }
}


/*
 * <form method="post">
    @foreach (var gender in Model.Genders)
    {
        <input type="radio" asp-for="Gender" value="@gender" />@gender<br />
    }
    <input type="submit"/>
</form>
*/