using INTEX2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models
{
    public class EFCrashRepository : ICrashRepository
    {
        private CrashDbContext context { get; set; }

        public EFCrashRepository(CrashDbContext temp)
        {
            context = temp;
        }

        public IQueryable<Crash> crashdata => context.crashdata;

    public void SaveCrash(Crash c)
        {
            context.SaveChanges();
        }


        public void CreateCrash(Crash c)
        {
            context.Add(c);
            context.SaveChanges();
        }

        public void UpdateCrash(Crash c)
        {
            context.Update(c);
            context.SaveChanges();
        }

        public void DeleteCrash(Crash c)
        {
            context.crashdata.Remove(c);
            context.SaveChanges();
        }

    }
}
