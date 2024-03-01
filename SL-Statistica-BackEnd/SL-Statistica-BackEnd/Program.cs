using DataManager;
using SL_Statistica_BackEnd.Apis;
using Utilities.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<LotteryDbContext>();
builder.Services.AddSingleton<LotteryWebScraper>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//app.UseAuthorization();
app.AddGeneralLotteryApi();
app.Run();
