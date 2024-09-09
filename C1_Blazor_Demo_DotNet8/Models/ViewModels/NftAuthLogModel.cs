namespace C1_Blazor_Demo_DotNet8.Models.ViewModels
{
    public class NftAuthLogModel
    {
        public string? LogId { get; set; } 
        public string? BranchId { get; set; } 
        public string? FunctionId { get; set; } 
        public string? ActionStatus { get; set; }
        public string? Remarks { get; set; } 
        public string? MakeBy { get; set; }
        public DateTime? MakeDate { get; set; } 
        public string? AuthLevel { get; set; }
        public string? AuthStatusId { get; set; } 
        public string? AuthBy { get; set; } 
        public DateTime? AuthDate { get; set; } 
        public string? AuthPendingLevel { get; set; } 
        public string LogTableId { get; set; } 
        public string? TableName { get; set; } 
        public string? TableDisplayName { get; set; } 
        public string? PkColumnName { get; set; } 
        public string? PkDisplayName { get; set; } 
        public string? PkColumnValue { get; set; } 
        public string? ParentTableFlag { get; set; } 
        public string? LogTabId { get; set; } 
        public string? ColumnName { get; set; } 
        public string? DisplayName { get; set; } 
        public string? OldValue { get; set; } 
        public string? NewValue { get; set; } 
        public string? DisplayFlag { get; set; } 
        public string? ReworkFlag { get; set; } 
    }
}
