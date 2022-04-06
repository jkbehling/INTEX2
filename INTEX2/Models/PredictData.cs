﻿using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models
{
    public class PredictData
    {
        
        public float crash_time { get; set; }

        public float crash_dayofweek { get; set; }

        public float crash_year { get; set; }

        public float crash_month { get; set; }

        public float work_zone_related_True { get; set; }

        public float pedestrian_involved_True { get; set; }

        public float bicyclist_involved_True { get; set; }

        public float motorcycle_involved_True { get; set; }
        public float improper_restraint_True { get; set; }
        public float unrestrained_True { get; set; }
        public float dui_True { get; set; }
        public float intersection_related_True { get; set; }
        public float wild_animal_related_True { get; set; }
        public float domestic_animal_related_True { get; set; }
        public float overturn_rollover_True { get; set; }
        public float commercial_motor_veh_involved_True { get; set; }
        public float teenage_driver_involved_True { get; set; }
        public float older_driver_involved_True { get; set; }
        public float night_dark_condition_True { get; set; }
        public float single_vehicle_True { get; set; }
        public float distracted_driving_True { get; set; }
        public float drowsy_driving_True { get; set; }
        public float roadway_departure_True { get; set; }


        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                crash_time, crash_dayofweek, crash_year, crash_month, 
                work_zone_related_True, pedestrian_involved_True, bicyclist_involved_True, 
                motorcycle_involved_True, improper_restraint_True, unrestrained_True, dui_True, 
                intersection_related_True, wild_animal_related_True, domestic_animal_related_True, 
                overturn_rollover_True, commercial_motor_veh_involved_True, teenage_driver_involved_True, 
                older_driver_involved_True, night_dark_condition_True, single_vehicle_True, distracted_driving_True, 
                drowsy_driving_True, roadway_departure_True
            };
            int[] dimensions = new int[] { 1, 23 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}
