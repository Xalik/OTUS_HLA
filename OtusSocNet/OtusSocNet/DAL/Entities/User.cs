using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtusSocNet.DAL.Entities;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    [MaxLength(36)]
    public Guid Id { get; set; }
    
    [Column("first_name")]
    [MaxLength(20)]
    public string FirstName { get; set; }
    
    [Column("second_name")]
    [MaxLength(20)]
    public string SecondName { get; set; }
    
    [Column("birth_date")]
    public DateOnly BirthDate { get; set; }
    
    [Column("is_male")]
    public bool IsMale { get; set; }
    
    [Column("biography")]
    [MaxLength(100)]
    public string Biography { get; set; }
    
    [Column("city")]
    [MaxLength(20)]
    public string City { get; set; }
    
    [Column("token")]
    [MaxLength(50)]
    public string Token { get; set; }
    
    [Column("token_expires")]
    public DateTime TokenExpires { get; set; }
    
    [Column("password_hash")]
    public byte[] PasswordHash { get; set; }
    
    [Column("salt")]
    [MaxLength(10)]
    public byte[] Salt { get; set; }
}