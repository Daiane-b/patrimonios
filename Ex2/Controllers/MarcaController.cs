using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tools;
using Actions;

namespace Ex2.Controllers
{
    public class MarcaController : ApiController
    {

        [Authorize]
        [HttpPost]
        [Route("api/marca/get")]
        public BasicResponse GetMarca()
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
                r = Marca.getMarca();
            }
            return r;
        }
        [Authorize]
        [HttpPost]
        [Route("api/marca/insert")]
        public BasicResponse InsertPatrimonio(MarcaInsert marca)
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
                r = Marca.insertMarca(marca);
            }
            return r;
        }
    }
}
