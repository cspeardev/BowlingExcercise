using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingEngine;

public class Roll
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="RolledPins"></param>
    /// <param name="frame"></param>
    /// <param name="Previous"></param>
    public Roll(int RolledPins, Frame frame, Roll? Previous)
    {
        PinsHit = RolledPins;
        RolledFrame = frame;
        PreviousRoll = Previous;
    }

    public int PinsHit { get; private set; }
    public Frame RolledFrame { get; private set; }
    public Roll? PreviousRoll { get; private set; }

    public bool Strike
    {
        get
        {
            return PinsHit == Frame.PIN_COUNT;
        }
    }


    public bool Spare
    {
        get => RolledFrame.Equals(PreviousRoll?.RolledFrame) && PinsHit + PreviousRoll?.PinsHit == Frame.PIN_COUNT;
    }
}
