using SAT.Autenticacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SAT.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IAutenticacion" en el código y en el archivo de configuración a la vez.
    
    [XmlSerializerFormat]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IAutenticacion")]
    public interface IAutenticacion
    {

        [System.ServiceModel.OperationContractAttribute(Action ="*")]
        AutenticaResponse Autentica(AutenticaRequest request);

      
    }
}
