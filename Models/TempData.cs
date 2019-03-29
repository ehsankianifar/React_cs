using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TempData
    {
        public int Id { get; set; }
        public DateTime ReadingDateTime { get; set; }
        public string AssetName { get; set; }
        public string DeviceName { get; set; }
        public double ReadingData { get; set; }
        public bool IsSent { get; set; }
    }

    public class NewAction
    {
        public int Id { get; set; }
        public string ActionDateTime { get; set; }
        public string ActinoName { get; set; }

    }

    public class NewAction2
    {
        public string ActionDateTime { get; set; }
        public string ActionName { get; set; }

    }

    public class TemperatureContext : DbContext
    {
        public TemperatureContext(DbContextOptions<TemperatureContext> options) : base(options)
        {

        }
        public DbSet<TempData> tempDatas { get; set; }
        public DbSet<NewAction> NewActions { get; set; }
    }
}
