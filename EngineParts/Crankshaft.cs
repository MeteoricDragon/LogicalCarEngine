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

        protected override bool ShouldActivate(CarPart target, in bool transferSuccess)
        {
            if (Engine.CycleComplete)
                return false;

            // TODO: activate Flywheel if we're going to torque converter (not yet)
            return true;
        }

        protected override bool BackToEngineLoop(CarPart target)
        {// TODO: if doing Torque Converter, readdress how the engine 
            //goes back to loop? Will torque converter be triggered by other strokes?
            if (this is Crankshaft 
                //&& target is TimingChain 
                && Engine.CycleComplete) 
                return true;
            return false;
        }

        public void Tick()
        {
            InvokeActivate();
        }
    }
}