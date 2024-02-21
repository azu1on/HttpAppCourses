using HttpApp.Attributes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.Attributes
{
    public class HttpPutAttribute : HttpAttribute
    {
        public HttpPutAttribute(string routing) : base(HttpMethod.Put, routing) { }

        public HttpPutAttribute() : base(HttpMethod.Put, null) { }
    }
}
