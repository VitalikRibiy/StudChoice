using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudChoice.DAL.EF
{
    class SampleData
    {
        public static void InitData(EFDBContext context)
        {
            if (!context.Subjects.Any())
            {
                context.Subjects.Add(new Models.Subject() { name = "First", description = "Description1", type="DVVS" });
                context.Subjects.Add(new Models.Subject() { name = "Second ", description = "Description2", type="DV"});
                context.SaveChanges();
            }
        }
    }
}
