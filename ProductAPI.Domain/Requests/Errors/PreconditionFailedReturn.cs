using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Domain.Requests.Errors
{

    public class PreconditionFailedReturn : ApiError
    {
        public IEnumerable<string> Conditions { get; set; }

        public PreconditionFailedReturn()
            : base(412)
        {
        }

        public PreconditionFailedReturn(string message)
            : base(412, message)
        {
        }

        public PreconditionFailedReturn(string message, IEnumerable<string> conditions)
            : base(412, message)
        {
            Conditions = conditions;
        }
    }
}
