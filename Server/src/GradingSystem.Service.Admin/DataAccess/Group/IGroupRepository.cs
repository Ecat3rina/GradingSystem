using GradingSystem.Service.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.Group
{
    public interface IGroupRepository
    {
        Task AddGroup(GroupModel model);
        Task<List<GroupModel>> GetGroups();
        Task<GroupModel> GetGroupById(Guid id);
        Task DeleteGroup(Guid id);
        Task UpdateGroup(GroupModel model);
    }
}
