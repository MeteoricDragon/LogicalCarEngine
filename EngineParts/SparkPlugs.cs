using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class SparkPlugs : ElectricalPart
    {
        public override string UserFriendlyName { get => "Spark Plugs"; }
        public SparkPlugs(Engine e) : base(e)
        {          
        }
    }
}