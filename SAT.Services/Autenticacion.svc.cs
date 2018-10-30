using SAT.Autenticacion;
using SAT.Autenticacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SAT.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Autenticacion" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Autenticacion.svc o Autenticacion.svc.cs en el Explorador de soluciones e inicie la depuración.
    
    
    public class Autenticacion : IAutenticacion
    {
      
        
        public AutenticaResponse Autentica(AutenticaRequest request)
        {
            AutenticaResponse response = new AutenticaResponse();
            response.AutenticaResult = new AutenticationEmulation().Autentica();
            return response;
        }
    }
}
