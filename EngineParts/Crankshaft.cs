using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.Cylinders;

namespace LogicalEngine.EngineParts
{
    public class Crankshaft : MechanicalPart
    {
        public override string UserFriendlyName { get => "Crankshaft"; }

        public Crankshaft(Engine e) : base(e)
        {
            Engine = e;
            UnitsOwned = 5;
            FrictionResistance = 0;
        }

        protected override bool ShouldActivate(CarPart target)
        {
            if (Engine.CycleComplete)
                return false;

            // TODO: activate Flywheel if we're going to torque converter (not yet)
            return base.ShouldActivate(target);
        }

        protected override bool PreTransferReturnToEngineLoop(CarPart target)
        {// TODO: if doing Torque Converter, readdress how the engine 
            //goes back to loop? Will torque converter be triggered by other strokes?
            if (Engine.CycleComplete || (Engine as CombustionEngine).Ignition.StartupOn )
                return true;
            return false;
        }
    }
}