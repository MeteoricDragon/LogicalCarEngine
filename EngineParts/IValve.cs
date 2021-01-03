using LogicalEngine;
using LogicalEngine.EngineParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public interface IValve
    {
        public bool IsOpen { get; set; }
    }
}
