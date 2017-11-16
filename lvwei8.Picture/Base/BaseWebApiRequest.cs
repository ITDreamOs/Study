using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lvwei8.Picture.Base
{
    public class BaseWebApiRequest<T>
    {
        public T Param { get; set; }
    }
}