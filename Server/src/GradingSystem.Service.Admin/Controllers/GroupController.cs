using GradingSystem.Service.Admin.Services.Group;
using GradingSystem.Service.Admin.ViewModels;
using GradingSystem.Service.Admin.ViewModels.Group;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Controllers
{

    [Route("group")]
    [ApiController]
    [Authorize(Policy = Startup.DefaultPolicy)]
    public class GroupController : Controller
    {

        private readonly IGroupStorageService _groupStorageService;
        public GroupController(IGroupStorageService groupStorageService)
        {
            _groupStorageService = groupStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(NewGroupViewModel model)
        {
            var result = await _groupStorageService.AddNewGroup(model);
            return Ok(new AddNewEntityResponseViewModel { EntityId = result });
        }

        [HttpGet("getallgroups")]
        public async Task<List<GroupViewModel>> GetAllGroups()
        {
            return await _groupStorageService.GetGroups();
        }
        [HttpGet("getbyid/{groupId}")]

        public async Task<GroupViewModel> GetGroupById(Guid groupId)
        {
            return await _groupStorageService.GetGroup(groupId);
        }
        [HttpDelete("deletegroup/{groupId}")]
        public async Task<IActionResult> DeleteGroup(Guid groupId)
        {
            await _groupStorageService.DeleteGroup(groupId);
            return Ok(new DeleteEntityResponseViewModel { EntityId = groupId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup(UpdateGroupViewModel model)
        {
            await _groupStorageService.UpdateGroup(model);
            return Ok(new UpdateEntityResponseViewModel { EntityId = model.Id });
        }

    }
}

