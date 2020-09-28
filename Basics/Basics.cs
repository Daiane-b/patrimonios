using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public enum RequisitionStatus
    {
        Unprocessed = 1,
        ProcessedWithError = 2,
        Processed = 3
    }
    public enum CodeStatus
    {
        PerfectProcess = 0,
        TokenNotInformed = 2,
        InvalidToken = 3,
        DuplicatedKey = 4,
        ParameterNotInformed = 5,
        PageNotExist = 6,
        RequiredFieldEmptyOrWorng = 7,
        ProcessException = 8
    }
    public class BasicResponse
    {
        public RequisitionStatus Status { get; set; }
        public CodeStatus CodeStatus { get; set; }
        public int RecordsAffect { get; set; }
        public object Data { get; set; }
        public List<string> Messages { get; set; }

        public BasicResponse()
        {
            Status = 0;
            CodeStatus = 0;
            RecordsAffect = 0;
            Data = null;
            Messages = new List<string>();
        }
    }
    public class BasicLogin
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool isAtutenticated { get; set; }

        public BasicLogin (IIdentity identify)
        {
            var i = (ClaimsIdentity)identify;
            isAtutenticated = identify.IsAuthenticated;
        }
    }
}
