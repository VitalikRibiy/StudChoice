using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using StudChoice.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private DbContextOptions _options;
        public ServiceModule(DbContextOptions connection)
        {
            _options = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_options);
        }
    }
}
