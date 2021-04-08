using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Flywheel : MechanicalPart
    {
        public override string UserFriendlyName { get => "Flywheel"; }
        public Flywheel(Engine e) : base(e)
        {
        }

        protected override bool ShouldActivate(CarPart target)
        {
            // TODO: activate Crankshaft if starting up, but torque converter if cycle completed (not yet)
            if (target is Crankshaft)
                return false;
            return true;
        }

        protected override bool PostTransferReturnToEngineLoop(CarPart sender)
        {
            return true;
        }
    }
}