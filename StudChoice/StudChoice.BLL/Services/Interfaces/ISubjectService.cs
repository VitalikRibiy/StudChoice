using StudChoice.BLL.DTOs;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace StudChoice.BLL.Services.Interfaces
{
    public interface ISubjectService : ICrudService<SubjectDTO>
    {
         Task updateState(long id);
    }
}
