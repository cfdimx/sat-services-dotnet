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
        static string _connectionString;
        public ConsultaCFDIServiceEmulation(string connectionString)
        {
            _connectionString = connectionString;
        }
      

        public static Acuse GetLastUpdatedDocument(string rfcEmisor, string rfcReceptor, decimal total, string uuid)
        {

            using (ReceptionDAO reception = new ReceptionDAO(new Database(new SQLDatabase(_connectionString))))
            {
                Acuse acuse = new Acuse();
                Document query = reception.ConsultaCFDI(total, Guid.Parse(uuid), rfcReceptor, rfcEmisor);

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
                    var relas = relations.GetRelationsParents(query.UUID).ToArray();
                    if (relas.Length > 0)
                    {

                        if (relas.Any(w=> reception.GetDocumentByUUID(w.ParentUUID)?.Estado!="Cancelado" || w.DocumentType == "P") )
                        {
                            acuse.EsCancelable = "No cancelable";
                            query.EsCancelable = "No cancelable";
                        }




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
                                    query.EsCancelable = "Cancelable con aceptacion";
                                }
                                else
                                {
                                    acuse.EsCancelable = "Cancelable sin aceptacion";
                                    query.EsCancelable = "Cancelable sin aceptacion";
                                }
                            }
                            else
                            {
                                if (IsAutoCancel(query))
                                {
                                    using (CancelationDAO cancelation = new CancelationDAO(new Database(new SQLDatabase(_connectionString))))
                                    {
                                        cancelation.CancelDocument(query.UUID);
                                        
                                        pendings.DeletePending(query.UUID);
                                        query.Estado = "Cancelado";
                                        query.EstatusCancelacion = null;
                                    }
                                }
                                acuse.EsCancelable = "Cancelable con aceptacion";
                                query.EsCancelable = "Cancelable con aceptacion";
                            }
                        }
                    }
                    reception.UpdateDocument(query);
                    var updated = reception.GetDocumentByUUID(Guid.Parse(uuid));
                    acuse.CodigoEstatus = updated.CodigoEstatus;
                    acuse.Estado = updated.Estado;
                    acuse.EstatusCancelacion = updated.EstatusCancelacion;
                }
                return acuse;
            }
        }
        public Acuse Consulta(string expresionImpresa)
        {
            expresionImpresa = Regex.Replace(expresionImpresa, @"\t|\n|\r", "");
            expresionImpresa = expresionImpresa.Replace("&", "%26").Trim();
            expresionImpresa =  expresionImpresa.Replace("?","").Trim();
            
            var dict = HttpUtility.ParseQueryString(expresionImpresa);
            string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
            ExpresionImpresa respObj = JsonConvert.DeserializeObject<ExpresionImpresa>(json);
            

            return GetLastUpdatedDocument(respObj.re, respObj.rr, decimal.Parse(respObj.tt), respObj.id);
            

        }

        private static bool IsGenerericRFC(Document cfdi)
        {
            string generic_rfc = "XAXX010101000";
            return cfdi.RfcReceptor == generic_rfc;
        }
        private static bool IsForeignRFC(Document cfdi)
        {
            string generic_rfc = "XEXX010101000";
            return cfdi.RfcReceptor == generic_rfc;
        }
        private static bool IsMore5K(Document cfd)
        {
            return cfd.Total > 5000;
        }
        private static bool IsEgresosNomina(Document document)
        {
            return document.TipoComprobante == "E" || document.TipoComprobante == "N";
        }
        private static bool IsCancelledByTime(Document doc)
        {
            //TODO: esto es para no esperar 10 mins, al subir poner 10

            var minutes = (int)(GetCentralTime() - doc.FechaEmision).TotalMinutes;
            return minutes > 10;// <---- aqui
        }

        private static bool IsAutoCancel(Document doc)
        {
            //TODO: esto es para no esperar 15 minutos, al subir poner 15
            using (PendingsDAO pendings = new PendingsDAO(new Database(new SQLDatabase(_connectionString))))
            {
                var pending = pendings.GetPendingByUUID(doc.UUID);
                if (pending == null) return false;
                var minutes = (int)(GetCentralTime() - pending.FechaSolicitud).TotalMinutes;
                return minutes > 15;
            }
        }
        private static DateTime GetCentralTime()
        {
            var centralTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), centralTimeZone);
        }
      
    }
}
