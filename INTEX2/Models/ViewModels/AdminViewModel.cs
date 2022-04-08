using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models.ViewModels
{
    public class AdminViewModel
    {
        public IQueryable<Crash> Crashes { get; set; }
        public Crash Crash { get; set; }
        public int crash_id { get; set; }
    }
}
