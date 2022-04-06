using BowlingEngine;
using System.Text;

BowlingGame game = new();

while (!game.IsGameFinished)
{
    PrintGame(game);
    Console.ReadLine();
    game.Roll();
}
PrintGame(game);



void PrintGame(BowlingGame game)
{
    StringBuilder builder = new();

    //TODO: Print header


    builder.AppendLine(String.Format("|{0,7}|{1,7}|{2,7}|{3,7}|", "Frame", "Roll 1", "Roll 2", "Roll 3"));

    int frameNumber = 1;
    foreach (var frame in game.Frames)
    {
        builder.AppendLine(String.Format("|{0,7}|{1,7}|{2,7}|{3,7}|", frameNumber++, frame.Rolls[0], frame.Rolls[1], frame.Rolls.Count() > 2 ? frame.Rolls[2].ToString() : ""));
    }

    builder.AppendLine($"Total score: {game.Score}");

    Console.Clear();
    Console.WriteLine(builder.ToString());

}