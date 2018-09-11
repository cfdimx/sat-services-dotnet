using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.RecibeCFDI
{
    public interface IRecibeServiceEmulation
    {
        AcuseRecepcion Recibe(CFDI cFDI);
    }
}
