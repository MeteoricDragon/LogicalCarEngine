using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class ValveExhaust : CarPart
    {
        public override string UserFriendlyName { get => "Exhaust Valve"; }
        public ValveExhaust(Engine e) : base(e)
        {
        }
    }
}