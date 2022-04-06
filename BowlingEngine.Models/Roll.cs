using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingEngine.Models;

public class Roll
{
    public Roll(int RolledPins, Frame frame, Roll Previous)
    {
        PinsHit = RolledPins;
        RolledFrame = frame;
        PreviousRoll = Previous;
    }

    public int PinsHit { get; private set; }
    public Frame RolledFrame { get; private set; }
    public Roll PreviousRoll { get; private set; }

    public bool Strike
    {
        get
        {
            return PinsHit == Frame.PIN_COUNT;
        }
    }

    public bool Spare
    {
        get
        {
            if (RolledFrame.Equals(PreviousRoll?.RolledFrame) && (PinsHit + PreviousRoll?.PinsHit) == Frame.PIN_COUNT)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
