public static class DateTimeExtension
{
    /// <summary>
    /// Verifies if date is a sturday or wednesday and it is not in the future
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static bool VerifyLotteryDrawDate(this DateTime date)
    {
        if(date.DayOfWeek != DayOfWeek.Wednesday || date.DayOfWeek != DayOfWeek.Saturday) return false;
        if(date.CompareTo(DateTime.Now) > 0) return false;
        return true;
    }

    /// <summary>
    /// Removes times from date 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
	public static DateTime ToDateOnly(this DateTime date)
	{
		return DateOnly.FromDateTime(date).ToDateTime(TimeOnly.MinValue);
	}
}