using Dapper;
using GradingSystem.Service.Admin.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystem.Service.Admin.DataAccess.ScheduleExam
{
    public class ScheduleExamRepository : IScheduleExamRepository
    {
        private readonly string _scheduleExamDbConnectionString;
        public ScheduleExamRepository(string connectionString)
        {
            _scheduleExamDbConnectionString = connectionString;
        }

        public async Task AddScheduleExam(ScheduleExamModel model)
        {
            using var connection = new SqlConnection(_scheduleExamDbConnectionString);
            await connection.OpenAsync();
            var query = @"INSERT INTO ScheduleExams (Id, GroupId, ExamId) VALUES (@Id, @GroupId, @ExamId)";
            await connection.ExecuteAsync(query, model);
        }

        public async Task<List<GroupExamsModel>> GetScheduleExams()
        {
            using var connection = new SqlConnection(_scheduleExamDbConnectionString);
            await connection.OpenAsync();
            var scheduleExamList = new List<GroupExamsModel>();
            var groupNames = await connection.QueryAsync<string>(@"select Groups.Name from Groups
                                    inner join ScheduleExams on Groups.Id = ScheduleExams.GroupId
                                    group by Groups.Name");
            groupNames = groupNames.ToList();
            var groupIds = await connection.QueryAsync<Guid>(@"select Groups.Id from Groups 
                                    inner join ScheduleExams on Groups.Id=ScheduleExams.GroupId
                                    group by Groups.Id");
            groupIds = groupIds.ToList();
            foreach (var item in groupNames.Zip(groupIds, (a, b) => new { A = a, B = b }))
            {
                var a = item.A;
                var b = item.B;
                var exams = await connection.QueryAsync<string>(@"select Exams.Name from Exams 
                                            inner join ScheduleExams on Exams.Id=ScheduleExams.ExamId
                                            where ScheduleExams.GroupId=@groupId", new { groupId = item.B });
                var scheduleExam = new GroupExamsModel
                {
                    GroupName = item.A,
                    Exams = exams.ToList()
                };
                scheduleExamList.Add(scheduleExam);

            }
            return scheduleExamList;
        }
    }
}
