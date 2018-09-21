using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiModel.Helper;

namespace RestApiModel.Model
{
    public interface IMessageHelper
    {
        bool SendIntercom(string message);

    }
}
