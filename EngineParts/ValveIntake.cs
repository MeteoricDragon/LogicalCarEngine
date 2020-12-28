using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class ValveIntake : CarPart
    {
        public override string UserFriendlyName { get => "Intake Valve"; }
        public ValveIntake(Engine e) : base(e)
        {
        }
    }
}