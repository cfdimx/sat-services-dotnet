using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CfdiConsultaRelacionados
{
    public class CfdiConsultaRelacionadosServiceEmulation : ICfdiConsultaRelacionadosServiceEmulation
    {
        public ConsultaRelacionados ProcesarRespuesta(PeticionConsultaRelacionados solicitud)
        {
            Guid uuid = Guid.Empty;
            if (solicitud == null || string.IsNullOrEmpty(solicitud.RfcReceptor))
                return new ConsultaRelacionados()
                {
                    UuidConsultado = solicitud.Uuid,
                    Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor}  -UUID: {solicitud.Uuid} - Clave: 301 - Error: La solicitud no tiene definido el RFC Receptor"
                };

            if (solicitud.RfcReceptor.Length != 13 && solicitud.RfcReceptor.Length != 12)
                return new ConsultaRelacionados()
                {
                    UuidConsultado = solicitud.Uuid,
                    Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - UUID: {solicitud.Uuid} - Clave: 301 - Error: El formato del RFC del receptor proporcionado no es válido."
                };

            if (Guid.TryParse(solicitud.Uuid, out uuid))
                return new ConsultaRelacionados()
                {
                    UuidConsultado = solicitud.Uuid,
                    Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - UUID:  - Clave: 301 - Error: Solicitud invalida, el uuid de la peticion no posee el formato correcto."
                };


            return new ConsultaRelacionados()
            {
                Resultado = $"WS Consulta CFDI relacionados RfcReceptor: {solicitud.RfcReceptor} - folio físcal: {solicitud.Uuid} - Clave: 2000 - Se encontraron CFDI relacionados",
                UuidConsultado = solicitud.Uuid,
                UuidsRelacionadosHijos = new UuidRelacionado[] {
                    new UuidRelacionado() {  RfcEmisor = "LAN8507268IA" , RfcReceptor = "LAN7008173R5", Uuid = "62CABB40-E680-43D7-8F42-CE596DA56052" } },
                UuidsRelacionadosPadres = new UuidPadre[] {
                    new UuidPadre() {  RfcEmisor = "LAN8507268IA" , RfcReceptor = "LAN7008173R5", Uuid = "26AC91A5-67C8-482F-A55C-8D3BAC90FAD6" } }
            };
        }
    }
}
