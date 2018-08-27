using System;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAT.CfdiConsultaRelacionados;
using SAT.Core;

namespace SAT.CfdiConsultaRelacionados_UT
{
    [TestClass]
    public class UT_CfdiConsultaRelacionados
    {
        ICfdiConsultaRelacionadosServiceEmulation _service;
        const string uuid = "fbba9c71-90dc-4dc4-8e8e-1bfa9482dcb2";
        const string rfcReceptor = "LAN7008173R5";
        const string rfcPacEnviaSolicitud = "AAA010101AAA";
        readonly PeticionConsultaRelacionados _solicitud;
        public UT_CfdiConsultaRelacionados()
        {
            _service = new CfdiConsultaRelacionadosServiceEmulation();
            _solicitud = Core.Helpers.XmlHelper.DeserealizeDocument<PeticionConsultaRelacionados>(
                Core.Helpers.XmlHelper.RemoveInvalidCharsXml(File.ReadAllText(@"Resources\Solicitud.xml")));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Core.Helpers.XmlHelper.RemoveInvalidCharsXml(File.ReadAllText(@"Resources\Solicitud.xml")));
        }

        [TestMethod]
        public void UT_CfdiConsultaRelacionados_301_Rfc_Undefined()
        {
            PeticionConsultaRelacionados solicitud = new PeticionConsultaRelacionados()
            {
                RfcPacEnviaSolicitud = rfcPacEnviaSolicitud,                
                Uuid = uuid,
                Signature = _solicitud.Signature
            };
            var response = _service.ProcesarRespuesta(solicitud);
            Assert.IsTrue(response.Resultado.Contains("Clave: 301 - Error: La solicitud no tiene definido el RFC Receptor"));
        }
        [TestMethod]
        public void UT_CfdiConsultaRelacionados_301_Rfc_Invalid()
        {
            PeticionConsultaRelacionados solicitud = new PeticionConsultaRelacionados()
            {
                RfcPacEnviaSolicitud = rfcPacEnviaSolicitud,
                RfcReceptor = "ABC",
                Uuid = uuid,
                Signature = _solicitud.Signature
            };
            var response = _service.ProcesarRespuesta(solicitud);
            Assert.IsTrue(response.Resultado.Contains("Clave: 301 - Error: El formato del RFC del receptor proporcionado no es válido."));
        }

        [TestMethod]
        public void UT_CfdiConsultaRelacionados_301_UUID_Invalid()
        {
            PeticionConsultaRelacionados solicitud = new PeticionConsultaRelacionados()
            {
                RfcPacEnviaSolicitud = rfcPacEnviaSolicitud,
                RfcReceptor = rfcReceptor,
                Uuid = "123",
                Signature = _solicitud.Signature
            };
            var response = _service.ProcesarRespuesta(solicitud);
            Assert.IsTrue(response.Resultado.Contains("UUID:  - Clave: 301 - Error: Solicitud invalida, el uuid de la peticion no posee el formato correcto."));
        }

        [TestMethod]
        public void UT_CfdiConsultaRelacionados_2000_OK()
        {
            //var isValid = SAT.Core.Helpers.SignatureHelper.ValidateSignatureXml(
            //    Core.Helpers.XmlHelper.RemoveInvalidCharsXml(File.ReadAllText(@"Resources\Solicitud.xml")));
            var response = _service.ProcesarRespuesta(_solicitud);
            Assert.IsTrue(response.Resultado.Contains("Clave: 2000 - Se encontraron CFDI relacionados"));
        }

        [TestMethod]        
        public void UT_CfdiConsultaRelacionados_InvalidSignature_Exception()
        {            
            //_solicitud.Signature.SignatureValue = Encoding.UTF8.GetBytes("HolaMundo");
            PeticionConsultaRelacionados solicitud = new PeticionConsultaRelacionados()
            {
                RfcPacEnviaSolicitud = rfcPacEnviaSolicitud,
                RfcReceptor = rfcReceptor,
                Uuid = uuid,
                Signature = _solicitud.Signature
            };
            var response = _service.ProcesarRespuesta(solicitud);
            Assert.IsTrue(response.Resultado.Contains("Clave: 1003 - Los datos de la firma no son correctos."));
        }

        [TestMethod]
        public void UT_CfdiConsultaRelacionados_1003()
        {
            //_solicitud.Signature.SignatureValue = Encoding.UTF8.GetBytes("HolaMundo");
            PeticionConsultaRelacionados solicitud = new PeticionConsultaRelacionados()
            {
                RfcPacEnviaSolicitud = _solicitud.RfcPacEnviaSolicitud,
                RfcReceptor = "AAN7008173R5",
                Uuid = _solicitud.Uuid,
                Signature = _solicitud.Signature
            };
            var response = _service.ProcesarRespuesta(solicitud);
            Assert.IsTrue(response.Resultado.Contains("Clave: 1003 - Error: Los datos del certificado no son correctos"));
        }

        [TestMethod]
        public void UT_CfdiConsultaRelacionados_302()
        {
            //_solicitud.Signature.SignatureValue = Encoding.UTF8.GetBytes("HolaMundo");
            PeticionConsultaRelacionados solicitud = new PeticionConsultaRelacionados()
            {
                RfcPacEnviaSolicitud = _solicitud.RfcPacEnviaSolicitud,
                RfcReceptor = _solicitud.RfcReceptor,
                Uuid = _solicitud.Uuid,
                Signature = _solicitud.Signature
            };
            solicitud.Signature.KeyInfo.X509Data.X509Certificate = null;
            var response = _service.ProcesarRespuesta(solicitud);
            Assert.IsTrue(response.Resultado.Contains("Clave: 302 - Error: No se cuenta con al menos una firma en el documento"));
        }
    }
}
