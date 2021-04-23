using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicalEngine.DBLogic.Models
{
    public class CarPart
    {
        public string PartName { get; set; }
        public int Units { get; set; }
        public int ManufacturerID { get; set; }
    }
}
