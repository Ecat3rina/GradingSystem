using GradingSystem.Service.Admin.Services.Student;
using GradingSystem.Service.Admin.ViewModels;
using GradingSystem.Service.Admin.ViewModels.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.Controllers
{
    [Route("student")]
    [ApiController]
    [Authorize(Policy = Startup.DefaultPolicy)]

    public class StudentController : ControllerBase
    {
        private readonly IStudentStorageService _studentStorageService;
        public StudentController(IStudentStorageService studentStorageService)
        {
            _studentStorageService = studentStorageService;
        }
       
        [HttpPost]
        public async Task<IActionResult> AddStudent(NewStudentViewModel model)
        {
            var result = await _studentStorageService.AddNewSudent(model);
            return Ok(new AddNewEntityResponseViewModel { EntityId = result });
        }

        [HttpGet]
        public async Task<List<StudentViewModel>> GetAllStudents()
        {
            return await _studentStorageService.GetStudents();
        }

        [HttpGet("{studentId}")]
        public async Task<StudentViewModel> GetStudentById(Guid studentId)
        {
            return await _studentStorageService.GetStudent(studentId);
        }

        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(Guid studentId)
        {
            await _studentStorageService.DeleteStudent(studentId);
            return Ok(new DeleteEntityResponseViewModel { EntityId = studentId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(UpdateStudentViewModel model)
        {
            await _studentStorageService.UpdateStudent(model);
            return Ok(new UpdateEntityResponseViewModel { EntityId = model.Id });
        }

    }
}
