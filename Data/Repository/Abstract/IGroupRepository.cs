using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
namespace Data.Repository.Abstract
{
	public interface IGroupRepository
    { 
        List<Group> GetAll();
        Group Get(int id);
        Group GetByName(string name);
        void Add(Group group);
        void Update(Group group);
        void Delete(Group group);
    }
}

