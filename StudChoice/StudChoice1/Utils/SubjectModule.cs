using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using StudChoice.BLL.Services.Implementations;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudChoice.Utils
{
    public class SubjectModule : NinjectModule
    {
        private DbContextOptions<EFDBContext> _options;
        public SubjectModule(DbContextOptions<EFDBContext> connection)
        {
            _options = connection;
        }
        public override void Load()
        {
            Bind<ISubjectService>().To<SubjectService>().WithConstructorArgument(_options);
        }
    }
}
