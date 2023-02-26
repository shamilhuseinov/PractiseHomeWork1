using System;
using Core.Entities;
using Data.Contexts;
using Data.Repository.Abstract;

namespace Data.Repository.Concrete
{
    public class TeacherRepository : ITeacherRepository
    {
        static int id;

        public List<Teacher> GetAll()
        {
            return DbContent.Teachers;
        }

        public Teacher Get(int id)
        {
            return DbContent.Teachers.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Teacher teacher)
        {
            id++;
            DbContent.Teachers.Add(teacher);
            teacher.Id = id;
        }

        public void Update(Teacher teacher)
        {
            var dbTeacher = DbContent.Teachers.FirstOrDefault(s => s.Id == teacher.Id);
            if (dbTeacher is not null)
            {
                dbTeacher.Name = teacher.Name;
                dbTeacher.Surname = teacher.Surname;
                dbTeacher.BirthDate = teacher.BirthDate;
                dbTeacher.Speciality = teacher.Speciality;
            }
        }

        public void Delete(Teacher teacher)
        {
            DbContent.Teachers.Remove(teacher);
        }
    }
}

