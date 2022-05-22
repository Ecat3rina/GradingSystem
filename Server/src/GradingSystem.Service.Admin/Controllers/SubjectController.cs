using GradingSystem.Service.Admin.Services.Subject;
using GradingSystem.Service.Admin.ViewModels;
using GradingSystem.Service.Admin.ViewModels.Subject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Controllers
{
    [Route("subject")]
    [ApiController]
    [Authorize(Policy = Startup.DefaultPolicy)]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectStorageService _subjectStorageService;
        public SubjectController(ISubjectStorageService subjectStorageService)
        {
            _subjectStorageService = subjectStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject(NewSubjectViewModel model)
        {
            var result = await _subjectStorageService.AddNewSubject(model);
            return Ok(new AddNewEntityResponseViewModel { EntityId = result });
        }

        [HttpGet]
        public async Task<List<SubjectViewModel>> GetAllSubjects()
        {
            return await _subjectStorageService.GetSubjects();
        }

        [HttpGet("{subjectId}")]
        public async Task<SubjectViewModel> GetSubjectById(Guid subjectId)
        {
            return await _subjectStorageService.GetSubject(subjectId);
        }

        [HttpDelete("{subjectId}")]
        public async Task<IActionResult> DeleteSubject(Guid subjectId)
        {
            await _subjectStorageService.DeleteSubject(subjectId);
            return Ok(new DeleteEntityResponseViewModel { EntityId = subjectId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubject(UpdateSubjectViewModel model)
        {
            await _subjectStorageService.UpdateSubject(model);
            return Ok(new UpdateEntityResponseViewModel { EntityId = model.Id });
        }
    }
}
