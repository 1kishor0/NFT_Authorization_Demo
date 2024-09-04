namespace C1_Blazor_Demo_DotNet8.Models.ViewModels
{
    public class UserAccountModel
    {
        public int LOG_ID { get; set; }
        public string? UserId { get; set; }
        public string? BranchId { get; set; }
        public string? FUNCTION_ID { get; set; }
        public string? FUNCTION_NM { get; set; }
        public string? PK_COLUMN_NAME { get; set; }
        public string? PK_COLUMN_VALUE { get; set; }
        public string? AUTH_BY { get; set; }
        public string? AUTH_DATE { get; set; }
        public string? Password { get; set; }
        public string? user_locked { get; set; }
    }
}
