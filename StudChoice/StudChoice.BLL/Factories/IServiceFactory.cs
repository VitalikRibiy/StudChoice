using StudChoice.BLL.Services.Interfaces;

namespace StudChoice.BLL.Factories
{
    public interface IServiceFactory
    {
        ISubjectService SubjectService { get; }
        IFacultyService FacultyService { get; }
        IProfessorService ProfessorService { get; }
        ICathedraService CathedraService { get; }
    }
}
