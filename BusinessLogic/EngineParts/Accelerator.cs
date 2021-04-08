using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Accelerator : FuelPart
    {
        public override string UserFriendlyName { get => "Accelerator"; }
        public Accelerator(Engine e) : base(e)
        { 
        }
    }
}