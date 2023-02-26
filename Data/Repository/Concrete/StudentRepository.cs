using System;
using Core.Entities;
using Data.Contexts;
using Data.Repository.Abstract;

namespace Data.Repository.Concrete
{
    public class StudentRepository : IStudentRepository
    {

        public List<Student> GetAll()
        {
            return DbContent.Students;
        }

        public Student Get(int id)
        {
            return DbContent.Students.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Student student)
        {
            DbContent.Students.Add(student);
        }

        public void Update(Student student)
        {
            var dbStudent = DbContent.Students.FirstOrDefault(s => s.Id == student.Id);
            if (dbStudent is not null)
            {
                dbStudent.Name = student.Name;
                dbStudent.Surname = student.Surname;
                dbStudent.BirthDate = student.BirthDate;
                dbStudent.Group = student.Group;
                dbStudent.GroupId = student.GroupId;
            }
        }

        public void Delete(Student student)
        {
            DbContent.Students.Remove(student);
        }

        public bool IsDublicateEmail(string email)
        {
            return DbContent.Students.Any(s => s.Email == email);
        }
    }
}

