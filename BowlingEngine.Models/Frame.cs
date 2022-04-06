namespace BowlingEngine.Models;

/// <summary>
/// Model for one frame.
/// </summary>
public class Frame
{
    private static int PIN_COUNT = 10;

    private int? StrikeBonus;
    private int? SpareBonus;

    /// <summary>
    /// No-arg constructor.
    /// </summary>
    public Frame(int RollCount)
    {
        Rolls = new int?[RollCount];
    }

    public void Roll(int pins)
    {
        for (int i = 0; i < Rolls.Length; i++)
        {
            if (Rolls[i].HasValue) continue;
            Rolls[i] = pins;
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
            return Rolls.Sum().Value + StrikeBonus.GetValueOrDefault() + SpareBonus.GetValueOrDefault();
        }
    }

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
                int remainingPins = PIN_COUNT;
                foreach(var pin in Rolls)
                {
                    if (pin.HasValue)
                    {
                        remainingPins -= pin.Value;
                    }
                }
                return remainingPins;
            }
        }
    }


    public bool Finished
    {
        get
        {
            if(Rolls.All(x => x.HasValue))
            {
                return true;
            }
            else if(Rolls.First().GetValueOrDefault() == PIN_COUNT)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void AddStrikePoints(int pins)
    {
        if (StrikeBonus == null)
        {
            StrikeBonus = pins;
        }
        else
        {
            StrikeBonus += pins;
        }
    }

    public void AddSparePoints(int pins)
    {
        if(SpareBonus == null)
        {
            SpareBonus = pins;
        }
    }

    public bool Strike
    {
        get => Rolls[0].HasValue && Rolls[0] == PIN_COUNT;
    }

    public bool Spare
    {
        get => (!Strike && Rolls.Sum() == PIN_COUNT);
    }
}
