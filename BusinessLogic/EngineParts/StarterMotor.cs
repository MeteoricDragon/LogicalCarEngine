using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class StarterMotor : MechanicalPart
    {
        public override string UserFriendlyName { get => "Starter Motor"; }
        public StarterMotor(Engine e) : base(e)
        {
        }
    }
}