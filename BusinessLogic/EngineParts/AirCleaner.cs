using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class AirCleaner : CarPart
    {
        public override string UserFriendlyName { get => "Air Cleaner"; }
        public AirCleaner(Engine e) : base(e)
        {
        }
    }
}