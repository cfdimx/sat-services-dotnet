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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SAT.ConsultaCFDI
{
    public class ConsultaCFDIServiceEmulation : IConsultaCFDIServiceEmulation
    {
        string _connectionString;
        public ConsultaCFDIServiceEmulation(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Acuse Consulta(string expresionImpresa)
        {
            expresionImpresa = Regex.Replace(expresionImpresa, @"\t|\n|\r", "");
            Acuse acuse = new Acuse();
            var dict = HttpUtility.ParseQueryString(expresionImpresa);
            string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
            ExpresionImpresa respObj = JsonConvert.DeserializeObject<ExpresionImpresa>(json);

            using (ReceptionDAO reception = new ReceptionDAO(new Database(new SQLDatabase(_connectionString))))
            {
                Document query = reception.GetDocumentByUUID(Guid.Parse(respObj.id));
                if (query == null)
                {
                    acuse.Estado = "No encontrado";
                    acuse.CodigoEstatus = "N - 602: Comprobante no encontrado.";
                    acuse.EsCancelable = "";
                    acuse.EstatusCancelacion = "";
                    return acuse;
                }
                using (RelationsDAO relations = new RelationsDAO(new Database(new SQLDatabase(_connectionString))))
                {

                    if (relations.GetRelationsParents(query.UUID).ToArray().Length > 0)
                    {
                        acuse.EsCancelable = "No cancelable";

                    }
                    else
                    {
                        using (PendingsDAO pendings = new PendingsDAO(new Database(new SQLDatabase(_connectionString))))
                        {

                            if (pendings.GetPendingByUUID(query.UUID) == null)
                            {
                                if ((IsCancelledByTime(query) && !IsGenerericRFC(query) && !IsForeignRFC(query) && IsMore5K(query) && !IsEgresosNomina(query)) || query.EstatusCancelacion == "Solicitud rechazada")
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
                                    using (CancelationDAO cancelation = new CancelationDAO(new Database(new SQLDatabase(_connectionString))))
                                    {
                                        cancelation.CancelDocument(query.UUID);
                                    }
                                }
                                acuse.EsCancelable = "Cancelable con aceptacion";
                            }
                        }
                    }
                    var updated = reception.GetDocumentByUUID(Guid.Parse(respObj.id));
                    acuse.CodigoEstatus = updated.CodigoEstatus;
                    acuse.Estado = updated.Estado;
                    acuse.EstatusCancelacion = updated.EstatusCancelacion;
                }
                return acuse;
            }

        }

        private bool IsGenerericRFC(Document cfdi)
        {
            string generic_rfc = "XAXX010101000";
            return cfdi.RfcReceptor == generic_rfc;
        }
        private bool IsForeignRFC(Document cfdi)
        {
            string generic_rfc = "XEXX010101000";
            return cfdi.RfcReceptor == generic_rfc;
        }
        private bool IsMore5K(Document cfd)
        {
            return decimal.Parse(cfd.Total) > 5000;
        }
        private bool IsEgresosNomina(Document document)
        {
            return document.TipoComprobante == "E" || document.TipoComprobante == "N";
        }
        private bool IsCancelledByTime(Document doc)
        {
            //TODO: esto es para no esperar 10 mins, al subir poner 10

            var minutes = (int)(GetCentralTime() - doc.FechaEmision).TotalMinutes;
            return minutes > 10;// <---- aqui
        }

        private bool IsAutoCancel(Document doc)
        {
            //TODO: esto es para no esperar 15 minutos, al subir poner 15
            using (PendingsDAO pendings = new PendingsDAO(new Database(new SQLDatabase(_connectionString))))
            {
                var pending = pendings.GetPendingByUUID(doc.UUID);
                if (pending == null) return false;
                var minutes = (int)(GetCentralTime() - pending.FechaSolicitud).TotalMinutes;
                return minutes > 15;//<----------- aqui
            }
        }
        private DateTime GetCentralTime()
        {
            TimeZoneInfo setTimeZoneInfo;
            setTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTime(DateTime.Now, setTimeZoneInfo);
        }
    }
}
