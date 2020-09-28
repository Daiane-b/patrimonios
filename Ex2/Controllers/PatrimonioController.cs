using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data;
using Tools;
using Actions;

namespace Ex2.Controllers
{
    public class PatrimonioController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("api/patrimonio/get")]
        public BasicResponse GetPatrimonio()
        {
            BasicResponse r = new BasicResponse();
            var login = new BasicLogin(User.Identity);

            if (!login.isAtutenticated)
            {
                r.Status = RequisitionStatus.ProcessedWithError;
                r.CodeStatus = CodeStatus.InvalidToken;
                r.Messages.Add("User not authnticated");
            }
            else
            {
                r = Patrimonio.getPatrimonio();
            }
            return r;
        }
        [Authorize]
        [HttpPost]
        [Route("api/patrimonio/insert")]
        public BasicResponse InsertPatrimonio(PatrimonioInsert patrimonio)
        {
            BasicResponse r = new BasicResponse();
            var login = new BasicLogin(User.Identity);

            if (!login.isAtutenticated)
            {
                r.Status = RequisitionStatus.ProcessedWithError;
                r.CodeStatus = CodeStatus.InvalidToken;
                r.Messages.Add("User not authnticated");
            }
            else
            {
                r = Patrimonio.insertPatromonio(patrimonio);
            }
            return r;
        }
    }
}
