using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class Carburetor : CarPart
    {
        public override string UserFriendlyName { get => "Carburetor"; }
        public Carburetor(Engine e) : base(e)
        {
        }
    }
}