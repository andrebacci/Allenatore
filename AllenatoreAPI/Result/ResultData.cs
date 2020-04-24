using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Result
{
    public class ResultData
    {
        public ResultData()
        {

        }

        public object Data { get; set; }

        public bool Status { get; set; }

        public string FunctionName { get; set; }

        public string Message { get; set; }
    }
}
