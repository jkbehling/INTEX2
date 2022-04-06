using INTEX2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models
{
    public interface ICrashRepository
    {
        IQueryable<Crash> crashdata { get; }

        public void SaveCrash(Crash c);
        public void CreateCrash(Crash c);
        public void UpdateCrash(Crash c);
        public void DeleteCrash(Crash c);
    }
}
