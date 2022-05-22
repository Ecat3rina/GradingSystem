using System.Text.Json;
using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GradingSystem.Portals.Admin.Components
{


    public partial class EditExam
    {
        [Parameter]
        public ExamModel ExamModel { get; set; }
        protected EditContext _editContext;
        protected ValidationMessageStore _validationMessageStore;
         [Inject]
        private IAuthenticationService _authService{get;set;} 

        [Parameter]
        public EventCallback<ExamModel> OnEditFinished { get; set; }

        [Parameter]
        public EventCallback<ExamModel> OnEditCanceled { get; set; }

        public string _initialSubject { get; set; }
        public string initialGradeSchemeName { get; set; }

        protected override void OnInitialized()
        {
            _editContext = new(ExamModel);
            _editContext.OnValidationRequested += OnValidateEditExamForm;
            _validationMessageStore = new(_editContext);
            _initialSubject = ExamModel.SubjectName;
            initialGradeSchemeName=ExamModel.GradeSchemeName;
        }


        protected void OnValidateEditExamForm(object sender, ValidationRequestedEventArgs e)
        {
            _validationMessageStore.Clear();

            if (string.IsNullOrWhiteSpace(ExamModel.Name))
            {
                _validationMessageStore.Add(() => ExamModel.Name, "Numele este obligatoriu");
            }

            if (string.IsNullOrWhiteSpace(ExamModel.SubjectId))
            {
                _validationMessageStore.Add(() => ExamModel.SubjectId, "Disciplina este obligatorie");
            }
            if (string.IsNullOrWhiteSpace(ExamModel.GradeSchemeId))
            {
                _validationMessageStore.Add(() => ExamModel.GradeSchemeId, "Schema de notare este obligatorie");
            }
            if ((ExamModel.NumberOfPages == 0) || string.IsNullOrEmpty(ExamModel.NumberOfPages.ToString()))
            {
                _validationMessageStore.Add(() => ExamModel.NumberOfPages, "Numarul de pagini este invalid");
            }

            if ((ExamModel.NumberOfEvaluators == 0) || string.IsNullOrEmpty(ExamModel.NumberOfEvaluators.ToString()))
            {
                _validationMessageStore.Add(() => ExamModel.NumberOfEvaluators, "Numarul de evaluatori este invalid");
            }

        }

        protected async Task OnFinishingEditClick()
        {
            await OnEditFinished.InvokeAsync(ExamModel);
        }
        protected async Task OnCancelEditClick()
        {
            await OnEditCanceled.InvokeAsync(null);
        }
        protected void OnFinishingSelectingSubject(string subjectId)
        {
            ExamModel.SubjectId = subjectId;
        }
        protected void OnFinishingSelectingGradeScheme(string gradeSchemeId)
        {
            ExamModel.GradeSchemeId = gradeSchemeId;
        }
    }
}