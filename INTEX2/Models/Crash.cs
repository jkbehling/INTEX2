using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTEX2.Models
{
    public partial class Crash
    {
        //Model to connect to data in the database
        [Key]
        [Required]
        public int CRASH_ID { get; set; }
        [Required]
        public string CRASH_DATETIME { get; set; }

        public string ROUTE { get; set; }

        public float MILEPOINT { get; set; }

        public float LAT_UTM_Y { get; set; }

        public float LONG_UTM_X { get; set; }

        public string MAIN_ROAD_NAME { get; set; }
        [Required]
        public string CITY { get; set; }
        [Required]
        public string COUNTY_NAME { get; set; }
        [Required]
        public int CRASH_SEVERITY_ID { get; set; }

        public string WORK_ZONE_RELATED { get; set; }

        public string PEDESTRIAN_INVOLVED { get; set; }

        public string BICYCLIST_INVOLVED { get; set; }

        public string MOTORCYCLE_INVOLVED { get; set; }

        public string IMPROPER_RESTRAINT { get; set; }

        public string UNRESTRAINED { get; set; }

        public string DUI { get; set; }

        public string INTERSECTION_RELATED { get; set; }

        public string WILD_ANIMAL_RELATED { get; set; }

        public string DOMESTIC_ANIMAL_RELATED { get; set; }

        public string OVERTURN_ROLLOVER { get; set; }

        public string COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }

        public string TEENAGE_DRIVER_INVOLVED { get; set; }

        public string OLDER_DRIVER_INVOLVED { get; set; }

        public string NIGHT_DARK_CONDITION { get; set; }

        public string SINGLE_VEHICLE { get; set; }

        public string DISTRACTED_DRIVING { get; set; }

        public string DROWSY_DRIVING { get; set; }

        public string ROADWAY_DEPARTURE { get; set; }

        public string CRASH_TIME { get; set; }

        public string CRASH_DAY { get; set; }

        public string CRASH_DAYOFWEEK { get; set; }

        public string CRASH_YEAR { get; set; }

        public int CRASH_MONTH { get; set; }
    }
}
