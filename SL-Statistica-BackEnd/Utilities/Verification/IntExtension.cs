namespace Utilities.Verification;
public static class IntExtension
{
	/// <summary>
	/// Returns true when year lies between 2013 and the current year
	/// </summary>
	/// <param name="year"></param>
	public static bool IsYearInLotteryDrawTimeSpan(this int year)
	{
		if (year > DateTime.Now.Year) return false;
		if (year < 2013) return false;
		return true;
	}
}

