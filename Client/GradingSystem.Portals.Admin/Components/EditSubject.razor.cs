using System.Text.Json;
using GradingSystem.Portals.Admin.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GradingSystem.Portals.Admin.Components
{


    public partial class EditSubject
    {
        [Parameter]
        public SubjectModel SubjectModel { get; set; }
        protected EditContext _editContext;
        protected ValidationMessageStore _validationMessageStore;

        [Parameter]
        public EventCallback<SubjectModel> OnEditFinished { get; set; }

        [Parameter]
        public EventCallback<SubjectModel> OnEditCanceled { get; set; }
       
        protected override void OnInitialized()
        {
            _editContext = new(SubjectModel);
            _editContext.OnValidationRequested += OnValidateEditSubjectForm;
            _validationMessageStore = new(_editContext);
        }


        protected void OnValidateEditSubjectForm(object sender, ValidationRequestedEventArgs e)
        {
            _validationMessageStore.Clear();
            if (string.IsNullOrWhiteSpace(SubjectModel.Name))
            {
                _validationMessageStore.Add(() => SubjectModel.Name, "Numele este obligatoriu");
            }
        }

        protected async Task OnFinishingEditClick()
        {
            await OnEditFinished.InvokeAsync(SubjectModel);
        }
        protected async Task OnCancelEditClick()
        {
            await OnEditCanceled.InvokeAsync(null);
        }
    }
}