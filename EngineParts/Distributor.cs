using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Distributor : ElectricalPart
    {
        public override string UserFriendlyName { get => "Distributor"; }
        public Distributor(Engine e) : base(e)
        {
        }
        protected override bool ActivateNext(CarPart part)
        {
            if (part is SparkPlugs)
                return true;
            return false;
        }
    }
}