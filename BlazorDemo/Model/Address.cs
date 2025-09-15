using System.ComponentModel.DataAnnotations;

namespace BlazorDemo.Model
{
    public class Address
    {
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string? Name { get; set; }
        [Required, MinLength(5), MaxLength(100)]
        public string? Street { get; set; }
        [Required, MinLength(2), MaxLength(50)]
        public string? City { get; set; }
        [Required, MinLength(2), MaxLength(50)]
        public string? State { get; set; }
        [Required, RegularExpression(@"^([0-9]{5})$")]
        public string? ZipCode { get; set; }
    }
}
