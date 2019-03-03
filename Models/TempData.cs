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
        public string Temperature { get; set; }
        public string Humidity { get; set; }
    }

    public class TemperatureContext : DbContext
    {
        public TemperatureContext(DbContextOptions<TemperatureContext> options) : base(options)
        {

        }
        public DbSet<TempData> tempDatas { get; set; }
    }
}
