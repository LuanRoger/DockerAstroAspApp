using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

[Table("Users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; init; }
    
    [MaxLength(50)]
    [Required]
    public required string name { get; init; }
    
    [Required]
    public required string email { get; init; }
}