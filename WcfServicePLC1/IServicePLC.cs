using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;


namespace WcfServicePLC1
{
      
    [ServiceContract]
    public interface IServicePLC
    {


        // @http://localhost:51259/servicePLC.svc/xml?tagValue=Test.Device1.C0&tagSerie=Test.Device1.C1


        [OperationContract]
        [WebInvoke(
            Method = "GET",            
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "xml?tagValue={valor}&tagSerie={serie}")]

        // Ready to go 19/04/2017  - Checkpoint 14:25
        Counter XMLData(string valor,string serie);




    }




}
