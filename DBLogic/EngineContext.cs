using Microsoft.EntityFrameworkCore;
using LogicalEngine.DBLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.DBLogic
{
    public class EngineContext : DbContext
    {
        public virtual DbSet<CarPart> CarParts { get; set; }
        public virtual DbSet<Manufacturer> Manfuacturers { get; set; }

    }
}
