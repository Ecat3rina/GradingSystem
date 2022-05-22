using GradingSystem.Portals.Admin.Models;
using GradingSystem.Portals.Admin.Components;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using GradingSystem.Portals.Admin.Services;
using System.Net.Http.Headers;

namespace GradingSystem.Portals.Admin.Pages;

public partial class Student
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    protected List<StudentModel> students { get; set; }
    protected List<GroupModel> groups { get; set; }

    private bool _showEditForm { get; set; } = false;
    private bool _showDeleteModal { get; set; } = false;
    public string studentId { get; set; }

    [Inject]
    private IHttpClientFactory _httpClientFactory {get;set;}
   
    [Inject]
    private IAuthenticationService _authService{get;set;}

    private HttpClient _httpClient { get; set; }

    protected StudentModel editStudent { get; set; }
    protected override async Task OnInitializedAsync()
    {
        _httpClient = _httpClientFactory.CreateClient("AdminApi");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetAuthTokenAsync());

        await Task.WhenAll(GetStudents(), GetGroups());
        foreach (var student in students)
        {
            student.GroupName = groups.FirstOrDefault(x => x.Id == student.GroupId)?.Name;
        }
    }

    private async Task GetStudents()
    {
        students = await _httpClient.GetFromJsonAsync<List<StudentModel>>("student");
    }

    private async Task GetGroups()
    {
        groups = await _httpClient.GetFromJsonAsync<List<GroupModel>>("group/getallgroups");
    }

    protected void OnAddNewStudentClick()
    {
        editStudent = new StudentModel();
        _showEditForm = true;
    }

    protected async Task OnEditStudentClick(string id,string groupId)
    {
        var student = await _httpClient.GetFromJsonAsync<StudentModel>($"/student/{id}");
        var group=await _httpClient.GetFromJsonAsync<GroupModel>($"group/getbyid/{groupId}");
        editStudent = student;
        editStudent.GroupName=group.Name;
        _showEditForm = true;
    }

    protected async Task OnDeleteStudentClick(string id)
    {
        _showDeleteModal = true;
        studentId = id;
        await Task.WhenAll(GetStudents(), GetGroups());
        foreach (var student in students)
        {
            student.GroupName = groups.FirstOrDefault(x => x.Id == student.GroupId)?.Name;
        }
    }

    protected async Task OnCloseDeleteModalClick(bool accepted)
    {
        if (accepted)
        {
            await _httpClient.DeleteAsync($"student/{studentId}");
        }
        _showDeleteModal = false;
        students = await _httpClient.GetFromJsonAsync<List<StudentModel>>("student/");
    }

    protected void OnGoBackClick()
    {
        _navigationManager.NavigateTo("home");
    }

    protected async Task OnFinishingEditingStudent(StudentModel studentModel)
    {
        _showEditForm = false;
        editStudent = null;
        if (string.IsNullOrWhiteSpace(studentModel.Id))
        {
            await _httpClient.PostAsJsonAsync("student", studentModel);
        }
        else
        {
            await _httpClient.PutAsJsonAsync("student", studentModel);
        }
        await Task.WhenAll(GetStudents(), GetGroups());
        foreach (var student in students)
        {
            student.GroupName = groups.FirstOrDefault(x => x.Id == student.GroupId)?.Name;
        }

    }

    protected void OnEditStudentCanceled()
    {
        _showEditForm = false;
    }
}
