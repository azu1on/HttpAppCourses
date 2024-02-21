using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.Models
{
    internal class Courses
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Discription { get; set; }

        internal void GetCoursesAsync(HttpListenerContext context)
        {
            throw new NotImplementedException();
        }
    }
}

//Get: /courses
//Get: /courses?id=123
//Post: /courses Request Body
//Put: /courses?id=123 Request Body
//Delete: /courrses?id=123