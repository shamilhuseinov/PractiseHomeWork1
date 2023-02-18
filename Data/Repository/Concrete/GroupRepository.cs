using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository.Abstract;
using Data.Contexts;

namespace Data.Repository.Concrete
{
    public class GroupRepository : IGroupRepository
    {
        static int id;

        public List<Group> GetAll()
        {
            return DbContent.Groups;
        }

        public Group Get(int id)
        {
            return DbContent.Groups.FirstOrDefault(g => g.Id == id);
        }

        public Group GetByName(string name)
        {
            return DbContent.Groups.FirstOrDefault(g => g.Name.ToLower() == name.ToLower());
        }

        public void Add(Group group)
        {
            id++;
            group.Id=id;
            group.CreatedAt = DateTime.Now;
            DbContent.Groups.Add(group);
        }

        public void Update(Group group)
        {
            var dbGroup = DbContent.Groups.FirstOrDefault(g => g.Id == group.Id);
            if (dbGroup is not null)
            {
                dbGroup.Name = group.Name;
                dbGroup.MaxSize = group.MaxSize;
                dbGroup.StartDate = group.StartDate;
                dbGroup.EndDate = group.EndDate;
                dbGroup.ModifiedAt = DateTime.Now;
            }
        }

        public void Delete(Group group)
        {
            DbContent.Groups.Remove(group);
        }
    }
}

