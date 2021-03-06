using INTEX2.Models;
using INTEX2.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        //Grab data from the crash repository
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

        //all of these parameters are used for filtering the data
        public IActionResult Summary(int pageNum = 1, int severity = 0, string county = null, string theRoute = null, string city = null, int month   =   0, string year   =   null, string workzone = null, float milepoint   =   0,
            string road = null, float latitude = 0, float longitude = 0, string pedestrian = null, string bicyclist = null, string motorcycle = null,
            string improperrestraint = null, string unrestrained = null, string dui = null, string intersection = null, string wildanimal = null, string domesticanimal = null, string rollover = null,
            string commercial = null, string teenager = null, string older = null, string night = null, string single = null, string distracted = null, string departure = null, string drowsy = null)
        {

            // Set the page size here
            int pageSize = 40;

            //These are used for the drop down lists.
            ViewBag.Counties = repo.crashdata.Select(x => x.COUNTY_NAME).Distinct();
            ViewBag.Severity = repo.crashdata.Select(x => x.CRASH_SEVERITY_ID).Distinct();
            ViewBag.Years = repo.crashdata.Select(x => x.CRASH_YEAR).Distinct();

            //This will filter the data unless the data entered in is null or 0
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


            //This creates a new view model for the summary page
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

        //This is for when the user searches by crashid on the summary page
        public IActionResult SummaryCrashId(int crashid, int pageNum = 1)
        {
            int pageSize = 50;
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

        //For the crash severity predictor
        public IActionResult Predictor()
        {
            return View();
        }

        //For details on a single record
        public IActionResult Details(int id)
        {
            var x = repo.crashdata.Single(x => x.CRASH_ID == id);
            return View(x);
        }

        //This is the admin page where you can CRUD records
        [Authorize]
        public IActionResult Admin(LoginModel loginModel)
        {
            var x = repo.crashdata;

            var y = new AdminViewModel
            {
                Crashes = x,
                crash_id = 0
            };

            return View(y);
        }

        //Details on a record for admin pages
        [Authorize]
        [HttpPost]
        public IActionResult AdminDetails(int crash_id)
        {
            Crash x = repo.crashdata.FirstOrDefault(x => x.CRASH_ID == crash_id);
            if(x != null)
            {
                return View(x);
            }
            else
            {
                ViewBag.ErrorMessage = "This ID doesn't exist.";
                return View("Admin");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult AdminCreate()
        {
            Crash crash = new Crash();
            return View(crash);
        }

        [HttpPost]
        public IActionResult AdminCreate(Crash c)
        {
            if (ModelState.IsValid)
            {
                //Autoincriments the Crash_id
                c.CRASH_ID = (repo.crashdata.Max(c => c.CRASH_ID))+1;
                c.CRASH_DATETIME = c.CRASH_YEAR + "-" + c.CRASH_MONTH + "-" + c.CRASH_DAY + "T" + c.CRASH_TIME;
                repo.CreateCrash(c);

                return View("Admin");
            }
            else
            {
                return View(c);
            }
        }

        public IActionResult AdminEdit()
        {
            int id = Convert.ToInt32(RouteData.Values["id"]);
            Crash y = repo.crashdata.Single(x => x.CRASH_ID == id);
            return View("AdminCreate", y);
        }

        public IActionResult AdminUpdate(Crash c)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateCrash(c);

                return View("Admin");
            }
            else
            {
                return View(c);
            }
        }

        //Admin stuff for deletion
        [HttpGet]
        public IActionResult AdminDelete()
        {

            int id = Convert.ToInt32(RouteData.Values["id"]);
            Crash y = repo.crashdata.Single(x => x.CRASH_ID == id);

            return View("ConfirmDelete", y);
        }

        [HttpPost]
        public IActionResult AdminDelete(Crash c)
        {

            repo.DeleteCrash(c);

            return View("Admin");
        }

        public IActionResult ConfirmDelete(Crash c)
        {
            return View(c);
        }

        public IActionResult MoreInfo()
        {
            return View();
        }

        
    }
}