using GradingSystem.Service.ThesisService.DataAccess;
using GradingSystem.Service.ThesisService.Models;
using GradingSystem.Service.ThesisService.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GradingSystem.Service.ThesisService.Services
{
    public class ThesisStorageService : IThesisStorageService
    {
        private readonly IThesisRepository _thesisRepository;
        private readonly HttpClient _scoringServiceHttpClient;
        public ThesisStorageService(IThesisRepository thesisRepository,
             IHttpClientFactory httpClientFactory)
        {
            _thesisRepository = thesisRepository;
            _scoringServiceHttpClient = httpClientFactory.CreateClient("ScoringService");
        }

        public async Task<Guid> AddNewThesisAsync(NewThesisViewModel model)
        {   
            var thesisModel = new ThesisModel
            {
                Id = Guid.NewGuid(),
                ExamId = model.ExamId,
                StudentId = model.StudentId,
                NumberOfPages = model.NumberOfPages,
                Filename = model.Filename,
                FileContent = model.Filestream
            };

            var payload = new
            {
                model.StudentId,
                model.ExamId,
                BlobId = thesisModel.Id
            };

            await _thesisRepository.AddThesisAsync(thesisModel);

            return thesisModel.Id;
        }

        public async Task<List<ThesisViewModel>> GetThesesByStudentId(Guid id)
        {
            var thesesViewModels = new List<ThesisViewModel>();
            var thesesModels = await _thesisRepository.GetThesesByStudentId(id);

            foreach (var thesisModel in thesesModels)
            {
                var thesisViewModel = new ThesisViewModel
                {
                    Id = thesisModel.Id,
                    StudentId=thesisModel.StudentId,
                    ExamId=thesisModel.ExamId,
                    FileContent=thesisModel.FileContent,
                    NumberOfPages=thesisModel.NumberOfPages,
                    Filename=thesisModel.Filename
                };
                thesesViewModels.Add(thesisViewModel);
            }

            return thesesViewModels;
        }

        public async Task<ThesisViewModel> GetThesisById(Guid thesisId)
        {
            ThesisModel thesisModel = await _thesisRepository.GetThesisById(thesisId);
            ThesisViewModel thesisViewModel = new ThesisViewModel
            {
                Id = thesisModel.Id,
                StudentId = thesisModel.StudentId,
                ExamId = thesisModel.ExamId,
                FileContent = thesisModel.FileContent,
                Filename = thesisModel.Filename
            };
            return thesisViewModel;
        }

        public async Task<ThesisViewModel> GetThesisByStudentAndExamId(Guid studentId, Guid examId)
        {
            ThesisModel thesisModel=await _thesisRepository.GetThesisByExamStudentId(studentId,examId);
            ThesisViewModel thesisViewModel = new ThesisViewModel 
            {
                Id=thesisModel.Id,
                StudentId=thesisModel.StudentId,
                ExamId=thesisModel.ExamId,
                FileContent=thesisModel.FileContent,
                Filename=thesisModel.Filename
            };
            return thesisViewModel;
        }

    }
}
