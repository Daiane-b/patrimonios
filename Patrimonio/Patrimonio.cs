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
    public class PatrimonioInsert
    {
        public String name { get; set; }
        public String description { get; set; }
        public int marcaId { get; set; }
    }

        public class Patrimonio
        {
            public int patrimonioId { get; set; }
            public String name { get; set; }
            public String description { get; set; }
            public int numberT { get; set; }
            public int marcaId { get; set; }

            public static BasicResponse getPatrimonio()
            {
                BasicResponse r = new BasicResponse();
                try
                {
                    using (ex2Context db = new ex2Context())
                    {
                        List<Patrimonio> patrimonios = new List<Patrimonio>();

                        patrimonios = db.PATRIMONIO.Select(p => new Patrimonio()
                        {
                            patrimonioId = p.patrimonioId,
                            name = p.nome,
                            description = p.descricao,
                            numberT = p.nTombo,
                            marcaId = p.marcaId
                        })
                        .ToList();

                        if (patrimonios.Count > 0)
                        {
                            r.Status = RequisitionStatus.Processed;
                            r.CodeStatus = CodeStatus.PerfectProcess;
                            r.Data = patrimonios;
                        }
                        else
                        {
                            r.Status = RequisitionStatus.Processed;
                            r.CodeStatus = CodeStatus.PerfectProcess;
                            r.Messages.Add("Não existem patrimonios cadastrados");
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

            public static BasicResponse insertPatromonio(PatrimonioInsert p)
            {
                BasicResponse r = new BasicResponse();
                try
                {
                    using (ex2Context db = new ex2Context())
                    {
                        int patrimonioId = 0;
                        var patrimonioExiste = db.PATRIMONIO.FirstOrDefault(pa => pa.nome == p.name && pa.marcaId == p.marcaId && pa.descricao == p.description);

                        if (patrimonioExiste == null)
                        {
                            PATRIMONIO novoPatrimonio = new PATRIMONIO()
                            {
                                
                                nome = p.name,
                                descricao = p.description,
                                marcaId = p.marcaId
                            };
                            db.PATRIMONIO.Add(novoPatrimonio);
                            db.SaveChanges();
                            patrimonioId = novoPatrimonio.patrimonioId;
                        }
                        else
                        {
                            patrimonioId = patrimonioExiste.patrimonioId;
                        }
                        r.Status = RequisitionStatus.Processed;
                        r.CodeStatus = CodeStatus.PerfectProcess;
                        r.Data = patrimonioId;
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
