using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Tools;
using System.Data;
using System.Security.Cryptography;

namespace Actions
{
    public class UsuarioInsert
    {
        public String name { get; set; }
        public String email { get; set; }
        public String password { get; set; }
    }

    public class Usuario
    {
        public int usuarioId { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public String password { get; set; }

        public static BasicResponse getUsuario()
        {
            BasicResponse r = new BasicResponse();
            try
            {
                using (ex2Context db = new ex2Context())
                {
                    List<Usuario> usuarios = new List<Usuario>();
                    //TODO: criptografar a senha
                    usuarios = db.USUARIO.Select(p => new Usuario()
                    {
                        usuarioId = p.idUsuario,
                        name = p.nome,
                        email = p.email,
                        password = p.senha
                    })
                    .ToList();

                    if (usuarios.Count > 0)
                    {
                        r.Status = RequisitionStatus.Processed;
                        r.CodeStatus = CodeStatus.PerfectProcess;
                        r.Data = usuarios;
                    }
                    else
                    {
                        r.Status = RequisitionStatus.Processed;
                        r.CodeStatus = CodeStatus.PerfectProcess;
                        r.Messages.Add("Não existem usuarios cadastrados");
                    }
                }

            }
            catch (Exception ex)
            {
                r.Status = RequisitionStatus.Unprocessed;
                r.CodeStatus = CodeStatus.ProcessException;
                r.Messages.Add($"Error: {ex.Message}");
                r.Messages.Add(ex.InnerException?.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "");
            }
            return r;
        }
        public static BasicResponse insertUsuario(UsuarioInsert u)
        {
            BasicResponse r = new BasicResponse();
            try
            {
                using (ex2Context db = new ex2Context())
                {
                    int userId = 0;
                    var usuarioExiste = db.USUARIO.FirstOrDefault(us => us.email == u.email);

                    if (usuarioExiste == null)
                    {
                        //TODO: criptografar a senha
                        USUARIO novoUsuario = new USUARIO()
                        {
                            
                            nome = u.name,
                            email = u.email,
                            senha = u.password
                        };
                        db.USUARIO.Add(novoUsuario);
                        db.SaveChanges();
                        userId = novoUsuario.idUsuario;
                    }
                    else
                    {
                        userId = usuarioExiste.idUsuario;
                        r.Messages.Add("Usuário já cadastrado");
                    }
                    r.Status = RequisitionStatus.Processed;
                    r.CodeStatus = CodeStatus.PerfectProcess;
                    r.Data = userId;
                }

            }
            catch (Exception ex)
            {
                r.Status = RequisitionStatus.Unprocessed;
                r.CodeStatus = CodeStatus.ProcessException;
                r.Messages.Add($"Error: {ex.Message}");
                r.Messages.Add(ex.InnerException?.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? "");
            }
            return r;
        }
    }
}
