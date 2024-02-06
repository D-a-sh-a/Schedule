using System.Collections.Generic;
using Schedule.Entities;
using Schedule.Models;
using Schedule.Repositories.Interfaces;
using Schedule.ViewModels;

namespace TrialFreelance.Repositories.Interfaces
{
    public interface ISubjectRepository:IGenericRepository<Subject>
    {
        Subject FindById(int id);
        IEnumerable<Subject> GetAll();

        IEnumerable<Subject> GetAllByGroupId(int id);
    }
}
