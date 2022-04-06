using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models.ViewModels
{
    public class SummaryViewModel
    {
        public IQueryable<Crash> Crashes { get; set; }
        public PageInfo PageInfo { get; set; }
        public int pageNum { get; set; }
        public int severity { get; set; }
        public string county { get; set; }
        public int crashid { get; set; }
        public string theRoute { get; set; }
        public string city { get; set; }
        public string workzone { get; set; }
        public float milepoint { get; set; }
        public string road { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string pedestrian { get; set; }
        public string bicyclist { get; set; }
        public string motorcycle { get; set; }
        public string improperrestraint { get; set; }
        public string unrestrained { get; set; }
        public string dui { get; set; }
        public string intersection { get; set; }
        public string wildanimal { get; set; }
        public string domesticanimal { get; set; }
        public string rollover { get; set; }
        public string commercial { get; set; }
        public string teenager { get; set; }
        public string older { get; set; }
        public string night { get;  set; }
        public string distracted { get;  set; }
        public string departure { get; set; }
        public string drowsy { get; set; }
        public string single { get; set; }
    }
}
