using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using StudChoice.BLL.Services;
using StudChoice.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudChoice1.Util
{
    public class SubjectModule : NinjectModule
    {
        private DbContextOptions _options;
        public SubjectModule(DbContextOptions connection)
        {
            _options = connection;
        }
        public override void Load()
        {
            Bind<ISubjectService>().To<SubjectService>().WithConstructorArgument(_options);
        }
    }
}
