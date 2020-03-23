using Ninject.Modules;
using StudChoice.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
