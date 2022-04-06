namespace BowlingEngine;

/// <summary>
/// Class for logic of bowling game.
/// </summary>
public class BowlingGame
{
    public List<Frame> Frames { get; private set; }
    
    private const int FRAME_COUNT = 10;
    private const int FINAL_FRAME_ROLLS = 3;
    private const int FRAME_ROLLS = 2;

    private static Random r = new();

    private List<Roll> rolls = new();
    private Roll? PreviousRoll;

    /// <summary>
    /// Constructor.
    /// </summary>
    public BowlingGame()
    {
        Frames = new List<Frame>();
        for (int i = 0; i < (FRAME_COUNT-1) ; i++)
        {
            Frames.Add(new(FRAME_ROLLS, false));
        };
        Frames.Add(new(FINAL_FRAME_ROLLS, true));
    }


    /// <summary>
    /// Perform a roll.
    /// </summary>
    public void Roll()
    {
        var rollingFrame = Frames.FirstOrDefault(f => !f.Finished);
        if (rollingFrame != null)
        {
            int rolledPins = r.Next(0, (rollingFrame.RemainingPins + 1));
            Roll roll = new Roll(rolledPins, rollingFrame, PreviousRoll);
            PreviousRoll = roll;
            rollingFrame.Roll(roll);
            rolls.Insert(0, roll);
            SpareStrikeBonus(rolledPins);
        }
    }

    /// <summary>
    /// Adds bonus for strike or roll to a frame.
    /// </summary>
    /// <param name="rolledPins"></param>
    private void SpareStrikeBonus(int rolledPins)
    {
        if (rolls.ElementAtOrDefault(1) != null && rolls[1].Spare)
        {
            rolls[1].RolledFrame.AddSparePoints(rolledPins);
        }
        if(rolls.ElementAtOrDefault(1) != null && rolls[1].Strike)
        {
            rolls[1].RolledFrame.AddStrikePoints(rolledPins);
        }
        if(rolls.ElementAtOrDefault(2) != null && rolls[2].Strike)
        {
            rolls[2].RolledFrame.AddStrikePoints(rolledPins);
        }
    }


    /// <summary>
    /// Total score of the game
    /// </summary>
    public int Score { get
        {
            int totalScore = 0;
            foreach(Frame frame in Frames)
            {
                totalScore+=frame.Score;
            }
            return totalScore;
        } 
    }

    /// <summary>
    /// Whether game is completed.
    /// </summary>
    public bool IsGameFinished
    {
        get
        {
            return Frames.All(f => f.Finished);
        }
    }

    /// <summary>
    /// Returns the last last roll that was performed in the game.
    /// </summary>
    public Roll? LastRoll
    {
        get
        {
            return rolls.FirstOrDefault();
        }
    }
}
