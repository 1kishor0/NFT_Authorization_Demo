using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C1_Blazor_Demo_DotNet8.Models.Entities
{
    [Table("NFT_AUTH_LOG_VAL_SKB")]
    public class AUTH_LOG
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("LOG_ID")]
        public string LOG_ID { get; set; }
        [Column("COLUMN_NAME")]
        public string COLUMN_NAME { get; set; }
        [Column("DISPLAY_NAME")]
        public string DISPLAY_NAME { get; set; }
        [Column("OLD_VALUE")]
        public string OLD_VALUE { get; set; }
        [Column("NEW_VALUE")]
        public string NEW_VALUE { get; set; }
        [Column("DISPLAY_FLAG")]
        public string DISPLAY_FLAG { get; set; }
        [Column("REWORK_FLAG")]
        public string REWORK_FLAG { get; set; }
        [Column("BRANCHID")]
        public string TABLE_NAME { get; set; }
        [Column("TABLE_NAME")]
        public string BRANCHID { get; set; }
        [Column("MAKE_BY")]
        public string MAKE_BY { get; set; }
        [Column("MAKE_DATE")]
        public string MAKE_DATE { get; set; }

    }
}
