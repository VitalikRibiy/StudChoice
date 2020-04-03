using StudChoice.BLL.Services.Interfaces;

namespace StudChoice.BLL.Factories
{
    public class ServiceFactory : IServiceFactory
    {
        public ISubjectService SubjectService { get; }

        public ServiceFactory(ISubjectService subjectService)
        {
            SubjectService = subjectService;
        }
    }
}
