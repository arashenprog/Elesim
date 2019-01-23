using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Logics.Util.FileMappers
{

    [IgnoreEmptyLines]
    [DelimitedRecord(";")]
    public class PrepaidFileMapperFormat
    {
        public string Number;

        public string ActivationCode;

        public string TraceCode;
    }

    

}
