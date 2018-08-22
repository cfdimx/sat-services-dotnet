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
    [ServiceBehavior(Namespace = "http://cancelacfd.sat.gob.mx", AddressFilterMode = AddressFilterMode.Any)]
    public class CancelaCFDBindingEmulation : ICancelaCFDBindingEmulation
    {
        public Acuse CancelaCFD(Cancelacion cancelacion)
        {
            DateTime cancelationDate = DateTime.Parse(string.Format("{0:s}", DateTime.UtcNow), CultureInfo.InvariantCulture);
            Acuse acuse = new Acuse();
            acuse.CodEstatus = "CA1000";
            acuse.Fecha = cancelationDate;

            if (cancelacion.Folios?.Count() > 0)
            {
                acuse.Folios = new AcuseFolios[cancelacion.Folios.Length];
                var acusesFolios = new List<AcuseFolios>();
                foreach (var folio in cancelacion.Folios)
                {
                    acusesFolios.Add(new AcuseFolios()
                    {
                        EstatusUUID = "201",
                        UUID = folio.UUID
                    });
                }
                acuse.Folios = acusesFolios.ToArray();
            }
            acuse.RfcEmisor = cancelacion.RfcEmisor;
            acuse.Signature = cancelacion.Signature;

            return acuse;
        }
    }
}
