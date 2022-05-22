using System.Text.Json;
using GradingSystem.Portals.Admin.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GradingSystem.Portals.Admin.Components
{

    public partial class EditStudent
    {
        [Parameter]
        public StudentModel StudentModel { get; set; }
        
        protected EditContext _editContext;
        protected ValidationMessageStore _validationMessageStore;

        [Parameter]
        public EventCallback<StudentModel> OnEditFinished { get; set; }

        [Parameter]
        public EventCallback<StudentModel> OnEditCanceled { get; set; }
        private string initialGroup { get; set; }
        protected override void OnInitialized()
        {
            _editContext = new(StudentModel);
            _editContext.OnValidationRequested += OnValidateEditStudentForm;
            _validationMessageStore = new(_editContext);
            initialGroup=StudentModel.GroupId;
        }


        protected void OnValidateEditStudentForm(object sender, ValidationRequestedEventArgs e)
        {
            _validationMessageStore.Clear();
            if (string.IsNullOrWhiteSpace(StudentModel.FirstName))
            {
                _validationMessageStore.Add(() => StudentModel.FirstName, "Prenumele este obligatoriu");
            }
            if (string.IsNullOrWhiteSpace(StudentModel.LastName))
            {
                _validationMessageStore.Add(() => StudentModel.LastName, "Numele este obligatoriu");
            }
            if (string.IsNullOrWhiteSpace(StudentModel.Address))
            {
                _validationMessageStore.Add(() => StudentModel.Address, "Adresa este obligatorie");
            }
            if (string.IsNullOrWhiteSpace(StudentModel.GroupId))
            {
                _validationMessageStore.Add(() => StudentModel.GroupId, "Grupul este obligatoriu");
            }
            if (string.IsNullOrWhiteSpace(StudentModel.IDNP))
            {
                _validationMessageStore.Add(() => StudentModel.IDNP, "IDNP-ul este obligatoriu");
            }
            if ((StudentModel.IDNP).Length != 13)
            {
                _validationMessageStore.Add(() => StudentModel.IDNP, "IDNP-ul trebuie sa fie compus din 13 cifre");
            }
            if (!((StudentModel.IDNP).All(char.IsDigit)))
            {
                _validationMessageStore.Add(() => StudentModel.IDNP, "IDNP-ul trebuie sa contina doar cifre");
            }
        }

        protected async Task OnFinishingEditClick()
        {
            await OnEditFinished.InvokeAsync(StudentModel);
        }
        protected async Task OnCancelEditClick()
        {
            await OnEditCanceled.InvokeAsync(null);
        }
        protected void OnFinishingSelectingGroup(string groupId)
        {
            StudentModel.GroupId=groupId;
        }
    }
}