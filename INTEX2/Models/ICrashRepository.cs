using INTEX2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models
{
    //ICrashRepository 
    public interface ICrashRepository
    {
        //In order to get data
        IQueryable<Crash> crashdata { get; }

        //Save, create, update, delete functionality
        public void SaveCrash(Crash c);
        public void CreateCrash(Crash c);
        public void UpdateCrash(Crash c);
        public void DeleteCrash(Crash c);

    }
}
