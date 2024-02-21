using Dapper;
using HttpApp.Attributes;
using HttpApp.Controllers.Base;
using HttpApp.Extensions;
using HttpApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpApp.Controllers
{
    internal class CourseController : ControllerBase
    {
        private const string connectionString = "Data Source=.;Initial Catalog=HttpAppDb;Integrated Security=True";

        [HttpGet("GetAll")]
        public async Task GetCoursesAsync(HttpListenerContext context)
        {
            using var writer = new StreamWriter(context.Response.OutputStream);

            using var connection = new SqlConnection(connectionString);
            var courses = await connection.QueryAsync<Courses>("select * from Courses");

            var coursesHtml = courses.GetHtml();
            await writer.WriteLineAsync(coursesHtml);
            context.Response.ContentType = "text/html";

            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }


        [HttpGet("GetById")]
        public async Task GetCourseByIdAsync(HttpListenerContext context)
        {
            var courseIdToGetObj =context.Request.QueryString["id"];

            if (courseIdToGetObj == null || int.TryParse(courseIdToGetObj, out int courseIdToGet) == false)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            using var connection = new SqlConnection(connectionString);
            var course = await connection.QueryFirstOrDefaultAsync<Courses>(
                sql: "select top 1 * from Courses where Id = @Id",
                param: new { Id = courseIdToGet });

            if (course is null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            using var writer = new StreamWriter(context.Response.OutputStream);

            var courseHtml = course.GetHtml();

            await writer.WriteLineAsync(courseHtml);
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }


        [HttpPost("Create")]
        public async Task PostCoursesAsync(HttpListenerContext context)
        {
            using var reader = new StreamReader(context.Request.InputStream);
            var json = await reader.ReadToEndAsync();

            var newCourses = JsonSerializer.Deserialize<Courses>(json);

            if (newCourses == null || newCourses.Price == null || string.IsNullOrWhiteSpace(newCourses.Name))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            using var connection = new SqlConnection(connectionString);
            var courses = await connection.ExecuteAsync(
                @"insert into Courses (Name, Price, Discription)
                    values(@Name, @Price, @Discription)",
                param: new
                {
                    newCourses.Name,
                    newCourses.Price,
                    newCourses.Discription
                });
            context.Response.StatusCode = (int)HttpStatusCode.Created;
        }

        [HttpDelete]
        public async Task DeleteCoursesAsync(HttpListenerContext context)
        {
            var courseIdDeleteObj = context.Request.QueryString["id"];
            if (courseIdDeleteObj == null || int.TryParse(courseIdDeleteObj, out int courseIdDelete) == false)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }


            using var connection = new SqlConnection(connectionString);
            var deletedRowsCount = await connection.ExecuteAsync(
                @"delete Courses where Id = @Id",
                param: new
                {
                    id = courseIdDelete,
                });

            if (deletedRowsCount == 0)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }

        [HttpPut]
        public async Task PutCoursesAsync(HttpListenerContext context)
        {

            var courseIdUpdateObj = context.Request.QueryString["id"];
            if (courseIdUpdateObj == null || int.TryParse(courseIdUpdateObj, out int courseIdUpdate) == false)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }
            using var reader = new StreamReader(context.Request.InputStream);
            var json = await reader.ReadToEndAsync();

            var courseUpdate = JsonSerializer.Deserialize<Courses>(json);

            if (courseUpdate == null || courseUpdate.Price == null || string.IsNullOrEmpty(courseUpdate.Name))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            using var connection = new SqlConnection(connectionString);
            var updatedRowsCount = await connection.ExecuteAsync(
                @"update Courses set Name = @Name, Price = @Price, Discription = @Discription where Id = @Id",
                param: new
                {
                    courseUpdate.Name,
                    courseUpdate.Price,
                    courseUpdate.Discription,
                    Id = courseIdUpdate,
                });
            if (updatedRowsCount == 0)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}
