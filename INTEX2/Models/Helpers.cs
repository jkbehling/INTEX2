using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace INTEX2.Models
{
    public class Helpers
    {
        public static string GetRDSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = appConfig["ebdb"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = appConfig["admin"];
            string password = appConfig["Ican'twaittofinishintexandthecore!$$$"];
            string hostname = appConfig["aage4mnx7cezcn.c1dtnhbcknoc.us-east-1.rds.amazonaws.com"];
            string port = appConfig["3306"];

            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}