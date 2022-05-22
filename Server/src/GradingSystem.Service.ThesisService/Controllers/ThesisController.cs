using GradingSystem.Service.ThesisService.Services;
using GradingSystem.Service.ThesisService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystem.Service.ThesisService.Controllers
{
    [Route("thesis")]
    [ApiController]
    public class ThesisController : ControllerBase
    {
        private readonly IThesisStorageService _thesisStorageService;
        public ThesisController(IThesisStorageService thesisStorageService)
        {
            _thesisStorageService = thesisStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddThesis([FromForm] NewThesisViewModel model)
        {
            var files = HttpContext.Request.Form.Files.Count;
            if (files == 1)
            {
                using var filestream = HttpContext.Request.Form.Files[0].OpenReadStream();
                model.Filestream = new byte[filestream.Length];
                _ = await filestream.ReadAsync(model.Filestream.AsMemory(0, model.Filestream.Length));

                var result = await _thesisStorageService.AddNewThesisAsync(model);
                return Ok(new AddNewThesisResponseViewModel { ThesisId = result });
            }
            else
            {
                return BadRequest(new { Message = "File is missing" });
            }
        }

        [HttpGet("{studentId}/{examId}")]
        public async Task<IActionResult> GetThesisByStudentIdAndExamId(Guid studentId, Guid examId)
        {

            ThesisViewModel thesis = await _thesisStorageService.GetThesisByStudentAndExamId(studentId,examId);
            var file = thesis.FileContent;
            return new FileContentResult(file, "application/pdf");
           
        }

        [HttpGet("id/{thesisId}")]
        public async Task<IActionResult> GetThesisById(Guid thesisId)
        {
            ThesisViewModel thesis = await _thesisStorageService.GetThesisById(thesisId);
            var file = thesis.FileContent;
            return new FileContentResult(file, "application/pdf");
        }
        [HttpGet("studentId/{studentId}")]
        public async Task<List<ThesisViewModel>> GetThesesByStudentId(Guid studentId)
        {
            return await _thesisStorageService.GetThesesByStudentId(studentId);
        }
    }
}
