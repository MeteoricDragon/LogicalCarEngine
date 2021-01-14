using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Pistons : MechanicalPart
    {
        public override string UserFriendlyName { get => "Pistons"; }
        public Pistons(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override void InvokeActivate()
        {
            if (Engine.CycleComplete == false)
                base.InvokeActivate();
        }
    }
}