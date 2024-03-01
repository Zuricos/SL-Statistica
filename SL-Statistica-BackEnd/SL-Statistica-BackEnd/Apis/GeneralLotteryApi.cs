using Contracts;
using DataManager;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Utilities.Services;
using Utilities.Verification;

namespace SL_Statistica_BackEnd.Apis;

public static class GeneralLotteryApi
{
    public static WebApplication AddGeneralLotteryApi(this WebApplication app)
    {
        app.MapGet("/LatestDraw", async Task<Results<Ok<LotteryDrawDto>, NotFound>> (LotteryDbContext ctx) =>
        {
            var lastDraw = await ctx.Draws.OrderBy(l => l.Date).LastOrDefaultAsync();
            if (lastDraw != null)
            {
                return TypedResults.Ok(lastDraw.MapToDto());
            }
            return TypedResults.NotFound();
        });

        app.MapGet("/",  Results<Ok<IEnumerable<LotteryDrawDto>>, BadRequest> (LotteryDbContext ctx, [AsParameters] LotteryDrawQueryParameters parameters) =>
        {
            var draws = ctx.Draws.FilterByParameter(parameters);
            return TypedResults.Ok(draws.Select(d => d.MapToDto()));
        });

		app.MapPost("/UpdateDB", async Task<Results<Ok<IEnumerable<LotteryDrawDto>>, BadRequest>>
            (int? year, LotteryWebScraper webScrapper) =>
		{
			var yearSet = year ?? DateTime.Now.Year;
            if (!yearSet.IsYearInLotteryDrawTimeSpan()) return TypedResults.BadRequest();

			var newDraws = await webScrapper.ScrapeLotteryData(yearSet.ToString());

			return TypedResults.Ok(newDraws.Select(d => d.MapToDto()));
		});
        return app;
    }

}


