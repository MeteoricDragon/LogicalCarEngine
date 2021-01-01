using LogicalEngine;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class CombustionChamber : FuelPart
    {
        public override string UserFriendlyName { get => "Combustion Chamber"; }
        public CombustionChamber(Engine e) : base(e)
        {
            Engine = e;
        }
    }
}
