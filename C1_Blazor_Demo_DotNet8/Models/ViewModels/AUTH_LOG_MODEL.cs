using System.ComponentModel.DataAnnotations.Schema;

namespace C1_Blazor_Demo_DotNet8.Models.ViewModels
{
    public class AUTH_LOG_MODEL
    {

        public string? LOG_ID { get; set; }
        public string? COLUMN_NAME { get; set; }
        public string? DISPLAY_NAME { get; set; }
        public string? OLD_VALUE { get; set; }
        public string? NEW_VALUE { get; set; }
        public string? DISPLAY_FLAG { get; set; }
        public string? REWORK_FLAG { get; set; }
        public string? TABLE_NAME { get; set; }
        public string? BRANCHID { get; set; }
        public string? MAKE_BY { get; set; }
        public string? MAKE_DATE { get; set; }
    }
}
