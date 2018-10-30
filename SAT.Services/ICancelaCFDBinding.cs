﻿using SAT.CancelaCFD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SAT.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "http://cancelacfd.sat.gob.mx")]
    [XmlSerializerFormat]
    public interface ICancelaCFDBinding
    {
        [OperationContract]
        Acuse CancelaCFD(Cancelacion Cancelacion);
    }
}
