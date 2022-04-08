using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models.ViewModels
{
    public class CrashesViewModel
    {
        //Crashes View Model 
        public IQueryable<Crash> crashdata { get; set; }
        public PageInfo PageInfo { get; set; }
        public int pageNum { get; set; }
    }
}
