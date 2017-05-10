using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;



namespace WcfServicePLC1
{
    [DataContract]
    public class Counter
    {

        [DataMember]
        public int cValor { get; set; }

        [DataMember]
        public int cSerie { get; set; }



    }
}