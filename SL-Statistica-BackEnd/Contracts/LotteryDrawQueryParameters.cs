namespace Contracts;
public class LotteryDrawQueryParameters
{
    public DateTime? Date { get; set; }
    public int? Take { get; set; }
    public int? Skip { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool OrderByAscending { get; set; }
}

public class Numbers
{
    public int One { get; set; }
    public int Two { get; set; }
}
