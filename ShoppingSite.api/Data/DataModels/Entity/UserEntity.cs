using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.api.Data.DataModels.Entity
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Sub { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
