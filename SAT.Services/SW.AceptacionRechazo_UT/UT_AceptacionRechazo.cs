using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAT.AceptacionRechazo;
using SAT.Core;
using SAT.Core.DL;
using SAT.Core.DL.DAO.Cancelation;
using SAT.Core.DL.DAO.Pendings;
using SAT.Core.DL.DAO.Reception;
using SAT.Core.DL.DAO.Relations;
using SAT.Core.DL.Implements.SQL;
using SAT.Core.DL.Implements.SQL.Repository.Entities;
using SAT.RecibeCFDI;

namespace SAT.AceptacionRechazo_UT
{
    [TestClass]
    public class UT_AceptacionRechazo
    {
        CancelationAceptacionRechazoServiceEmulation _service;
        RecibeServiceEmulation _receptionService;
        const string uuid = "fbba9c71-90dc-4dc4-8e8e-1bfa9482dcb2";
        const string rfcReceptor = "LAN7008173R5";
        const string rfcPacEnviaSolicitud = "AAA010101AAA";
        const string solicitudXML = "<?xml version='1.0' encoding='utf-8'?><SolicitudAceptacionRechazo xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' RfcReceptor='LAN7008173R5' RfcPacEnviaSolicitud='DAL050601L35' Fecha='2018-08-24T09:50:39' xmlns='http://cancelacfd.sat.gob.mx'><Folios><UUID>8f19c224-ad3e-4b97-a1e6-e82f000f0e21</UUID><Respuesta>Aceptacion</Respuesta></Folios><Signature xmlns='http://www.w3.org/2000/09/xmldsig#'><SignedInfo><CanonicalizationMethod Algorithm='http://www.w3.org/TR/2001/REC-xml-c14n-20010315' /><SignatureMethod Algorithm='http://www.w3.org/2000/09/xmldsig#rsa-sha1' /><Reference URI=''><Transforms><Transform Algorithm='http://www.w3.org/2000/09/xmldsig#enveloped-signature' /></Transforms><DigestMethod Algorithm='http://www.w3.org/2000/09/xmldsig#sha1' /><DigestValue>V+kZEZeAXsvau47407nDsbtABSw=</DigestValue></Reference></SignedInfo><SignatureValue>NQ5dZDvXo1MWiG/9/OSjJ3W+0Smfl7eEf9fhbb9CJXQIhtJ9/ZMf35oZXJqpI3vWiMuuU//nLUo7AgxK+NBaVmX+EHbXh2UxQhKwnqBX62ikU0aEjey0OcfPpdhJHb6DOYEJFVB23ZroiKSmtyvY2FeLUgvoQJRnrDxNE05XTAT6vFxSe+XI6Dn9VYgDCtu1nC9YNkIGdSD2F5kVapdGf7wfubvYrBvqhwcYuTXMLMOGINqzpeZLaaIBIH/Kz0Q4kqy/aQG/R+703lDivnsMFEfdMtTMhlOQEWO/mehjqB0tiIJ9dw7o17rO0x8tc7tQiYztPvnnyNyZRINPqZ6D5Q==</SignatureValue><KeyInfo><X509Data><X509IssuerSerial><X509IssuerName>OID.1.2.840.113549.1.9.2=Responsable: ACDMA, OID.2.5.4.45=SAT970701NN3, L=Coyoacán, S=Distrito Federal, C=MX, PostalCode=06300, STREET='Av. Hidalgo 77, Col. Guerrero', E=asisnet@pruebas.sat.gob.mx, OU=Administración de Seguridad de la Información, O=Servicio de Administración Tributaria, CN=A.C. 2 de pruebas(4096)</X509IssuerName><X509SerialNumber>3230303031303030303030333030303232383135</X509SerialNumber></X509IssuerSerial><X509Certificate>MIIFxTCCA62gAwIBAgIUMjAwMDEwMDAwMDAzMDAwMjI4MTUwDQYJKoZIhvcNAQELBQAwggFmMSAwHgYDVQQDDBdBLkMuIDIgZGUgcHJ1ZWJhcyg0MDk2KTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMSkwJwYJKoZIhvcNAQkBFhphc2lzbmV0QHBydWViYXMuc2F0LmdvYi5teDEmMCQGA1UECQwdQXYuIEhpZGFsZ28gNzcsIENvbC4gR3VlcnJlcm8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQRGlzdHJpdG8gRmVkZXJhbDESMBAGA1UEBwwJQ295b2Fjw6FuMRUwEwYDVQQtEwxTQVQ5NzA3MDFOTjMxITAfBgkqhkiG9w0BCQIMElJlc3BvbnNhYmxlOiBBQ0RNQTAeFw0xNjEwMjUyMTUyMTFaFw0yMDEwMjUyMTUyMTFaMIGxMRowGAYDVQQDExFDSU5ERU1FWCBTQSBERSBDVjEaMBgGA1UEKRMRQ0lOREVNRVggU0EgREUgQ1YxGjAYBgNVBAoTEUNJTkRFTUVYIFNBIERFIENWMSUwIwYDVQQtExxMQU43MDA4MTczUjUgLyBGVUFCNzcwMTE3QlhBMR4wHAYDVQQFExUgLyBGVUFCNzcwMTE3TURGUk5OMDkxFDASBgNVBAsUC1BydWViYV9DRkRJMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgvvCiCFDFVaYX7xdVRhp/38ULWto/LKDSZy1yrXKpaqFXqERJWF78YHKf3N5GBoXgzwFPuDX+5kvY5wtYNxx/Owu2shNZqFFh6EKsysQMeP5rz6kE1gFYenaPEUP9zj+h0bL3xR5aqoTsqGF24mKBLoiaK44pXBzGzgsxZishVJVM6XbzNJVonEUNbI25DhgWAd86f2aU3BmOH2K1RZx41dtTT56UsszJls4tPFODr/caWuZEuUvLp1M3nj7Dyu88mhD2f+1fA/g7kzcU/1tcpFXF/rIy93APvkU72jwvkrnprzs+SnG81+/F16ahuGsb2EZ88dKHwqxEkwzhMyTbQIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAJ/xkL8I+fpilZP+9aO8n93+20XxVomLJjeSL+Ng2ErL2GgatpLuN5JknFBkZAhxVIgMaTS23zzk1RLtRaYvH83lBH5E+M+kEjFGp14Fne1iV2Pm3vL4jeLmzHgY1Kf5HmeVrrp4PU7WQg16VpyHaJ/eonPNiEBUjcyQ1iFfkzJmnSJvDGtfQK2TiEolDJApYv0OWdm4is9Bsfi9j6lI9/T6MNZ+/LM2L/t72Vau4r7m94JDEzaO3A0wHAtQ97fjBfBiO5M8AEISAV7eZidIl3iaJJHkQbBYiiW2gikreUZKPUX0HmlnIqqQcBJhWKRu6Nqk6aZBTETLLpGrvF9OArV1JSsbdw/ZH+P88RAt5em5/gjwwtFlNHyiKG5w+UFpaZOK3gZP0su0sa6dlPeQ9EL4JlFkGqQCgSQ+NOsXqaOavgoP5VLykLwuGnwIUnuhBTVeDbzpgrg9LuF5dYp/zs+Y9ScJqe5VMAagLSYTShNtN8luV7LvxF9pgWwZdcM7lUwqJmUddCiZqdngg3vzTactMToG16gZA4CWnMgbU4E+r541+FNMpgAZNvs2CiW/eApfaaQojsZEAHDsDv4L5n3M1CC7fYjE/d61aSng1LaO6T1mh+dEfPvLzp7zyzz+UgWMhi5Cs4pcXx1eic5r7uxPoBwcCTt3YI1jKVVnV7/w=</X509Certificate></X509Data></KeyInfo></Signature></SolicitudAceptacionRechazo>";
        readonly SolicitudAceptacionRechazo _solicitud;
        CancelationDAO _cancelation;
        PendingsDAO _pendings;
        ReceptionDAO _reception;
        RelationsDAO _relations;
        public UT_AceptacionRechazo()
        {
            _cancelation = CancelationDAO.Instance(new SAT.Core.DL.Database(new SQLDatabase(Environment.GetEnvironmentVariable("EMULATION_DB"))));
            _pendings = PendingsDAO.Instance(new SAT.Core.DL.Database(new SQLDatabase(Environment.GetEnvironmentVariable("EMULATION_DB"))));
            _reception = new ReceptionDAO(new SAT.Core.DL.Database(new SQLDatabase(Environment.GetEnvironmentVariable("EMULATION_DB"))));
            _relations = new RelationsDAO(new SAT.Core.DL.Database(new SQLDatabase(Environment.GetEnvironmentVariable("EMULATION_DB"))));
            _service = new CancelationAceptacionRechazoServiceEmulation(_pendings, _cancelation);
            _receptionService = new RecibeServiceEmulation(_relations, _reception, Environment.GetEnvironmentVariable("EMULATION_SAS"));
            _solicitud = Core.Helpers.XmlHelper.DeserealizeDocument<SolicitudAceptacionRechazo>(Core.Helpers.XmlHelper.RemoveInvalidCharsXml(solicitudXML));
        }
    



        [TestMethod]
        public void UT_OK_AcceptReject()
        {
            

            var res = _service.ProcesarRespuesta(_solicitud);

            Assert.IsTrue(res.CodEstatus == "1000");
        }

        [TestMethod]
        public void UT_OK_AcceptReject301()
        {

            SolicitudAceptacionRechazo solicitudAceptacionRechazo = new SolicitudAceptacionRechazo()
            {
                RfcPacEnviaSolicitud = rfcPacEnviaSolicitud,
                RfcReceptor = null,
                Folios = _solicitud.Folios,
                Fecha = _solicitud.Fecha,
                Signature = _solicitud.Signature
            };
            var res = _service.ProcesarRespuesta(solicitudAceptacionRechazo);

            Assert.IsTrue(res.CodEstatus == "301");
        }

        [TestMethod]
        public void UT_OK_AcceptReject301_2()
        {
            SolicitudAceptacionRechazo solicitudAceptacionRechazo = new SolicitudAceptacionRechazo()
            {
                RfcPacEnviaSolicitud = rfcPacEnviaSolicitud,
                RfcReceptor = rfcReceptor+"J6",
                Folios = _solicitud.Folios,
                Fecha = _solicitud.Fecha,
                Signature = _solicitud.Signature
            };
            var res = _service.ProcesarRespuesta(solicitudAceptacionRechazo);

            Assert.IsTrue(res.CodEstatus == "301");
        }
        [TestMethod]
        public void UT_OK_AcceptReject1005()
        {
            List<SolicitudAceptacionRechazoFolios> folios = new List<SolicitudAceptacionRechazoFolios>();
            folios.Add(new SolicitudAceptacionRechazoFolios() { UUID="4574", Respuesta=TipoAccionPeticionCancelacion.Aceptacion});
            SolicitudAceptacionRechazo solicitudAceptacionRechazo = new SolicitudAceptacionRechazo()
            {
                RfcPacEnviaSolicitud = rfcPacEnviaSolicitud,
                RfcReceptor = rfcReceptor,
                Folios = folios.ToArray(),
                Fecha = _solicitud.Fecha,
                Signature = _solicitud.Signature
            };
            var res = _service.ProcesarRespuesta(solicitudAceptacionRechazo);
            
            Assert.IsTrue(res.CodEstatus == "1000");
            Assert.IsTrue(res.Folios[0].EstatusUUID == "1005");
        }

     
 
        }


    }

