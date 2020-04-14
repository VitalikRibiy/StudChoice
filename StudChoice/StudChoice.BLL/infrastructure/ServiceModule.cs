using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using StudChoice.DAL.EF;
using StudChoice.DAL.UnitOfWork;

namespace StudChoice.BLL.infrastructure
{
    class ServiceModule : NinjectModule
    {
        private DbContextOptions<StudChoiceContext> _options;
        public ServiceModule(DbContextOptions<StudChoiceContext> connection)
        {
            _options = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_options);
        }
    }
}
