using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
namespace Data.Repository.Abstract
{
	public interface IGroupRepository:IRepository<Group>
    {
        Group GetByName(string name);
    }
}

