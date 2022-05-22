using System.Text.Json;
using GradingSystem.Portals.Admin.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using GradingSystem.Portals.Admin.Services;
using System.Net.Http.Headers;

namespace GradingSystem.Portals.Admin.Components
{


    public partial class EditGradeScheme
    {
        [Inject]
        private IHttpClientFactory _httpClientFactory { get; set; }

        private HttpClient _httpClient { get; set; }
        [Parameter]
        public GradeSchemeModel GradeSchemeModel { get; set; }
        protected EditContext _editContext;
        protected ValidationMessageStore _validationMessageStore;

        [Parameter]
        public EventCallback<GradeSchemeModel> OnEditFinished { get; set; }

        [Parameter]
        public EventCallback<GradeSchemeModel> OnEditCanceled { get; set; }
        public string initialExamName { get; set; } = "";
         [Inject]
        private IAuthenticationService _authService{get;set;} 

        protected override async Task OnInitializedAsync()
        {
            _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

            _editContext = new EditContext(GradeSchemeModel);
            _editContext.OnValidationRequested += OnValidateEditGradeSchemeForm;
            _validationMessageStore = new(_editContext);

            if (GradeSchemeModel.Id == null)
            {
                GradeSchemeModel.GradeSchemeComponents = new List<GradeSchemeComponentModel>(new GradeSchemeComponentModel[10]);
                for (int i = 0; i < GradeSchemeModel.GradeSchemeComponents.Count; i++)
                {
                    GradeSchemeModel.GradeSchemeComponents[i] = new GradeSchemeComponentModel { Id = Guid.NewGuid(), Grade = i + 1 };
                }
            }
        }


        protected void OnValidateEditGradeSchemeForm(object sender, ValidationRequestedEventArgs e)
        {
            _validationMessageStore.Clear();

            if (string.IsNullOrWhiteSpace(GradeSchemeModel.Name))
            {
                _validationMessageStore.Add(() => GradeSchemeModel.Name, "Numele este obligatoriu");
            }
            for (int i = 0; i < GradeSchemeModel.GradeSchemeComponents.Count; i++)
            {
                var pozitie = i;
                if (GradeSchemeModel.GradeSchemeComponents[pozitie].Grade == null)
                {
                    _validationMessageStore.Add(() => GradeSchemeModel.GradeSchemeComponents[pozitie].Grade, "Nota este obligatorie");
                }
                if (GradeSchemeModel.GradeSchemeComponents[pozitie].MinimumScore == null)
                {
                    _validationMessageStore.Add(() => GradeSchemeModel.GradeSchemeComponents[pozitie].Grade, "Scorul minim este obligatoriu");
                }
                if (GradeSchemeModel.GradeSchemeComponents[pozitie].MaximumScore == null)
                {
                    _validationMessageStore.Add(() => GradeSchemeModel.GradeSchemeComponents[pozitie].Grade, "Scorul maxim este obligatoriu");
                }
            }
        }

        protected async Task OnFinishingEditClick()
        {
            await OnEditFinished.InvokeAsync(GradeSchemeModel);
        }
        protected async Task OnCancelEditClick()
        {
            await OnEditCanceled.InvokeAsync(null);
        }

    }
}