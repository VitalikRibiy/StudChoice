using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using StudChoice.DAL.EF;
using StudChoice.DAL.UnitOfWork;

namespace StudChoice.BLL.Infrastructure
{
   public class ServiceModule : NinjectModule
    {
        private readonly DbContextOptions<StudChoiceContext> options;

        public ServiceModule(DbContextOptions<StudChoiceContext> connection)
        {
            options = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(options);
        }
    }
}
