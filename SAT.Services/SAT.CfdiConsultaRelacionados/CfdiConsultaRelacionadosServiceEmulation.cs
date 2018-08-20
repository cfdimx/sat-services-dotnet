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
