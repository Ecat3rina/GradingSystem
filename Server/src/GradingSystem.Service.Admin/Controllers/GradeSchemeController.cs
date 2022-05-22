using GradingSystem.Service.Admin.Services.GradeScheme;
using GradingSystem.Service.Admin.ViewModels;
using GradingSystem.Service.Admin.ViewModels.GradeScheme;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Controllers
{
    [Route("gradescheme")]
    [ApiController]
    [Authorize(Policy = Startup.DefaultPolicy)]
    public class GradeSchemeController : ControllerBase
    {
        private readonly IGradeSchemeStorageService _gradeSchemeStorageService;

        public GradeSchemeController(IGradeSchemeStorageService gradeSchemeStorageService)
        {
            _gradeSchemeStorageService=gradeSchemeStorageService;
        }
        [HttpGet]
        public async Task<List<GradeSchemeViewModel>> GetAllGradeSchemes()
        {
            return await _gradeSchemeStorageService.GetGradeSchemes();
        }
        [HttpPost]
        public async Task<IActionResult> AddGradeScheme(NewGradeSchemeViewModel model)
        {
            var result = await _gradeSchemeStorageService.AddNewGradeScheme(model);
            return Ok(new AddNewEntityResponseViewModel { EntityId = result });
        }

        [HttpGet("{gradeSchemeId}")]
        public async Task<GradeSchemeViewModel> GetGradeSchemeById(Guid gradeSchemeId)
        {
            return await _gradeSchemeStorageService.GetGradeScheme(gradeSchemeId);
        }
        [AllowAnonymous]
        [HttpGet("examId/{examId}")]
        public async Task<IActionResult> GetGradeSchemeByExamId(Guid examId)
        {
            return  Ok(await _gradeSchemeStorageService.GetGradeSchemeByExamId(examId));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGradeScheme(UpdateGradeSchemeViewModel model)
        {
            await _gradeSchemeStorageService.UpdateGradeScheme(model);
            return Ok(new UpdateEntityResponseViewModel { EntityId = model.Id });
        }
        [HttpDelete("{gradeSchemeId}")]
        public async Task<IActionResult> DeleteGradeScheme(Guid gradeSchemeId)
        {
            await _gradeSchemeStorageService.DeleteGradeScheme(gradeSchemeId);
            return Ok(new DeleteEntityResponseViewModel { EntityId = gradeSchemeId });
        }
    }
}
