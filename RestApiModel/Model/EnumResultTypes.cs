using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModel.Model
{
    public enum EnumResultTypes
    {
        OK = 1,
        SQLERROR,
        NOTFOUND,
        INVALIDARGUMENT,
        ERROR
    }
}
