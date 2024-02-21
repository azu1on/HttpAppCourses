using HttpApp.Attributes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.Attributes
{
    public class HttpPostAttribute : HttpAttribute
    {
        public HttpPostAttribute(string routing) : base(HttpMethod.Post, routing) { }

        public HttpPostAttribute() : base(HttpMethod.Post, null) { }
    }
}
