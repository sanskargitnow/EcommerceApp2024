using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    public class Category
    { 
        [Required]
        
        public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
    public int DisplayOrder { get; set; }
}
}

