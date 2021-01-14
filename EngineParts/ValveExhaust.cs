using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChamber;

namespace LogicalEngine.EngineParts
{
    public class ValveExhaust : CarPart, IValve
    {
        public override string UserFriendlyName { get => "Exhaust Valve"; }
        public bool IsOpen { 
            get {
                return ((Engine as CombustionEngine).Chamber.StrokeCycle == CombustionStrokeCycle.Exhaust);
            } 
        }

        public ValveExhaust(Engine e) : base(e)
        {
            Engine = e;
        }
    }
}