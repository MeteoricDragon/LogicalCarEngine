using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;

namespace LogicalEngine.EngineParts
{
    public class Distributor : ElectricalPart
    {
        public override string UserFriendlyName { get => "Distributor"; }

        public override int UnitTriggerThreshold { get => 5; }
        public Distributor(Engine e) : base(e)
        {
        }

        protected override bool CanFill(UnitContainer givingPart)
        {
            if (givingPart is CamShaft)
                return false;
            return true;
        }
    }
}