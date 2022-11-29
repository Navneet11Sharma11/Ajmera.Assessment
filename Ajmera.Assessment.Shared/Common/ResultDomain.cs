    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajmera.Assessment.Shared.Common
{
    public class ResultDomain
    {
        private bool _isSuccess;
        public ResultDomain()
        {
            _isSuccess = true;
            Errors = new List<string>();
        }

        public object Data { get; set; }

        public int TotalCount { get; set; }

        public bool IsSuccess
        {
            get { return _isSuccess && !Errors.Any(); }
            set { _isSuccess = value; }
        }

        public List<string> Errors { get; set; }
        public string Message { get; set; }
    }
}
