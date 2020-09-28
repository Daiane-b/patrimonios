using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Tools;
using System.Data.Entity;


namespace Actions
{
    public class MarcaInsert
    {
        public String name { get; set; }
    }

    public class Marca
    {
        public int marcaId { get; set; }
        public String name { get; set; }

        public static BasicResponse getMarca()
        {
            BasicResponse r = new BasicResponse();
            try
            {
                using (ex2Context db = new ex2Context())
                {
                    List<Marca> marcas = new List<Marca>();

                    marcas = db.MARCA.Select(p => new Marca()
                    {
                        name = p.nome,                       
                        marcaId = p.marcaId
                    })
                    .ToList();

                    if (marcas.Count > 0)
                    {
                        r.Status = RequisitionStatus.Processed;
                        r.CodeStatus = CodeStatus.PerfectProcess;
                        r.Data = marcas;
                    }
                    else
                    {
                        r.Status = RequisitionStatus.Processed;
                        r.CodeStatus = CodeStatus.PerfectProcess;
                        r.Messages.Add("Não existem marcas cadastradas");
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

        public static BasicResponse insertMarca(MarcaInsert p)
        {
            BasicResponse r = new BasicResponse();
            try
            {
                using (ex2Context db = new ex2Context())
                {
                    int marcaId = 0;
                    var marcaExiste = db.MARCA.FirstOrDefault(pa => pa.nome == p.name);

                    int tombMax = 0;
                    if (marcaExiste == null)
                    {
                        MARCA novaMarca = new MARCA()
                        {

                            nome = p.name
                        };
                        db.MARCA.Add(novaMarca);
                        db.SaveChanges();
                        marcaId = novaMarca.marcaId;
                    }
                    else
                    {
                        marcaId = marcaExiste.marcaId;
                    }
                    r.Status = RequisitionStatus.Processed;
                    r.CodeStatus = CodeStatus.PerfectProcess;
                    r.Data = marcaId;
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
