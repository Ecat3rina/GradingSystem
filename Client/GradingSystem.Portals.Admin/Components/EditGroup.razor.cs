using System.Text.Json;
using GradingSystem.Portals.Admin.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GradingSystem.Portals.Admin.Components
{

    public partial class EditGroup
    {
        [Parameter]
        public GroupModel GroupModel { get; set; }
        
        protected EditContext _editContext;
        protected ValidationMessageStore _validationMessageStore;

        [Parameter]
        public EventCallback<GroupModel> OnEditFinished { get; set; }

        [Parameter]
        public EventCallback<GroupModel> OnEditCanceled { get; set; }

        protected override void OnInitialized()
        {
            _editContext = new(GroupModel);
            _editContext.OnValidationRequested += OnValidateEditGroupForm;
            _validationMessageStore = new(_editContext);
        }


        protected void OnValidateEditGroupForm(object sender, ValidationRequestedEventArgs e)
        {
            _validationMessageStore.Clear();
            if (string.IsNullOrWhiteSpace(GroupModel.Name))
            {
                _validationMessageStore.Add(() => GroupModel.Name, "Numele este obligatoriu");
            }
        }

        protected async Task OnFinishingEditClick()
        {
            await OnEditFinished.InvokeAsync(GroupModel);
        }
        protected async Task OnCancelEditClick()
        {
            await OnEditCanceled.InvokeAsync(null);
        }
    }
}