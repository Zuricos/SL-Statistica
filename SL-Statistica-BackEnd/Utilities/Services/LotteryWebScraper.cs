using DataManager.Models;
using HtmlAgilityPack;
using System.Globalization;
using DataManager;
using Microsoft.EntityFrameworkCore;

namespace Utilities.Services;
public class LotteryWebScraper(IDbContextFactory<LotteryDbContext> dbFactory)
{
	private readonly HttpClient _httpClient = new();
	private readonly string _baseUrl = "http://www.mylottoy.net/de/lotto-schweiz/lottozahlen/6aus45/"; //later on get this from appsettings.json

    public async Task<List<LotteryDraw>> ScrapeLotteryData(string year)
	{
		var newDraws = new List<LotteryDraw>();
		try
		{
			var dataList = new List<LotteryDraw>();
			// Fetch the HTML content from the url
			var doc = await RequestHtmlPage($"{_baseUrl}lottozahlen-{year}.asp");

			// Select all elements with lottery values
			var zippedList = FilterLotteryDraws(doc);

			foreach (var draw in zippedList)
			{
				LotteryDraw? lottoDraw = ParseTo(draw.Date, draw.Num, draw.Lucky);
				if (lottoDraw is null) break;
				dataList.Add(lottoDraw);
			}
			await using var ctx = await dbFactory.CreateDbContextAsync();

            var lastDrawDateInDb = ctx.Draws.OrderByDescending(d => d.Date).First().Date;
             newDraws = dataList.Where(d => d.Date.CompareTo(lastDrawDateInDb) > 0).ToList();

            ctx.Draws.UpdateRange(newDraws);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
		}

		return newDraws;
	}

	private async Task<HtmlDocument> RequestHtmlPage(string url)
	{
		var response = await _httpClient.GetAsync(url);
		if (!response.IsSuccessStatusCode) throw new HttpRequestException("Request not succeeded.");

		// Parse the HTML content
		var htmlContent = await response.Content.ReadAsStringAsync();
		var doc = new HtmlDocument();
		doc.LoadHtml(htmlContent);
		return doc;
	}
	private static IEnumerable<(string Date, string Num, string Lucky)> FilterLotteryDraws(HtmlDocument doc)
	{
		var dates = doc.DocumentNode.SelectNodes("//div[@class='span-30']")
												.Where(node => node.ChildNodes.Count == 1 && node.FirstChild.NodeType is HtmlNodeType.Text)
												.Select(node => node.InnerText).ToList();
		Console.WriteLine($"Amount of Dates: {dates.Count}");
		var numbers = doc.DocumentNode.SelectNodes("//div[@class='lz320']")
											.Where(node => node.ChildNodes.Count == 1 && node.FirstChild.NodeType is HtmlNodeType.Text)
											.Select(node => node.InnerText).ToList();
		Console.WriteLine($"Amount of Numbers: {numbers.Count}");
		var luckyNumbers = doc.DocumentNode.SelectNodes("//div[@class='lzmini']")
											.Where(node => node.ChildNodes.Count == 2 && node.ChildNodes["span"].InnerText.Contains("Plus"))
											.Select(node => node.InnerText).ToList();
		Console.WriteLine($"Amount of LuckyNumbers: {luckyNumbers.Count}");
		if (dates.Count != numbers.Count && numbers != luckyNumbers)
		{
			Console.WriteLine("Not an Equal Amount of date, numbers and luckyNumbers could be filtered from the Html");
			Console.WriteLine($"Date: {dates.Count} | Numbers: {numbers.Count} | LuckyNumbers: {luckyNumbers.Count}");
			return new List<(string Date, string Num, string Lucky)>();
		}
		return dates.Zip(numbers, (date, number) => (Date: date, Num: number))
			.Zip(luckyNumbers, (prev, lucky) => (prev.Date, prev.Num, Lucky: lucky));
	}
	private static LotteryDraw? ParseTo(string dateText, string numberText, string luckyNumberText)
	{
		if (!DateTime.TryParseExact(dateText, "dddd 'den' dd.MM.yyyy",
			CultureInfo.GetCultureInfo("de-DE"), DateTimeStyles.None, out var dateTime)) return null;
		var numbers = numberText.Split(" : ").Select(int.Parse).ToArray();
		var luckyNumber = int.Parse(luckyNumberText.Replace("\"", "").Replace("Plus:",""));
		return new LotteryDraw()
		{
			Date = dateTime,
			NumberOne = numbers[0],
			NumberTwo = numbers[1],
			NumberThree = numbers[2],
			NumberFour = numbers[3],
			NumberFive = numbers[4],
			NumberSix = numbers[5],
			LuckyNumber = luckyNumber
		};
	}
}
