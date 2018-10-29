using SAT.Core.DL.DAO.Reception;
using SAT.Core.DL.DAO.Relations;
using SAT.Core.DL.Implements.SQL;
using SAT.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CfdiConsultaRelacionados
{
    public class CfdiConsultaRelacionadosServiceEmulation : ICfdiConsultaRelacionadosServiceEmulation
    {
       
    
        private  string _connectionString;
        public CfdiConsultaRelacionadosServiceEmulation(string connectionString)
        {
            _connectionString = connectionString;
            
            
        }
        public ConsultaRelacionados ProcesarRespuesta(PeticionConsultaRelacionados solicitud)
        {
            using (RelationsDAO _relations = new RelationsDAO(new Core.DL.Database(new SQLDatabase(_connectionString))))
            {
                using (ReceptionDAO _receptions = new ReceptionDAO(new Core.DL.Database(new SQLDatabase(_connectionString))))
                {

                    Guid uuid = Guid.Empty;
                    if (solicitud == null || string.IsNullOrEmpty(solicitud.RfcReceptor))
                        return new ConsultaRelacionados()
                        {
                            UuidConsultado = solicitud.Uuid,
                            Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor}  -UUID: {solicitud.Uuid} - Clave: 301 - Error: La solicitud no tiene definido el RFC Receptor"
                        };

                    if (!Core.Helpers.FiscalHelper.IsTaxIdValid(solicitud.RfcReceptor))
                        return new ConsultaRelacionados()
                        {
                            UuidConsultado = solicitud.Uuid,
                            Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - UUID: {solicitud.Uuid} - Clave: 301 - Error: El formato del RFC del receptor proporcionado no es válido."
                        };

                    if (!Guid.TryParse(solicitud.Uuid, out uuid))
                        return new ConsultaRelacionados()
                        {
                            UuidConsultado = solicitud.Uuid,
                            Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - UUID:  - Clave: 301 - Error: Solicitud invalida, el uuid de la peticion no posee el formato correcto."
                        };
                    //Validate RFCReceptor with Certificate
                    var xmlDocument = XmlMessageSerializer.SerializeDocumentToXml(solicitud);
                    var certificate = XmlHelper.GetCertificateFromXml(xmlDocument);
                    var signature = XmlHelper.GetSignatureFromXml(xmlDocument);
                    if (certificate == null || string.IsNullOrEmpty(signature))
                        return new ConsultaRelacionados()
                        {
                            UuidConsultado = solicitud.Uuid,
                            Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - UUID: {solicitud.Uuid} - Clave: 302 - Error: No se cuenta con al menos una firma en el documento."
                        };


                    var taxIdInfo = SignatureHelper.CertificateGetInfoTaxId(certificate);
                    var d =  _receptions.GetDocumentByUUID(Guid.Parse(solicitud.Uuid));
                    if (taxIdInfo.taxId != d.RfcReceptor)
                    {
                        return new ConsultaRelacionados()
                        {
                            UuidConsultado = solicitud.Uuid,
                            Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - UUID: {solicitud.Uuid} - Clave: 2002 - Error: El RFC no corresponde al UUID consultado."
                        };
                    }

                    if (certificate != null && taxIdInfo.taxId != solicitud.RfcReceptor)
                        return new ConsultaRelacionados()
                        {
                            UuidConsultado = solicitud.Uuid,
                            Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - UUID: {solicitud.Uuid} - Clave: 1003 - Error: Los datos del certificado no son correctos [Rfc: { solicitud.RfcReceptor } &lt;=> Certificado.Rfc: {taxIdInfo.taxId} &lt;=> Certificado.RfcRepresentanteLegal: {taxIdInfo.legalTaxId}]."
                        };

                    //Validate Signature
                    bool isValid = SAT.Core.Helpers.SignatureHelper.ValidateSignatureXml(xmlDocument);
                    if (!isValid)
                    {
                        return new ConsultaRelacionados()
                        {
                            UuidConsultado = solicitud.Uuid,
                            Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - UUID: {solicitud.Uuid} - Clave: 1003 - Los datos de la firma no son correctos. - Warning: Esta validación es del servicio de Emulación. -"
                        };
                    }

                    List<UuidRelacionado> childrens = new List<UuidRelacionado>();
                    List<UuidPadre> parents = new List<UuidPadre>();

                    foreach (var child in _relations.GetRelationsChildren(Guid.Parse(solicitud.Uuid)))
                    {
                        var doc = _receptions.GetDocumentByUUID(child.UUID);
                        if (doc != null)
                        {
                            var rfcEmisor = doc.RfcEmisor;
                            var rfcReceptor = doc.RfcReceptor;
                            var uid = doc.UUID;
                            childrens.Add(new UuidRelacionado() { RfcEmisor = rfcEmisor, RfcReceptor = rfcReceptor, Uuid = uid.ToString() });
                        }
                        
                    }
                    foreach (var parent in _relations.GetRelationsParents(Guid.Parse(solicitud.Uuid)))
                    {
                        var doc = _receptions.GetDocumentByUUID(parent.UUID);
                        if (doc != null)
                        {
                            var rfcEmisor = doc.RfcEmisor;
                            var rfcReceptor = doc.RfcReceptor;
                            var uid = parent.ParentUUID;
                            parents.Add(new UuidPadre() { RfcEmisor = rfcEmisor, RfcReceptor = rfcReceptor, Uuid = uid.ToString() });
                        }
                        
                    }
                    if (childrens.ToArray().Length == 0 && parents.ToArray().Length == 0)
                    {
                        return new ConsultaRelacionados()
                        {
                            Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - folio físcal: {solicitud.Uuid} - Clave: 2001 - No Existen cfdi relacionados al folio fiscal",
                            UuidConsultado = solicitud.Uuid,
                            UuidsRelacionadosHijos = new UuidRelacionado[] { },
                            UuidsRelacionadosPadres = new UuidPadre[] { }
                        };

                    }

                    return new ConsultaRelacionados()
                    {
                        Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - folio físcal: {solicitud.Uuid} - Clave: 2000 - Se encontraron CFDI relacionados",
                        UuidConsultado = solicitud.Uuid,
                        UuidsRelacionadosHijos = childrens.ToArray(),
                        UuidsRelacionadosPadres = parents.ToArray()
                    };
                }
            }
        }
    }
}
