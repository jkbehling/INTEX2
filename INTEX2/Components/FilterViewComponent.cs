using INTEX2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Components
{
    public class FilterViewComponent : ViewComponent
    {
        private ICrashRepository repo { get; set; }

        public FilterViewComponent(ICrashRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            var counties = repo.crashdata
                .Select(x => x.COUNTY_NAME)
                .Distinct()
                .OrderBy(x => x);

            return View(counties);
        }
    }
}