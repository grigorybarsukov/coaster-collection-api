namespace WebApi.Controllers;

public class CoasterParameters
{
    public int PageNumber { get; set; } = 1;
    public int? PageSize { get; set; }
    public string SortBy { get; set; } = "";
    public string SortByDescending { get; set; } = "";
}