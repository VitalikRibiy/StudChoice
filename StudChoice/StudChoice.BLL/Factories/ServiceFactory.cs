using StudChoice.BLL.Services.Interfaces;

namespace StudChoice.BLL.Factories
{
    public class ServiceFactory : IServiceFactory
    {
        public ServiceFactory(ISubjectService subjectService)
        {
            SubjectService = subjectService;
        }

        public ISubjectService SubjectService { get; }
    }
}
