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

    public partial class EditEvaluationScheme
    {
        [Inject]
        private IHttpClientFactory _httpClientFactory { get; set; }

        private HttpClient _httpClient { get; set; }
        [Parameter]
        public EvaluationSchemeModel EvaluationSchemeModel { get; set; }
        protected EditContext _editContext;
        protected ValidationMessageStore _validationMessageStore;

        [Parameter]
        public EventCallback<EvaluationSchemeModel> OnEditFinished { get; set; }

        [Parameter]
        public EventCallback<EvaluationSchemeModel> OnEditCanceled { get; set; }
        public string initialExamName { get; set; }
        public bool generateEvaluationComponents { get; set; } = false;
         [Inject]
        private IAuthenticationService _authService{get;set;} 

        protected override async Task OnInitializedAsync()
        {
            _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

            _editContext = new(EvaluationSchemeModel);
            _editContext.OnValidationRequested += OnValidateEditEvaluationSchemeForm;
            _validationMessageStore = new(_editContext);
            if (EvaluationSchemeModel.Id != null)
            {
                var examModel = await _httpClient.GetFromJsonAsync<ExamModel>($"exam/GetExamById/{EvaluationSchemeModel.ExamId}");
                initialExamName = examModel.Name;
            }

        }


        protected void OnValidateEditEvaluationSchemeForm(object sender, ValidationRequestedEventArgs e)
        {
            _validationMessageStore.Clear();
            if (string.IsNullOrWhiteSpace(EvaluationSchemeModel.Name))
            {
                _validationMessageStore.Add(() => EvaluationSchemeModel.Name, "Numele este obligatoriu");
            }
            if (string.IsNullOrWhiteSpace(EvaluationSchemeModel.ExamId))
            {
                _validationMessageStore.Add(() => EvaluationSchemeModel.ExamId, "Examenul este obligatoriu");
            }
            if (EvaluationSchemeModel.NumberOfItems <= 0)
            {
                _validationMessageStore.Add(() => EvaluationSchemeModel.NumberOfItems, "Numarul de itemi este obligatoriu");
            }
            for (int i = 0; i < EvaluationSchemeModel.EvaluationSchemeComponents.Count; i++)
            {
                var pozitie = i;
                if (EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].ItemNr == null || EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].ItemNr <= 0)
                {
                    _validationMessageStore.Add(() => EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].ItemNr, "Numarul itemului este obligatoriu");
                }
                if (EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].PageNr == null || EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].PageNr <= 0)
                {
                    _validationMessageStore.Add(() => EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].ItemNr, "Numarul paginii este obligatoriu");
                }
                if (EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].MinimumScore == null || EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].MinimumScore < 0)
                {
                    _validationMessageStore.Add(() => EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].ItemNr, "Scorul minim este obligatoriu");
                }
                if (EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].MaximumScore == null || EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].MaximumScore <= 0)
                {
                    _validationMessageStore.Add(() => EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].ItemNr, "Scorul maxim este obligatoriu");
                }
                if (string.IsNullOrWhiteSpace(EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].CorrectAnswer))
                {
                    _validationMessageStore.Add(() => EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].ItemNr, "Raspunsul corect este obligatoriu");
                }
                if (string.IsNullOrWhiteSpace(EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].Specifications))
                {
                    _validationMessageStore.Add(() => EvaluationSchemeModel.EvaluationSchemeComponents[pozitie].ItemNr, "Specificatiile sunt obligatorii");
                }
            }

        }
        protected async Task OnFinishingEditClick()
        {
            await OnEditFinished.InvokeAsync(EvaluationSchemeModel);
        }
        protected async Task OnCancelEditClick()
        {
            await OnEditCanceled.InvokeAsync(null);
        }
        protected void OnFinishingSelectingExam(string examId)
        {
            EvaluationSchemeModel.ExamId = examId;
        }
        private void GetEvaluationSchemeComponents()
        {
            generateEvaluationComponents = true;
            EvaluationSchemeModel.EvaluationSchemeComponents = new List<EvaluationSchemeComponentModel>(new EvaluationSchemeComponentModel[EvaluationSchemeModel.NumberOfItems]);
            for (int i = 0; i < EvaluationSchemeModel.EvaluationSchemeComponents.Count; i++)
            {
                EvaluationSchemeModel.EvaluationSchemeComponents[i] = new EvaluationSchemeComponentModel { Id = Guid.NewGuid() };
            }
        }
    }
}