using SAT.Core.DL.DAO.Reception;
using SAT.Core.DL.DAO.Relations;
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
       
        private RelationsDAO _relations;
        private ReceptionDAO _receptions;
        public CfdiConsultaRelacionadosServiceEmulation(RelationsDAO relations, ReceptionDAO receptions)
        {
           
            _relations = relations;
            _receptions = receptions;
            
        }
        public ConsultaRelacionados ProcesarRespuesta(PeticionConsultaRelacionados solicitud)
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
            foreach (var child in _relations.GetRelationsChildren(solicitud.Uuid))
            {
                var doc = _receptions.GetDocumentByUUID(child.UUID);
                var rfcEmisor = doc.RfcEmisor;
                var rfcReceptor = doc.RfcReceptor;
                var uid = doc.UUID;
                childrens.Add(new UuidRelacionado() { RfcEmisor = rfcEmisor, RfcReceptor = rfcReceptor, Uuid = uid });
            }
            foreach (var parent in _relations.GetRelationsParents(solicitud.Uuid))
            {
                var doc = _receptions.GetDocumentByUUID(parent.UUID);
                var rfcEmisor = doc.RfcEmisor;
                var rfcReceptor = doc.RfcReceptor;
                var uid = parent.ParentUUID;
                parents.Add(new UuidPadre() { RfcEmisor = rfcEmisor, RfcReceptor = rfcReceptor, Uuid = uid });
            }
            if(childrens.ToArray().Length == 0 && parents.ToArray().Length == 0)
            {
                return new ConsultaRelacionados()
                {
                    Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - folio físcal: {solicitud.Uuid} - Clave: 2001 - No Existen cfdi relacionados al folio fiscal",
                    UuidConsultado = solicitud.Uuid,
                    UuidsRelacionadosHijos = new UuidRelacionado[] {},
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
