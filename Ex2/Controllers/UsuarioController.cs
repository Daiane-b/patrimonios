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
    public class UsuarioController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("api/usuario/get")]
        public BasicResponse GetUsuario()
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
                r = Usuario.getUsuario();
            }
            return r;
        }
        [Authorize]
        [HttpPost]
        [Route("api/usuario/insert")]
        public BasicResponse InsertUsuario(UsuarioInsert usuario)
        {
            BasicResponse r = new BasicResponse();
            //var login = new BasicLogin(User.Identity);

            //if (!login.isAtutenticated)
            //{
            //    r.Status = RequisitionStatus.ProcessedWithError;
            //    r.CodeStatus = CodeStatus.InvalidToken;
            //    r.Messages.Add("User not authnticated");
            //}
            //else
            //{
                r = Usuario.insertUsuario(usuario);
            //}
            return r;
        }
    }
}
