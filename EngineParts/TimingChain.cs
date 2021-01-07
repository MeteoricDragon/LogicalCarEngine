using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class TimingChain : MechanicalPart
    {
        public override int UnitsToGive { get => 20; }
        public override string UserFriendlyName { get => "Timing Chain"; }
        public TimingChain(Engine e) : base(e)
        {
        }
    }
}