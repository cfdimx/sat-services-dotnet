using SAT.Core.DL;
using SAT.Core.DL.DAO.Reception;
using SAT.Core.DL.DAO.Relations;
using SAT.Core.DL.Implements.SQL;
using SAT.Core.DL.Implements.SQL.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SAT.RecibeCFDI
{
    public class RecibeServiceEmulation : IRecibeServiceEmulation
    {
        
        private string SAS;
        private RelationsDAO _relations;
        private ReceptionDAO _reception;
        public RecibeServiceEmulation(RelationsDAO relations, ReceptionDAO receptions, string SASc )
        {
            SAS = SASc;
            _relations = relations;
            _reception = receptions;
        }


        public AcuseRecepcion Recibe(CFDI cFDI)
        {

            try
            {
                XmlDocument xml = GetXml(cFDI.RutaCFDI);
                if (xml == null) throw new XmlException();
                var root = xml.DocumentElement;
                SaveDocument(cFDI, root);
                return new AcuseRecepcion()
                {
                    AcuseRecepcionCFDI = new Acuse()
                    {
                        UUID = cFDI.EncabezadoCFDI.UUID,
                        CodEstatus = "Comprobante recibido satisfactoriamente",
                        Fecha = DateTime.Now,
                        NoCertificadoSAT = root["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].GetAttribute("NoCertificadoSAT"),
                        
            }
                };
            }
            catch (XmlException)
            {
                return SendError("502", cFDI);
            }
            
        }
        private AcuseRecepcion SendError(string error, CFDI cFDI)
        {
            List<IncidenciaAcuseRecepcion> incidencias = new List<IncidenciaAcuseRecepcion>();
            
            IncidenciaAcuseRecepcion incidencia = new IncidenciaAcuseRecepcion()

            {
                CodigoError = error,
                FechaRegistro = new DateTime(),
                IdIncidencia = Guid.NewGuid(),
                RfcEmisor = cFDI.EncabezadoCFDI.RfcEmisor
            };
            incidencias.Add(incidencia);
            return new AcuseRecepcion()
            {
                AcuseRecepcionCFDI = new Acuse()
                {
                    CodEstatus = error,
                    Fecha = new DateTime(),
                    Incidencia = incidencias.ToArray()
                }
            };
        }
        private void SaveDocument(CFDI cFDI, XmlElement root)
        {
           
            string rfc_emisor = root["cfdi:Emisor"].GetAttribute("Rfc");
            string rfc_receptor = root["cfdi:Receptor"].GetAttribute("Rfc");
            string total = root.GetAttribute("Total");
            string uuid = root["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].GetAttribute("UUID");
            string tipo_comprobante = root.GetAttribute("TipoDeComprobante");
            string noCertificado = root.GetAttribute("NoCertificado");
            string version = root.GetAttribute("Version");
            string fecha_emision = root.GetAttribute("Fecha");
            string fecha_timbrado = root["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].GetAttribute("FechaTimbrado");
            var relations = root["cfdi:CfdiRelacionados"];
            if(relations != null)
            {
                string RelationsType = relations.GetAttribute("TipoRelacion");
                string[] RelationsUUIDS = GetRelations(relations.GetElementsByTagName("cfdi:CfdiRelacionado"));

                if (RelationsUUIDS.Length != 0)
                {
                    SaveRelations(RelationsType, uuid, RelationsUUIDS);
                }
            }
           
           
            _reception.Receive(cFDI.RutaCFDI,
                "S - Comprobante obtenido satisfactoriamente",
                "Cancelable sin Aceptacion",
                "Vigente",
                null,
                uuid,
                tipo_comprobante,
                total,
                rfc_receptor,
                rfc_emisor,
                version,
                noCertificado,
                fecha_emision,
                fecha_timbrado);
        }

        
       private string GetEsCancelable(XmlElement root)
        {
            string rfc_emisor = root["cfdi:Emisor"].GetAttribute("Rfc");
            string rfc_receptor = root["cfdi:Receptor"].GetAttribute("Rfc");
            string total = root.GetAttribute("Total");
            string uuid = root["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].GetAttribute("UUID");
            string tipo_comprobante = root.GetAttribute("TipoDeComprobante");
            string noCertificado = root.GetAttribute("NoCertificado");
            string version = root.GetAttribute("Version");
            string fecha_emision = root.GetAttribute("Fecha");
            string fecha_timbrado = root["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].GetAttribute("FechaTimbrado");
            
            if (float.Parse(total) <= 5000 &&
                (tipo_comprobante == "N" || tipo_comprobante == "E" || tipo_comprobante == "T" ) &&
                rfc_receptor == "XAXX010101000")
            {
                return "Cancelable sin Aceptacion";
            }

            //TODO: agregar la validacion para cancelados
            else if (tipo_comprobante == "P")
            {
                return "No Cancelable";
            }
            return "Cancelable con Aceptacion";
        }

      


        private void SaveRelations(string relationType,string parentUUID, string[] uuids)
        {
            foreach (string uuid in uuids)
            {
                _relations.SaveRelations(uuid, parentUUID, relationType);
            }
        }

        private string[] GetRelations(XmlNodeList relations)
        {
            List<string> rels = new List<string>();
            foreach(XmlElement r in relations)
            {
                rels.Add(r.GetAttribute("UUID"));
            }

            return rels.ToArray();
        }

        private XmlDocument GetXml(string source)
        {
            try
            {
                source = source + SAS;
                string xmlStr;
                using (var wc = new WebClient())
                {
                    xmlStr = wc.DownloadString(source);
                }
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlStr);

                return xmlDoc;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
