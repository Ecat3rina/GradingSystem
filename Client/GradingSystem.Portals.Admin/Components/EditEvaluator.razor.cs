using System.Text.Json;
using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GradingSystem.Portals.Admin.Components
{

    public partial class EditEvaluator
    {
        [Parameter]
        public EvaluatorModel EvaluatorModel { get; set; }
        protected EditContext _editContext;
         [Inject]
        private IAuthenticationService _authService{get;set;} 
        protected ValidationMessageStore _validationMessageStore;

        [Parameter]
        public EventCallback<EvaluatorModel> OnEditFinished { get; set; }

        [Parameter]
        public EventCallback<EvaluatorModel> OnEditCanceled { get; set; }
        public string initialSubject { get; set; } 

        protected override void OnInitialized()
        {
            _editContext = new(EvaluatorModel);
            _editContext.OnValidationRequested += OnValidateEditEvaluatorForm;
            _validationMessageStore = new(_editContext);
            initialSubject=EvaluatorModel.SubjectName;
        }


        protected void OnValidateEditEvaluatorForm(object sender, ValidationRequestedEventArgs e)
        {
            _validationMessageStore.Clear();
            if (string.IsNullOrWhiteSpace(EvaluatorModel.FirstName))
            {
                _validationMessageStore.Add(() => EvaluatorModel.FirstName, "Prenumele este obligatoriu");
            }
            if (string.IsNullOrWhiteSpace(EvaluatorModel.LastName))
            {
                _validationMessageStore.Add(() => EvaluatorModel.LastName, "Numele este obligatoriu");
            }
            if (string.IsNullOrWhiteSpace(EvaluatorModel.SubjectId))
            {
                _validationMessageStore.Add(() => EvaluatorModel.SubjectId, "Disciplina este obligatorie");
            }
        }
        protected async Task OnFinishingEditClick()
        {
            await OnEditFinished.InvokeAsync(EvaluatorModel);
        }
        protected async Task OnCancelEditClick()
        {
            await OnEditCanceled.InvokeAsync(null);
        }
        protected void OnFinishingSelectingSubject(string subjectId)
        {
            EvaluatorModel.SubjectId=subjectId;
        }
    }
}