using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace C1_Blazor_Demo_DotNet8.Models.Entities
{
    [Table("ABS_user_profiles_skb")]

    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("user_locked")]
        public string user_locked { get; set; }
    }
}
