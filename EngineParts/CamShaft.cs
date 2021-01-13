using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class CamShaft : MechanicalPart
    {
        public override string UserFriendlyName { get => "Camshaft"; }
        public CamShaft(Engine e) : base(e)
        {
        }

    }
}