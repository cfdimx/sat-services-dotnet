using SAT.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAT.Core.Helpers;
using System.ServiceModel;

namespace SAT.CancelaCFD
{
    [ServiceBehavior(Namespace = "http://cancela",AddressFilterMode = AddressFilterMode.Any)]
    public class CancelaCFDServiceEmulation : ICancelaCFDServiceEmulation
    {
        public Acuse CancelaCFD(Cancelacion cancelacion)
        {
            DateTime cancelationDate = DateTime.Parse(string.Format("{0:s}", DateTime.UtcNow), CultureInfo.InvariantCulture);
            Acuse acuse = new Acuse();
            acuse.CodEstatus = "CA1000";
            acuse.Fecha = cancelationDate;
            if (cancelacion.Folios?.Count() > 0)
            {
                foreach (var folio in cancelacion.Folios)
                {
                    acuse.Folios.Append(new AcuseFolios()
                    {
                        EstatusUUID = "201",
                        UUID = folio.UUID
                    });
                }
            }
            acuse.RfcEmisor = cancelacion.RfcEmisor;
            acuse.Signature = cancelacion.Signature;

            return acuse;
        }
    }
}
