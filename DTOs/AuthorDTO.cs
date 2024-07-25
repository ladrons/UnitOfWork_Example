using System.ComponentModel.DataAnnotations;

namespace UnitOfWork_Example.DTOs
{
    public class AuthorDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
