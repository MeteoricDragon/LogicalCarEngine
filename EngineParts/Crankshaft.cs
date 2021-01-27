using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;

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

        protected override bool ShouldActivate(CarPart activatingPart)
        {
            if (Engine.CycleComplete) 
                return false;         

            return true;
        }

        protected override bool BackToEngineLoop(CarPart sender)
        {
            if (sender is Pistons && Engine.CycleComplete)
                return true;
            return false;
        }

        public void Tick()
        {
            InvokeActivate();
        }
    }
}