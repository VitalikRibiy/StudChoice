using StudChoice.BLL.Services.Interfaces;

namespace StudChoice.BLL.Factories
{
    public class ServiceFactory : IServiceFactory
    {
        public ServiceFactory(
            ISubjectService subjectService,
            IFacultyService facultyService,
            IProfessorService professorService,
            ICathedraService cathedraService)
        {
            SubjectService = subjectService;
            FacultyService = facultyService;
            ProfessorService = professorService;
            CathedraService = cathedraService;
        }

        public ISubjectService SubjectService { get; }
        public IFacultyService FacultyService { get; }
        public IProfessorService ProfessorService { get; }
        public ICathedraService CathedraService { get; }
    }
}
