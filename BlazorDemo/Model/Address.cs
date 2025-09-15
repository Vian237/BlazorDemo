using System.ComponentModel.DataAnnotations;

namespace BlazorDemo.Model
{
    public class Address
    {
        public int Id { get; set; }
        [Required, MinLength(3, ErrorMessage ="Please use a Name bigger than 3 letters."), MaxLength(50, ErrorMessage ="Please use a Name less than 100 letters.")]
        public string? Name { get; set; }
        [Required, MinLength(5, ErrorMessage = "Please use an Address bigger than 5 letters."), MaxLength(100, ErrorMessage = "Please use an Address less than 100 letters.")]
        public string? Street { get; set; }
        [Required, MinLength(2, ErrorMessage = "Please use a City bigger than 2 letters."), MaxLength(50, ErrorMessage = "Please use a City bigger less 50 letters.")]
        public string? City { get; set; }
        [Required, MinLength(2, ErrorMessage = "Please use a State bigger than 2 letters."), MaxLength(50, ErrorMessage = "Please use a State less than 50 letters.")]
        public string? State { get; set; }
        [Required, RegularExpression(@"^([0-9]{5})$", ErrorMessage ="Please use a valid Postal code with five numbers.")]
        public string? ZipCode { get; set; }
    }
}
