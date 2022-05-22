using System.Text.Json;
using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GradingSystem.Portals.Admin.Components
{

    public partial class AddScheduleExam
    {
        [Parameter]
        public ScheduleExamModel ScheduleExamModel { get; set; }
        protected EditContext _editContext;
        protected ValidationMessageStore _validationMessageStore;

        [Parameter]
        public EventCallback<ScheduleExamModel> OnEditFinished { get; set; }

        [Parameter]
        public EventCallback<ScheduleExamModel> OnEditCanceled { get; set; }
         [Inject]
        private IAuthenticationService _authService{get;set;} 
        public string initialExamName { get; set; }=""; 
        public string initialGroup { get; set; } ="";


        protected override void OnInitialized()
        {
            _editContext = new(ScheduleExamModel);
            _editContext.OnValidationRequested += OnValidateAddScheduleExamForm;
            _validationMessageStore = new(_editContext);
        }


        protected void OnValidateAddScheduleExamForm(object sender, ValidationRequestedEventArgs e)
        {
            _validationMessageStore.Clear();
            if (string.IsNullOrWhiteSpace(ScheduleExamModel.ExamId))
            {
                _validationMessageStore.Add(() => ScheduleExamModel.ExamId, "Examenul este obligatoriu");
            }
            if (string.IsNullOrWhiteSpace(ScheduleExamModel.GroupId))
            {
                _validationMessageStore.Add(() => ScheduleExamModel.GroupId, "Grupul este obligatoriu");
            }
        }
        protected async Task OnFinishingEditClick()
        {
            await OnEditFinished.InvokeAsync(ScheduleExamModel);
        }
        protected async Task OnCancelEditClick()
        {
            await OnEditCanceled.InvokeAsync(null);
        }
        
        protected void OnFinishingSelectingGroup(string groupId)
        {
            ScheduleExamModel.GroupId=groupId;
        }
        protected void OnFinishingSelectingExam(string examId)
        {
            ScheduleExamModel.ExamId=examId;
        }
    }
}