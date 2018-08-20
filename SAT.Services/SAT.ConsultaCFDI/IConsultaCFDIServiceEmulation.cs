using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.ConsultaCFDI
{
    public interface IConsultaCFDIServiceEmulation
    {
        Acuse Consulta(string expresionImpresa);
    }
}
