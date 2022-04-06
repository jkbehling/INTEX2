using INTEX2.Models;
using INTEX2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Controllers
{

    public class HomeController : Controller
    {
        private ICrashRepository repo;

        public HomeController(ICrashRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Summary(int pageNum  =  1, int severity  =  0, string county  =  null, string theRoute  =  null, string city  =  null, int month=0, string year=null, string workzone  =  null, float milepoint=0,
            string road  =  null, float latitude  =  0, float longitude  =  0, string pedestrian  =  null, string bicyclist  =  null, string motorcycle  =  null,
            string improperrestraint  =  null, string unrestrained  =  null, string dui  =  null, string intersection  =  null, string wildanimal  =  null, string domesticanimal  =  null, string rollover  =  null,
            string commercial  =  null, string teenager  =  null, string older  =  null, string night  =  null, string single  =  null, string distracted  =  null, string departure  =  null, string drowsy  =  null)
        {
            
            
            int pageSize = 20;
            ViewBag.Counties = repo.crashdata.Select(x => x.COUNTY_NAME).Distinct();
            ViewBag.Severity = repo.crashdata.Select(x => x.CRASH_SEVERITY_ID).Distinct();
            ViewBag.Years = repo.crashdata.Select(x => x.CRASH_YEAR).Distinct();

            var currentCrashes = repo.crashdata
                .Where(x => x.CRASH_SEVERITY_ID == severity || severity == 0)
                .Where(x => x.COUNTY_NAME == county || county == null)
                .Where(x => x.ROUTE == theRoute || theRoute == null)
                .Where(x => x.CITY == city || city == null)
                .Where(x => x.CRASH_MONTH == month || month == 0)
                .Where(x => x.CRASH_YEAR == year || year == null)
                .Where(x => x.WORK_ZONE_RELATED == workzone || workzone == null)
                .Where(x => x.MILEPOINT == milepoint || milepoint == 0)
                .Where(x => x.MAIN_ROAD_NAME == road || road == null)
                .Where(x => x.LAT_UTM_Y == latitude || latitude == 0)
                .Where(x => x.LONG_UTM_X == longitude || longitude == 0)
                .Where(x => x.PEDESTRIAN_INVOLVED == pedestrian || pedestrian == null)
                .Where(x => x.BICYCLIST_INVOLVED == bicyclist || bicyclist == null)
                .Where(x => x.MOTORCYCLE_INVOLVED == motorcycle || motorcycle == null)
                .Where(x => x.IMPROPER_RESTRAINT == improperrestraint || improperrestraint == null)
                .Where(x => x.UNRESTRAINED == unrestrained || unrestrained == null)
                .Where(x => x.DUI == dui || dui == null)
                .Where(x => x.INTERSECTION_RELATED == intersection || intersection == null)
                .Where(x => x.WILD_ANIMAL_RELATED == wildanimal || wildanimal == null)
                .Where(x => x.DOMESTIC_ANIMAL_RELATED == domesticanimal || domesticanimal == null)
                .Where(x => x.OVERTURN_ROLLOVER == rollover || rollover == null)
                .Where(x => x.COMMERCIAL_MOTOR_VEH_INVOLVED == commercial || commercial == null)
                .Where(x => x.TEENAGE_DRIVER_INVOLVED == teenager || teenager == null)
                .Where(x => x.OLDER_DRIVER_INVOLVED == older || older == null)
                .Where(x => x.NIGHT_DARK_CONDITION == night || night == null)
                .Where(x => x.SINGLE_VEHICLE == single || single == null)
                .Where(x => x.DISTRACTED_DRIVING == distracted || distracted == null)
                .Where(x => x.DROWSY_DRIVING == drowsy || drowsy == null)
                .Where(x => x.ROADWAY_DEPARTURE == departure || departure == null)
                .OrderBy(x => x.CRASH_ID);


            var x = new SummaryViewModel
            {
                Crashes = currentCrashes.Skip((pageNum - 1) * pageSize).Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes = currentCrashes.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum,
                    SeverityFilter = severity,
                    CountyFilter = county,
                    RouteFilter = theRoute,
                    CityFilter = city,
                    MonthFilter = month,
                    YearFilter = year,
                    WorkZoneFilter = workzone,
                    MilePointFilter = milepoint,
                    RoadFilter = road,
                    LatitudeFilter = latitude,
                    LongitudeFilter = longitude,
                    PedestrianFilter = pedestrian,
                    BicyclistFilter = bicyclist,
                    MotorcycleFilter = motorcycle,
                    ImpropperRestraintFilter = improperrestraint,
                    UnrestrainedFilter = unrestrained,
                    DUIFilter = dui,
                    IntersectionFilter = intersection,
                    WildAnimalFilter = wildanimal,
                    DomesticAnimalFilter = domesticanimal,
                    RolloverFilter = rollover,
                    CommercialFilter = commercial,
                    TeenagerFilter = teenager,
                    OlderFilter = older,
                    NightFilter = night,
                    SingleFilter = single,
                    DistractedFilter = distracted,
                    DrowsyFilter = drowsy,
                    DepartureFilter = departure
                },

                //for the form
                pageNum = pageNum,
                county = county,
                theRoute = theRoute

            };


            return View(x);
        }

        public IActionResult SummaryCrashId(int crashid, int pageNum  =  1)
        {
            int pageSize = 20;
            ViewBag.Counties = repo.crashdata.Select(x => x.COUNTY_NAME).Distinct();
            ViewBag.Severity = repo.crashdata.Select(x => x.CRASH_SEVERITY_ID).Distinct();
            var currentCrashes = repo.crashdata.Where(x => x.CRASH_ID == crashid);

            var x = new SummaryViewModel
            {
                Crashes = currentCrashes.Skip((pageNum - 1) * pageSize).Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes = currentCrashes.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum,

                },

                //for the form
                pageNum = pageNum,


            };


            return View("Summary", x);
        }

        public IActionResult Predictor()
        {
            return View();
        }
    }
}
