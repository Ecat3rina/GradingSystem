using GradingSystem.Service.Admin.ViewModels.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.Group
{
    public interface IGroupStorageService
    {
        Task<Guid> AddNewGroup(NewGroupViewModel model);
        Task<List<GroupViewModel>> GetGroups();
        Task<GroupViewModel> GetGroup(Guid id);
        Task<Guid> DeleteGroup(Guid id);
        Task<Guid> UpdateGroup(UpdateGroupViewModel model);
    }
}
