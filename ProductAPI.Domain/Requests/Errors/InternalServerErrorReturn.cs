using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Domain.Requests.Errors
{
    public class InternalServerErrorReturn : ApiError
    {
        public InternalServerErrorReturn()
            : base(500)
        {
        }

        public InternalServerErrorReturn(string message)
            : base(500, message)
        {
        }
    }
}
