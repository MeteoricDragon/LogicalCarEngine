using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class CamShaft : MechanicalPart
    {
        public override string UserFriendlyName { get => "Camshaft"; }
        public CamShaft(Engine e) : base(e)
        {
        }

        // if camshaft is activating valves, shouldn't transfer momentum.  
        // if I transfer momentum, when units get added in the valve, it will report as fuel being added.
        // That would work for fuel pump sending fuel, but not for camshaft opening valve
    }
}