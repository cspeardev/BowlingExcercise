using BowlingEngine;
using System.Text;


bool continuePlaying = true;

while (continuePlaying)
{
    BowlingGame game = new();
    while (!game.IsGameFinished)
    {
        PrintGame(game);
        Console.WriteLine("Press enter to roll.");
        Console.ReadLine();
        game.Roll();
    }
    PrintGame(game);
    Console.WriteLine("Game over.");
    Console.WriteLine("Would you like to play again?");
    Console.WriteLine("Enter press Y and enter to play another game.");
    string? input = Console.ReadLine();
    continuePlaying = input?.ToLower() == "y";
}




static void PrintGame(BowlingGame game)
{
    StringBuilder builder = new();

    builder.AppendLine(String.Format("|{0,7}|{1,7}|{2,7}|{3,7}|{4,12}|", "Frame", "Roll 1", "Roll 2", "Roll 3", "Frame Score"));

    int frameNumber = 1;
    foreach (var frame in game.Frames)
    {
        builder.AppendLine(String.Format("|{0,7}|{1,7}|{2,7}|{3,7}|{4,12}|", frameNumber++, frame.Rolls[0], frame.Rolls[1], frame.Rolls.Length > 2 ? frame.Rolls[2].ToString() : "", frame.Score));
    }

    if(game.LastRoll != null)
    {
        if (game.LastRoll.Strike)
        {
            builder.AppendLine("You rolled a strike!");
        }
        else if (game.LastRoll.Spare)
        {
            builder.AppendLine($"You hit {game.LastRoll.PinsHit} pin{(game.LastRoll.PinsHit == 1 ? "" : "s")} and got a spare!");
        }
        else
        {

            builder.AppendLine($"You hit {game.LastRoll.PinsHit} pin{(game.LastRoll.PinsHit == 1 ? "" : "s")}.");
        }
    }

    builder.AppendLine($"Total score: {game.Score}");
    
    Console.Clear();
    Console.WriteLine(builder.ToString());

}