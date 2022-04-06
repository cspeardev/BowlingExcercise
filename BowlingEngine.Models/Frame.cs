﻿namespace BowlingEngine.Models;

/// <summary>
/// Model for one frame.
/// </summary>
public class Frame
{
    public const int PIN_COUNT = 10;

    private int StrikeBonus = 0;
    private int? SpareBonus;
    private int StrikeBonuses = 0;
    

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
            return Rolls.Sum().Value + StrikeBonus + SpareBonus.GetValueOrDefault();
        }
    }

    public bool FinalFrame { get; private set; }

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

                    int remainingPins = PIN_COUNT;
                    foreach (var pin in Rolls)
                    {
                        if (pin.HasValue)
                        {
                            remainingPins -= pin.Value;
                        }
                    }
                    return remainingPins;
                }
                else
                {
                    //If it's the first roll of the final frame, there will be ten more pins
                    if(Rolls.All(r => !r.HasValue))
                    {
                        return PIN_COUNT;
                    }else
                    //If the most recent roll is a strike, there will be ten more pins
                    if (Rolls.Last(r => r.HasValue).GetValueOrDefault() == PIN_COUNT)
                    {
                        return PIN_COUNT;
                    }
                    else
                    {
                        //Case for when there was a strike the first roll of the final frame, and a spare for the last two.
                        int remainingPins = PIN_COUNT;
                        remainingPins -= Rolls.LastOrDefault(r => r.HasValue).Value;
                        return remainingPins;
                    }
                }
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
                if(emptyRollCount == 1 && Rolls[0] < PIN_COUNT)
                {
                    return true;
                }
                //If there was a strike and then a spare
                return !(emptyRollCount > 0);
            }
        }
    }

    public void AddStrikePoints(int pins)
    {
        StrikeBonus += pins;
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
