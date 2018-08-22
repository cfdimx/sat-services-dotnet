using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAT.AceptacionRechazo;
using SAT.Core;

namespace UT_AceptacionRechazo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UT_OK_Pendings()
        {
            CancelationAceptacionRechazoServiceEmulation ca = new CancelationAceptacionRechazoServiceEmulation();
            string receptor = "LAN7008173R5";
            var result = ca.ObtenerPeticionesPendientes(receptor);
            Assert.AreEqual(result.CodEstatus,"1100");
        }

        [TestMethod]
        public void UT_OK_AcceptReject()
        {
            List<SolicitudAceptacionRechazoFolios> folios = new List<SolicitudAceptacionRechazoFolios>();
            folios.Add(new SolicitudAceptacionRechazoFolios { UUID = Guid.NewGuid().ToString(), Respuesta= TipoAccionPeticionCancelacion.Aceptacion});
            SignatureType signature = new SignatureType { KeyInfo = new KeyInfoType { X509Data = new X509DataType {X509Certificate = Convert.FromBase64String("MIIFxTCCA62gAwIBAgIUMjAwMDEwMDAwMDAzMDAwMjI4MTUwDQYJKoZIhvcNAQELBQAwggFmMSAwHgYDVQQDDBdBLkMuIDIgZGUgcHJ1ZWJhcyg0MDk2KTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMSkwJwYJKoZIhvcNAQkBFhphc2lzbmV0QHBydWViYXMuc2F0LmdvYi5teDEmMCQGA1UECQwdQXYuIEhpZGFsZ28gNzcsIENvbC4gR3VlcnJlcm8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQRGlzdHJpdG8gRmVkZXJhbDESMBAGA1UEBwwJQ295b2Fjw6FuMRUwEwYDVQQtEwxTQVQ5NzA3MDFOTjMxITAfBgkqhkiG9w0BCQIMElJlc3BvbnNhYmxlOiBBQ0RNQTAeFw0xNjEwMjUyMTUyMTFaFw0yMDEwMjUyMTUyMTFaMIGxMRowGAYDVQQDExFDSU5ERU1FWCBTQSBERSBDVjEaMBgGA1UEKRMRQ0lOREVNRVggU0EgREUgQ1YxGjAYBgNVBAoTEUNJTkRFTUVYIFNBIERFIENWMSUwIwYDVQQtExxMQU43MDA4MTczUjUgLyBGVUFCNzcwMTE3QlhBMR4wHAYDVQQFExUgLyBGVUFCNzcwMTE3TURGUk5OMDkxFDASBgNVBAsUC1BydWViYV9DRkRJMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgvvCiCFDFVaYX7xdVRhp/38ULWto/LKDSZy1yrXKpaqFXqERJWF78YHKf3N5GBoXgzwFPuDX+5kvY5wtYNxx/Owu2shNZqFFh6EKsysQMeP5rz6kE1gFYenaPEUP9zj+h0bL3xR5aqoTsqGF24mKBLoiaK44pXBzGzgsxZishVJVM6XbzNJVonEUNbI25DhgWAd86f2aU3BmOH2K1RZx41dtTT56UsszJls4tPFODr/caWuZEuUvLp1M3nj7Dyu88mhD2f+1fA/g7kzcU/1tcpFXF/rIy93APvkU72jwvkrnprzs+SnG81+/F16ahuGsb2EZ88dKHwqxEkwzhMyTbQIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAJ/xkL8I+fpilZP+9aO8n93+20XxVomLJjeSL+Ng2ErL2GgatpLuN5JknFBkZAhxVIgMaTS23zzk1RLtRaYvH83lBH5E+M+kEjFGp14Fne1iV2Pm3vL4jeLmzHgY1Kf5HmeVrrp4PU7WQg16VpyHaJ/eonPNiEBUjcyQ1iFfkzJmnSJvDGtfQK2TiEolDJApYv0OWdm4is9Bsfi9j6lI9/T6MNZ+/LM2L/t72Vau4r7m94JDEzaO3A0wHAtQ97fjBfBiO5M8AEISAV7eZidIl3iaJJHkQbBYiiW2gikreUZKPUX0HmlnIqqQcBJhWKRu6Nqk6aZBTETLLpGrvF9OArV1JSsbdw/ZH+P88RAt5em5/gjwwtFlNHyiKG5w+UFpaZOK3gZP0su0sa6dlPeQ9EL4JlFkGqQCgSQ+NOsXqaOavgoP5VLykLwuGnwIUnuhBTVeDbzpgrg9LuF5dYp/zs+Y9ScJqe5VMAagLSYTShNtN8luV7LvxF9pgWwZdcM7lUwqJmUddCiZqdngg3vzTactMToG16gZA4CWnMgbU4E+r541+FNMpgAZNvs2CiW/eApfaaQojsZEAHDsDv4L5n3M1CC7fYjE/d61aSng1LaO6T1mh+dEfPvLzp7zyzz+UgWMhi5Cs4pcXx1eic5r7uxPoBwcCTt3YI1jKVVnV7/w=") ,
                X509IssuerSerial = new X509IssuerSerialType { X509IssuerName = "OID.1.2.840.113549.1.9.2=Responsable: ACDMA, OID.2.5.4.45=SAT970701NN3, L=Coyoacán, S=Distrito Federal, C=MX, PostalCode=06300, STREET='Av.Hidalgo 77, Col.Guerrero', E=asisnet@pruebas.sat.gob.mx, OU=Administración de Seguridad de la Información, O=Servicio de Administración Tributaria, CN=A.C. 2 de pruebas(4096)",
                X509SerialNumber = "3230303031303030303030333030303232383135" } } } ,


                SignatureValue = Convert.FromBase64String("RBCIPI+SBC7y68+67Etuxp//04mjh5kOEkPnGrUCMM0gc2eR83CQMasXISq948rjbqK05Lclz5mdtT+GQwG5rw3DK3zJBpXpUqZ4FomBr+cD5285xOUrWMXdLO4pGfkZ1trU9FsiRKLoJxUesyCg8AvQZ5l/J1XIe0/ZJR7crUtETjpLyiRo5hSXuDnEbywL5Tc1UNQPTYxCv0EremdFpH2+K+bLXZAQPR+x3RlD4b2T50TCPyS73J6qq/Uj7CWQ5FU2M55yuby2Z2Vix/sy+8NB047MJ/pw4QILwCTffE4oD0t+8WD1eNo+ndyW4mYNkt6DEn1Fi2pwhrJA2ct3dA=="),
                SignedInfo = new SignedInfoType { Reference = new ReferenceType { DigestValue = Convert.FromBase64String("UoieV151aY8dURamzUBbQSVNzF4=") } } };
            CancelationAceptacionRechazoServiceEmulation ca = new CancelationAceptacionRechazoServiceEmulation();
            SolicitudAceptacionRechazo solicitudAceptacionRechazo = new SolicitudAceptacionRechazo {
                RfcPacEnviaSolicitud = "DAL050601L35",
                RfcReceptor = "LAN7008173R5",
                Fecha = DateTime.ParseExact("2018-06-28T10:41:39", "yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                Folios = folios.ToArray(),
                Signature = signature,

            };

            var res = ca.ProcesarRespuesta(solicitudAceptacionRechazo);

            Assert.IsTrue(res.CodEstatus == "1000");
        }

        [TestMethod]
        public void UT_OK_AcceptReject300()
        {
            List<SolicitudAceptacionRechazoFolios> folios = new List<SolicitudAceptacionRechazoFolios>();
            folios.Add(new SolicitudAceptacionRechazoFolios { UUID = Guid.NewGuid().ToString(), Respuesta = TipoAccionPeticionCancelacion.Aceptacion });
            SignatureType signature = new SignatureType
            {
                KeyInfo = new KeyInfoType
                {
                    X509Data = new X509DataType
                    {
                        X509Certificate = Convert.FromBase64String("MIIFxTCCA62gAwIBAgIUMjAwMDEwMDAwMDAzMDAwMjI4MTUwDQYJKoZIhvcNAQELBQAwggFmMSAwHgYDVQQDDBdBLkMuIDIgZGUgcHJ1ZWJhcyg0MDk2KTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMSkwJwYJKoZIhvcNAQkBFhphc2lzbmV0QHBydWViYXMuc2F0LmdvYi5teDEmMCQGA1UECQwdQXYuIEhpZGFsZ28gNzcsIENvbC4gR3VlcnJlcm8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQRGlzdHJpdG8gRmVkZXJhbDESMBAGA1UEBwwJQ295b2Fjw6FuMRUwEwYDVQQtEwxTQVQ5NzA3MDFOTjMxITAfBgkqhkiG9w0BCQIMElJlc3BvbnNhYmxlOiBBQ0RNQTAeFw0xNjEwMjUyMTUyMTFaFw0yMDEwMjUyMTUyMTFaMIGxMRowGAYDVQQDExFDSU5ERU1FWCBTQSBERSBDVjEaMBgGA1UEKRMRQ0lOREVNRVggU0EgREUgQ1YxGjAYBgNVBAoTEUNJTkRFTUVYIFNBIERFIENWMSUwIwYDVQQtExxMQU43MDA4MTczUjUgLyBGVUFCNzcwMTE3QlhBMR4wHAYDVQQFExUgLyBGVUFCNzcwMTE3TURGUk5OMDkxFDASBgNVBAsUC1BydWViYV9DRkRJMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgvvCiCFDFVaYX7xdVRhp/38ULWto/LKDSZy1yrXKpaqFXqERJWF78YHKf3N5GBoXgzwFPuDX+5kvY5wtYNxx/Owu2shNZqFFh6EKsysQMeP5rz6kE1gFYenaPEUP9zj+h0bL3xR5aqoTsqGF24mKBLoiaK44pXBzGzgsxZishVJVM6XbzNJVonEUNbI25DhgWAd86f2aU3BmOH2K1RZx41dtTT56UsszJls4tPFODr/caWuZEuUvLp1M3nj7Dyu88mhD2f+1fA/g7kzcU/1tcpFXF/rIy93APvkU72jwvkrnprzs+SnG81+/F16ahuGsb2EZ88dKHwqxEkwzhMyTbQIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAJ/xkL8I+fpilZP+9aO8n93+20XxVomLJjeSL+Ng2ErL2GgatpLuN5JknFBkZAhxVIgMaTS23zzk1RLtRaYvH83lBH5E+M+kEjFGp14Fne1iV2Pm3vL4jeLmzHgY1Kf5HmeVrrp4PU7WQg16VpyHaJ/eonPNiEBUjcyQ1iFfkzJmnSJvDGtfQK2TiEolDJApYv0OWdm4is9Bsfi9j6lI9/T6MNZ+/LM2L/t72Vau4r7m94JDEzaO3A0wHAtQ97fjBfBiO5M8AEISAV7eZidIl3iaJJHkQbBYiiW2gikreUZKPUX0HmlnIqqQcBJhWKRu6Nqk6aZBTETLLpGrvF9OArV1JSsbdw/ZH+P88RAt5em5/gjwwtFlNHyiKG5w+UFpaZOK3gZP0su0sa6dlPeQ9EL4JlFkGqQCgSQ+NOsXqaOavgoP5VLykLwuGnwIUnuhBTVeDbzpgrg9LuF5dYp/zs+Y9ScJqe5VMAagLSYTShNtN8luV7LvxF9pgWwZdcM7lUwqJmUddCiZqdngg3vzTactMToG16gZA4CWnMgbU4E+r541+FNMpgAZNvs2CiW/eApfaaQojsZEAHDsDv4L5n3M1CC7fYjE/d61aSng1LaO6T1mh+dEfPvLzp7zyzz+UgWMhi5Cs4pcXx1eic5r7uxPoBwcCTt3YI1jKVVnV7/w="),
                        X509IssuerSerial = new X509IssuerSerialType
                        {
                            X509IssuerName = "OID.1.2.840.113549.1.9.2=Responsable: ACDMA, OID.2.5.4.45=SAT970701NN3, L=Coyoacán, S=Distrito Federal, C=MX, PostalCode=06300, STREET='Av.Hidalgo 77, Col.Guerrero', E=asisnet@pruebas.sat.gob.mx, OU=Administración de Seguridad de la Información, O=Servicio de Administración Tributaria, CN=A.C. 2 de pruebas(4096)",
                            X509SerialNumber = "3230303031303030303030333030303232383135"
                        }
                    }
                },


                SignatureValue = Convert.FromBase64String("RBCIPI+SBC7y68+67Etuxp//04mjh5kOEkPnGrUCMM0gc2eR83CQMasXISq948rjbqK05Lclz5mdtT+GQwG5rw3DK3zJBpXpUqZ4FomBr+cD5285xOUrWMXdLO4pGfkZ1trU9FsiRKLoJxUesyCg8AvQZ5l/J1XIe0/ZJR7crUtETjpLyiRo5hSXuDnEbywL5Tc1UNQPTYxCv0EremdFpH2+K+bLXZAQPR+x3RlD4b2T50TCPyS73J6qq/Uj7CWQ5FU2M55yuby2Z2Vix/sy+8NB047MJ/pw4QILwCTffE4oD0t+8WD1eNo+ndyW4mYNkt6DEn1Fi2pwhrJA2ct3dA=="),
                SignedInfo = new SignedInfoType { Reference = new ReferenceType { DigestValue = Convert.FromBase64String("UoieV151aY8dURamzUBbQSVNzF4=") } }
            };
            CancelationAceptacionRechazoServiceEmulation ca = new CancelationAceptacionRechazoServiceEmulation();
            SolicitudAceptacionRechazo solicitudAceptacionRechazo = new SolicitudAceptacionRechazo
            {
                RfcPacEnviaSolicitud = "DAL050601L35",
                RfcReceptor = "LAN7008173R",
                Fecha = DateTime.ParseExact("2018-06-28T10:41:39", "yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                Folios = folios.ToArray(),
                Signature = signature,

            };

            var res = ca.ProcesarRespuesta(solicitudAceptacionRechazo);

            Assert.IsTrue(res.CodEstatus == "300");
        }

        [TestMethod]
        public void UT_OK_AcceptReject305()
        {
            List<SolicitudAceptacionRechazoFolios> folios = new List<SolicitudAceptacionRechazoFolios>();
            folios.Add(new SolicitudAceptacionRechazoFolios { UUID = Guid.NewGuid().ToString(), Respuesta = TipoAccionPeticionCancelacion.Aceptacion });
            SignatureType signature = new SignatureType
            {
                KeyInfo = new KeyInfoType
                {
                    X509Data = new X509DataType
                    {
                        X509Certificate = Convert.FromBase64String("WIIFxTCCA62gAwIBAgIUMjAwMDEwMDAwMDAzMDAwMjI4MTUwDQYJKoZIhvcNAQELBQAwggFmMSAwHgYDVQQDDBdBLkMuIDIgZGUgcHJ1ZWJhcyg0MDk2KTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMSkwJwYJKoZIhvcNAQkBFhphc2lzbmV0QHBydWViYXMuc2F0LmdvYi5teDEmMCQGA1UECQwdQXYuIEhpZGFsZ28gNzcsIENvbC4gR3VlcnJlcm8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQRGlzdHJpdG8gRmVkZXJhbDESMBAGA1UEBwwJQ295b2Fjw6FuMRUwEwYDVQQtEwxTQVQ5NzA3MDFOTjMxITAfBgkqhkiG9w0BCQIMElJlc3BvbnNhYmxlOiBBQ0RNQTAeFw0xNjEwMjUyMTUyMTFaFw0yMDEwMjUyMTUyMTFaMIGxMRowGAYDVQQDExFDSU5ERU1FWCBTQSBERSBDVjEaMBgGA1UEKRMRQ0lOREVNRVggU0EgREUgQ1YxGjAYBgNVBAoTEUNJTkRFTUVYIFNBIERFIENWMSUwIwYDVQQtExxMQU43MDA4MTczUjUgLyBGVUFCNzcwMTE3QlhBMR4wHAYDVQQFExUgLyBGVUFCNzcwMTE3TURGUk5OMDkxFDASBgNVBAsUC1BydWViYV9DRkRJMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgvvCiCFDFVaYX7xdVRhp/38ULWto/LKDSZy1yrXKpaqFXqERJWF78YHKf3N5GBoXgzwFPuDX+5kvY5wtYNxx/Owu2shNZqFFh6EKsysQMeP5rz6kE1gFYenaPEUP9zj+h0bL3xR5aqoTsqGF24mKBLoiaK44pXBzGzgsxZishVJVM6XbzNJVonEUNbI25DhgWAd86f2aU3BmOH2K1RZx41dtTT56UsszJls4tPFODr/caWuZEuUvLp1M3nj7Dyu88mhD2f+1fA/g7kzcU/1tcpFXF/rIy93APvkU72jwvkrnprzs+SnG81+/F16ahuGsb2EZ88dKHwqxEkwzhMyTbQIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAJ/xkL8I+fpilZP+9aO8n93+20XxVomLJjeSL+Ng2ErL2GgatpLuN5JknFBkZAhxVIgMaTS23zzk1RLtRaYvH83lBH5E+M+kEjFGp14Fne1iV2Pm3vL4jeLmzHgY1Kf5HmeVrrp4PU7WQg16VpyHaJ/eonPNiEBUjcyQ1iFfkzJmnSJvDGtfQK2TiEolDJApYv0OWdm4is9Bsfi9j6lI9/T6MNZ+/LM2L/t72Vau4r7m94JDEzaO3A0wHAtQ97fjBfBiO5M8AEISAV7eZidIl3iaJJHkQbBYiiW2gikreUZKPUX0HmlnIqqQcBJhWKRu6Nqk6aZBTETLLpGrvF9OArV1JSsbdw/ZH+P88RAt5em5/gjwwtFlNHyiKG5w+UFpaZOK3gZP0su0sa6dlPeQ9EL4JlFkGqQCgSQ+NOsXqaOavgoP5VLykLwuGnwIUnuhBTVeDbzpgrg9LuF5dYp/zs+Y9ScJqe5VMAagLSYTShNtN8luV7LvxF9pgWwZdcM7lUwqJmUddCiZqdngg3vzTactMToG16gZA4CWnMgbU4E+r541+FNMpgAZNvs2CiW/eApfaaQojsZEAHDsDv4L5n3M1CC7fYjE/d61aSng1LaO6T1mh+dEfPvLzp7zyzz+UgWMhi5Cs4pcXx1eic5r7uxPoBwcCTt3YI1jKVVnV7/w="),
                        X509IssuerSerial = new X509IssuerSerialType
                        {
                            X509IssuerName = "OID.1.2.840.113549.1.9.2=Responsable: ACDMA, OID.2.5.4.45=SAT970701NN3, L=Coyoacán, S=Distrito Federal, C=MX, PostalCode=06300, STREET='Av.Hidalgo 77, Col.Guerrero', E=asisnet@pruebas.sat.gob.mx, OU=Administración de Seguridad de la Información, O=Servicio de Administración Tributaria, CN=A.C. 2 de pruebas(4096)",
                            X509SerialNumber = "3230303031303030303030333030303232383135"
                        }
                    }
                },


                SignatureValue = Convert.FromBase64String("RBCIPI+SBC7y68+67Etuxp//04mjh5kOEkPnGrUCMM0gc2eR83CQMasXISq948rjbqK05Lclz5mdtT+GQwG5rw3DK3zJBpXpUqZ4FomBr+cD5285xOUrWMXdLO4pGfkZ1trU9FsiRKLoJxUesyCg8AvQZ5l/J1XIe0/ZJR7crUtETjpLyiRo5hSXuDnEbywL5Tc1UNQPTYxCv0EremdFpH2+K+bLXZAQPR+x3RlD4b2T50TCPyS73J6qq/Uj7CWQ5FU2M55yuby2Z2Vix/sy+8NB047MJ/pw4QILwCTffE4oD0t+8WD1eNo+ndyW4mYNkt6DEn1Fi2pwhrJA2ct3dA=="),
                SignedInfo = new SignedInfoType { Reference = new ReferenceType { DigestValue = Convert.FromBase64String("UoieV151aY8dURamzUBbQSVNzF4=") } }
            };
            CancelationAceptacionRechazoServiceEmulation ca = new CancelationAceptacionRechazoServiceEmulation();
            SolicitudAceptacionRechazo solicitudAceptacionRechazo = new SolicitudAceptacionRechazo
            {
                RfcPacEnviaSolicitud = "DAL050601L35",
                RfcReceptor = "LAN7008173R5",
                Fecha = DateTime.ParseExact("2018-06-28T10:41:39", "yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                Folios = folios.ToArray(),
                Signature = signature,

            };

            var res = ca.ProcesarRespuesta(solicitudAceptacionRechazo);

            Assert.IsTrue(res.CodEstatus == "305");
        }
    }
}
