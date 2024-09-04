using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace C1_Blazor_Demo_DotNet8.Models.Entities
{


    [Table("ABS_SYS_APP_FUNCTION")]

    public class ABS_SYS_APP_FUNCTION
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("FUNCTION_ID")]
        public string FUNCTION_ID { get; set; }
        [Column("FUNCTION_NM")]
        public string FUNCTION_NM { get; set; }
    }
}
