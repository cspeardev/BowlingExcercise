using BowlingEngine.Models;

namespace BowlingEngine;

public class BowlingGame
{
    private static Random r = new();
    public List<Frame> Frames { get; private set; }

    private const int FRAME_COUNT = 10;
    private const int FINAL_FRAME_ROLLS = 3;
    private const int REGULAR_FRAME_ROLLS = 2;

    public BowlingGame()
    {
        Frames = new List<Frame>();
        for (int i = 0; i < (FRAME_COUNT-1) ; i++)
        {
            Frames.Add(new(REGULAR_FRAME_ROLLS));
        };
        Frames.Add(new(FINAL_FRAME_ROLLS));
    }


    /// <summary>
    /// Perform a roll.
    /// </summary>
    public void Roll()
    {
        //Identify which frame to roll against

        var rollingFrame = Frames.FirstOrDefault(f => !f.Finished);

        if(rollingFrame != null)
        {
            int rolledPins = r.Next(0, (rollingFrame.RemainingPins+1));
            rollingFrame.Roll(rolledPins);
            int currentFrameIndex = Frames.FindIndex(f => f.Equals(rollingFrame));

            if (currentFrameIndex > 0 && (Frames[currentFrameIndex - 1].Strike || Frames[currentFrameIndex - 1].Spare))
            {
                Frames[currentFrameIndex - 1].AddStrikePoints(rolledPins);
            }
            else if(currentFrameIndex > 1 && Frames[currentFrameIndex - 2].Strike) {
                Frames[currentFrameIndex - 2].AddStrikePoints(rolledPins);
            }

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

    public bool IsGameFinished
    {
        get
        {
            return Frames.All(f => f.Finished);
        }
    }
}
