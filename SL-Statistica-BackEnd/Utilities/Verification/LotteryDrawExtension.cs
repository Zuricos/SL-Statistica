using Contracts;
using DataManager.Models;

namespace Utilities.Verification;
public static class LotteryDrawExtension
{
	public static IEnumerable<LotteryDraw> FilterByParameter(this IEnumerable<LotteryDraw> lotteryDraws, LotteryDrawQueryParameters parameters)
	{
		IEnumerable<LotteryDraw> filtered = new List<LotteryDraw>();
		//Early Out if Date is set
		if (parameters.Date is not null)
		{
			if (parameters.Date.Value.VerifyLotteryDrawDate())
				return lotteryDraws.Where(d => d.Date.Equals(parameters.Date.Value.ToDateOnly()));
			else
				return filtered;
		}

		filtered = parameters.OrderByAscending ? lotteryDraws.OrderBy(d => d.Date) : lotteryDraws.OrderByDescending(d => d.Date);

		if (parameters.FromDate is not null)
		{
			filtered = filtered.Where(d => d.Date.CompareTo(parameters.FromDate.Value) > 0);
		}
		if (parameters.ToDate is not null)
		{
			filtered = filtered.Where(d => d.Date.CompareTo(parameters.ToDate.Value) < 0);
		}
		if (parameters.Skip is not null)
		{
			filtered = filtered.Skip(parameters.Skip.Value);
		}
		if (parameters.Take is not null)
		{
			filtered = filtered.Take(parameters.Take.Value);
		}
		return filtered;
	}

	public static LotteryDraw MapToLotteryDraw(this LotteryDrawDto dto)
	{
		return new LotteryDraw()
		{
			Date = dto.Date,
			NumberOne = dto.NumberOne,
			NumberTwo = dto.NumberTwo,
			NumberThree = dto.NumberThree,
			NumberFour = dto.NumberFour,
			NumberFive = dto.NumberFive,
			NumberSix = dto.NumberSix,
			LuckyNumber = dto.LuckyNumber
		};
	}

	public static LotteryDrawDto MapToDto(this LotteryDraw draw)
	{
		return new LotteryDrawDto()
		{
			Date = draw.Date,
			NumberOne = draw.NumberOne,
			NumberTwo = draw.NumberTwo,
			NumberThree = draw.NumberThree,
			NumberFour = draw.NumberFour,
			NumberFive = draw.NumberFive,
			NumberSix = draw.NumberSix,
			LuckyNumber = draw.LuckyNumber
		};
	}
}
