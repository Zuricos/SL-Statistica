using Contracts;
using DataManager;
using DataManager.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Utilities.Services;
using Utilities.Verification;

namespace SL_Statistica_BackEnd.Apis;

public static class GeneralLotteryApi
{
    public static WebApplication AddGeneralLotteryApi(this WebApplication app)
    {
        app.MapGet("/LatestDraw", async Task<Results<Ok<LotteryDrawDto>, BadRequest>> (LotteryDbContext ctx) =>
        {
            var lastDraw = await ctx.Draws.OrderBy(l => l.Date).LastOrDefaultAsync();
            if (lastDraw != null)
            {
                return TypedResults.Ok(lastDraw.MapToDto());
            }
            return TypedResults.BadRequest();
        });

        app.MapGet("/",  Results<Ok<IEnumerable<LotteryDrawDto>>, BadRequest> (LotteryDbContext ctx, [AsParameters] LotteryDrawQueryParameters parameters) =>
        {
            var draws = ctx.Draws.FilterByParameter(parameters);
            return TypedResults.Ok(draws.Select(d => d.MapToDto()));
        });

		app.MapGet("/UpdateDB", async Task<Results<Ok<IEnumerable<LotteryDrawDto>>, BadRequest>>
            (int? year, LotteryWebScraper webscraper, LotteryDbContext ctx) =>
		{
			int yearSet = year ?? DateTime.Now.Year;
            if (!yearSet.IsYearInLotteryDrawTimeSpan()) return TypedResults.BadRequest();

			List<LotteryDraw> fetchedDraws = await webscraper.ScrapeLotteryData(yearSet.ToString());
			var lastDrawDateInDb = ctx.Draws.OrderByDescending(d => d.Date).First().Date;
			var newDraws = fetchedDraws.Where(d => d.Date.CompareTo(lastDrawDateInDb) > 0);
			
            ctx.Draws.UpdateRange(newDraws);
            ctx.SaveChanges();

			return TypedResults.Ok(newDraws.Select(d => d.MapToDto()));
		});
        return app;
    }

}


