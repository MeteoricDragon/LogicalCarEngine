using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Crankshaft : MechanicalPart
    {
        public override string UserFriendlyName { get => "Crankshaft"; }
        public override int UnitsToGive { get => 15; }
        public override int UnitsMax { get => 50; }
        public Crankshaft(Engine e) : base(e)
        {
            Engine = e;
            UnitsOwned = 5;
            FrictionResistance = 0;
        }

        protected override bool TryActivate( CarPart activatingPart)
        {
            if (activatingPart is Flywheel)
            {
                (Engine as CombustionEngine).StrokeCycleChange(CombustionEngine.CombustionStrokeCycle.Intake);
            }

            if (UnitsOwned >= UnitTriggerThreshold)
                return base.TryActivate(activatingPart);
            return false;
        }
    }
}