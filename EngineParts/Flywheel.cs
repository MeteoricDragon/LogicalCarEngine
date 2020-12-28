using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Flywheel : MechanicalPart
    {
        public override string UserFriendlyName { get => "Flywheel"; }
        public Flywheel(Engine e) : base(e)
        {
        }
    }
}