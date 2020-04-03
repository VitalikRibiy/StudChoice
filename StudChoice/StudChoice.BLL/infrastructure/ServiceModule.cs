using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using StudChoice.DAL.EF;
using StudChoice.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.infrastructure
{
    class ServiceModule : NinjectModule
    {
        private DbContextOptions<EFDBContext> _options;
        public ServiceModule(DbContextOptions<EFDBContext> connection)
        {
            _options = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_options);
        }
    }
}
