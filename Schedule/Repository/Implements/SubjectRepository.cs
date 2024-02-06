using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.Data;
using Schedule.Models;
using Schedule.Repositories.Interfaces;
using Schedule.ViewModels;
using TrialFreelance.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Schedule.Repositories.Implements
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext dbCon;

        public SubjectRepository(ApplicationDbContext dbCon)
        {
            this.dbCon = dbCon;
        }

        public async void Add(Subject model)
        {
            await dbCon.Subjects.AddAsync(model);
            await dbCon.SaveChangesAsync();
        }

        public void Delete(Subject model)
        {
            throw new NotImplementedException();
        }

        public async Task<Subject> FindById(int id)
        {
            return await dbCon.Subjects.SingleOrDefaultAsync(p => p.id_subject == id);
        }

        public IEnumerable<Subject> GetAll()
        {
            throw new NotImplementedException();
        }


        public void Update(Subject model)
        {
            throw new NotImplementedException();
        }

        int IGenericRepository<Subject>.Add(Subject model)
        {
            throw new NotImplementedException();
        }

        Subject ISubjectRepository.FindById(int id)
        {
            throw new NotImplementedException();
        }

        Subject IGenericRepository<Subject>.FindById(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Subject> ISubjectRepository.GetAllByGroupId(int id)
        {
            throw new NotImplementedException();
        }
    }

}
