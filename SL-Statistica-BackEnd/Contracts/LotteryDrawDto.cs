namespace Contracts;
public record LotteryDrawDto
{
    public required int NumberOne { get; init; }
    public required int NumberTwo { get; init; }
    public required int NumberThree { get; init; }
    public required int NumberFour { get; init; }
    public required int NumberFive { get; init; }
    public required int NumberSix { get; init; }
    public required int LuckyNumber { get; init; }
    public required DateTime Date { get; init; }
    public int? JackPot { get; init; }
}
