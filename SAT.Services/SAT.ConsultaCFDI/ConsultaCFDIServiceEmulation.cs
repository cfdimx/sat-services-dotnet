using Newtonsoft.Json;
using SAT.ConsultaCFDI.Models;
using SAT.Core.DL;
using SAT.Core.DL.DAO.Cancelation;
using SAT.Core.DL.DAO.Pendings;
using SAT.Core.DL.DAO.Reception;
using SAT.Core.DL.DAO.Relations;
using SAT.Core.DL.Implements.SQL;
using SAT.Core.DL.Implements.SQL.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SAT.ConsultaCFDI
{
    public class ConsultaCFDIServiceEmulation : IConsultaCFDIServiceEmulation
    {

        
        
        private ReceptionDAO _reception;
        private CancelationDAO _cancelation;
        private PendingsDAO _pendings;
        private RelationsDAO _relations;
        public ConsultaCFDIServiceEmulation(ReceptionDAO reception, CancelationDAO cancelation, PendingsDAO pendings, RelationsDAO relations)
        {
            _pendings = pendings;
            _cancelation = cancelation;
            _reception = reception;
            _relations = relations;
        }
        public Acuse Consulta(string expresionImpresa)
        {
            Acuse acuse = new Acuse();
            var dict = HttpUtility.ParseQueryString(expresionImpresa);
            string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
            ExpresionImpresa respObj = JsonConvert.DeserializeObject<ExpresionImpresa>(json);
            //Document query = _reception.ConsultaCFDI(respObj.tt,respObj.id, respObj.rr, respObj.re);
            Document query = _reception.GetDocumentByUUID(respObj.id);
            if (_relations.GetRelationsParents(query.UUID).ToArray().Length > 0)
            {
                acuse.EsCancelable = "No cancelable";
            }
            else
            {
                if (_pendings.GetPendingByUUID(query.UUID) == null)
                {
                    if (IsCancelledByTime(query))
                    {
                        acuse.EsCancelable = "Cancelable con aceptacion";


                    }

                    else
                    {
                        acuse.EsCancelable = "Cancelable sin aceptacion";


                    }
                }

                else
                {
                    if (IsAutoCancel(query))
                    {

                        _cancelation.CancelDocument(query.UUID);
                    }
                    acuse.EsCancelable = "Cancelable con aceptacion";
                }
            }
            
            

            
            var updated = _reception.GetDocumentByUUID(respObj.id);
            acuse.CodigoEstatus = updated.CodigoEstatus;
            acuse.Estado = updated.Estado;
            acuse.EstatusCancelacion = updated.EstatusCancelacion;
            
            return acuse;
        }

        

        private bool IsCancelledByTime(Document doc)
        {
            //TODO: esto es para no esperar 10 mins, al subir poner 10
            var minutes = (int)(DateTime.Now - doc.FechaEmision ).TotalMinutes;
            return minutes > 10;// <---- aqui
        }

        private bool IsAutoCancel(Document doc)
        {
            //TODO: esto es para no esperar 15 minutos, al subir poner 15
            var pending = _pendings.GetPendingByUUID(doc.UUID);
            if (pending == null) return false;
            var minutes = (int)(DateTime.Now - pending.FechaSolicitud).TotalMinutes;
            return minutes > 15;//<----------- aqui
        }
    }
}
