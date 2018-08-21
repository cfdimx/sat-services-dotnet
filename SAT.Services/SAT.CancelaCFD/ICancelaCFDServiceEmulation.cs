using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CancelaCFD
{
    public interface ICancelaCFDServiceEmulation
    {
        Acuse CancelaCFD(Cancelacion Cancelacion);
    }
}
