namespace BowlingEngine;

/// <summary>
/// Model for one frame.
/// </summary>
public class Frame
{
    public const int PIN_COUNT = 10;

    private int StrikeBonus = 0;
    private int? SpareBonus;

    /// <summary>
    /// No-arg constructor.
    /// </summary>
    public Frame(int RollCount, bool Final)
    {
        Rolls = new int?[RollCount];
        FinalFrame = Final;
    }

    public void Roll(Roll r)
    {
        for (int i = 0; i < Rolls.Length; i++)
        {
            if (Rolls[i].HasValue) continue;
            Rolls[i] = r.PinsHit;
            break;
        }
    }

    public int?[] Rolls { get; private set; }

    /// <summary>
    /// Total score for the frame.
    /// </summary>
    public int Score
    {
        get
        {
            return Rolls.Sum().GetValueOrDefault() + StrikeBonus + SpareBonus.GetValueOrDefault();
        }
    }

    /// <summary>
    /// Whether this frame is the final frame of the game and special logic will be applied.
    /// </summary>
    public bool FinalFrame { get; private set; }


    /// <summary>
    /// The number of pins left to be hit.
    /// </summary>
    public int RemainingPins
    {
        get
        {
            if (Finished)
            {
                return 0;
            }
            else
            {
                if (!FinalFrame)
                {
                    return PIN_COUNT - Rolls.AsQueryable().Sum().GetValueOrDefault();
                }
                else
                {
                    //If it's the first roll of the final frame, there will be ten more pins
                    if (Rolls.All(r => !r.HasValue))
                    {
                        return PIN_COUNT;
                    }
                    else
                    //If the most recent roll is a strike, there will be ten more pins
                    if (Rolls.Last(r => r.HasValue).GetValueOrDefault() == PIN_COUNT)
                    {
                        return PIN_COUNT;
                    }
                    else
                    {
                        //Case for when there was a strike the first roll of the final frame, and a spare for the last two.
                        return PIN_COUNT - Rolls.LastOrDefault(r => r.HasValue).GetValueOrDefault();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Whether the frame is finished.
    /// </summary>
    public bool Finished
    {
        get
        {
            if (Rolls.All(x => x.HasValue))
            {
                return true;
            }

            if (!FinalFrame)
            {
                if (Rolls.First().GetValueOrDefault() == PIN_COUNT)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                int emptyRollCount = Rolls.Where(x => !x.HasValue).Count();

                //If there was a spare or less in the first two rolls
                if (emptyRollCount == 1 && Rolls[0] < PIN_COUNT)
                {
                    return true;
                }
                //If there was a strike and then a spare
                return !(emptyRollCount > 0);
            }
        }
    }

    /// <summary>
    /// Add points from one of the next two rolls after having gotten a strike in this frame.
    /// </summary>
    /// <param name="pins"></param>
    public void AddStrikePoints(int pins)
    {
        StrikeBonus += pins;
    }

    /// <summary>
    /// Add extra points from the next roll after having gotten a spare in this frame.
    /// </summary>
    /// <param name="pins"></param>
    public void AddSparePoints(int pins)
    {
        if (SpareBonus == null)
        {
            SpareBonus = pins;
        }
    }
}
