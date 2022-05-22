using GradingSystem.Service.Admin.DataAccess.Group;
using GradingSystem.Service.Admin.Models;
using GradingSystem.Service.Admin.ViewModels.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Services.Group
{
    public class GroupStorageService : IGroupStorageService
    {
        private readonly IGroupRepository _groupRepository;
        public GroupStorageService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<Guid> AddNewGroup(NewGroupViewModel model)
        {
            var groupModel = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name=model.Name,
                Description=model.Description
            };
            await _groupRepository.AddGroup(groupModel);

            return groupModel.Id;
        }

        public async Task<Guid> DeleteGroup(Guid id)
        {
            await _groupRepository.DeleteGroup(id);
            return id;
        }

        public async Task<GroupViewModel> GetGroup(Guid id)
        {
            var groupModel = await _groupRepository.GetGroupById(id);
            var groupViewModel = new GroupViewModel
            {
                Id = groupModel.Id,
                Name=groupModel.Name,
                Description=groupModel.Description
            };

            return groupViewModel;
        }

        public async Task<List<GroupViewModel>> GetGroups()
        {
            var groupViewModels = new List<GroupViewModel>();
            var groupModels = await _groupRepository.GetGroups();

            foreach (var groupModel in groupModels)
            {
                var groupViewModel = new GroupViewModel
                {
                    Id = groupModel.Id,
                    Name=groupModel.Name,
                    Description=groupModel.Description
                };
                groupViewModels.Add(groupViewModel);
            }

            return groupViewModels;
        }

        public async Task<Guid> UpdateGroup(UpdateGroupViewModel model)
        {
            var groupToUpdate = new GroupModel
            {
                Id = model.Id,
                Name=model.Name,
                Description=model.Description
            };
            await _groupRepository.UpdateGroup(groupToUpdate);
            return groupToUpdate.Id;
        }
    }
}
