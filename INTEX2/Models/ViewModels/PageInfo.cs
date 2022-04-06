using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumCrashes { get; set; }
        public int CrashesPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int SeverityFilter { get; set; }
        public string CountyFilter { get; set; }
        public string RouteFilter { get; set; }
        public string CityFilter { get; set; }
        public string WorkZoneFilter { get; set; }
        public string MilePointFilter { get; set; }
        public string RoadFilter { get; set; }
        public float LatitudeFilter { get; set; }
        public float LongitudeFilter { get; set; }
        public string PedestrianFilter { get; set; }
        public string BicyclistFilter { get; set; }
        public string MotorcycleFilter { get; set; }
        public string ImpropperRestraintFilter { get; set; }
        public string UnrestrainedFilter { get; set; }
        public string DUIFilter { get; set; }
        public string IntersectionFilter { get; set; }
        public string WildAnimalFilter { get; set; }
        public string DomesticAnimalFilter { get; set; }
        public string RolloverFilter { get; set; }
        public string CommercialFilter { get; set; }
        public string TeenagerFilter { get; set; }
        public string OlderFilter { get; set; }
        public string NightFilter { get; set; }
        public string SingleFilter { get; set; }
        public string DistractedFilter { get; set; }
        public string DepartureFilter { get; set; }
        public string DrowsyFilter { get; set; }

        //Get number of pages
        public int TotalPages => (int)Math.Ceiling((double)TotalNumCrashes / CrashesPerPage);
    }
}
