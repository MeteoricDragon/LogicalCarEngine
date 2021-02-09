using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.Cylinders;

namespace LogicalEngine.EngineParts
{
    public class Pistons : MechanicalPart
    {
        public override string UserFriendlyName { get => "Pistons"; }
        public Pistons(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override void RefreshEngineStage(CarPart sender)
        {
            var CE = Engine as CombustionEngine;
            CE.Chamber.NextStroke();
        }
    }
}