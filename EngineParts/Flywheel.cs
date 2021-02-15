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
            if (Engine.CycleComplete)
                return false;
            // TODO: activate Crankshaft if starting up, but torque converter if cycle completed (not yet)
            return true;
        }
    }
}