using SAT.Core.DL.Implements.SQL.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.DAO.Reception
{
    public class ReceptionDAO : DAOBase
    {

        private static Object _lock = new Object();
        private static ReceptionDAO _instance = null;

        public ReceptionDAO(Database db):base(db)
        {
            
        }

        public static ReceptionDAO Instance(Database db)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ReceptionDAO(db);
                    }
                }
            }

            return _instance;
        }

        public Document GetDocumentByUUID(Guid uuid)
        {
            return (Document)_db.GetDocumentByUUID(uuid);
        }

        public Document ConsultaCFDI(string total, Guid uuid, string rfcReceptor, string rfcEmisor)
        {
            return (Document)_db.GetDocument(uuid,total, rfcReceptor, rfcEmisor);
        }
        public void UpdateDocument(Document doc)
        {
            _db.Update(doc);
            _db.Save();
        }


        public bool Receive(string xmlUrl,
            string codigoSatus,
            string esCancelable,
            string estado,
            string estatusCancelacion,
            Guid uuid,
            string tipoComprobante,
            string total,
            string rfcReceptor,
            string rfcEmisor,
            string versionComprobante,
            string numeroCertificado,
            string fechaEmision,
            string fechaTimbrado)
        {
            try
            {
                Document doc = new Document()
                {
                    XmlUrl = xmlUrl,
                    CodigoEstatus = codigoSatus,
                    EsCancelable = esCancelable,
                    Estado = estado,
                    EstatusCancelacion = estatusCancelacion,
                    UUID = uuid,
                    TipoComprobante = tipoComprobante,
                    Total = total,
                    RfcReceptor = rfcReceptor,
                    RfcEmisor = rfcEmisor,
                    VersionComprobante = versionComprobante,
                    NumeroCertificado = numeroCertificado,
                    FechaEmision = DateTime.Parse(fechaEmision),
                    FechaTimbrado = DateTime.Parse(fechaTimbrado)
                };
                
                _db.SaveDocument(doc);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
