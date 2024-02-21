using HttpApp.Attributes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.Attributes
{
    public class HttpDeleteAttribute : HttpAttribute
    {
        public HttpDeleteAttribute(string routing) : base(HttpMethod.Delete, routing) { }

        public HttpDeleteAttribute() : base(HttpMethod.Delete, null) { }
    }
}
