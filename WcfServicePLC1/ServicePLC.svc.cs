using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace WcfServicePLC1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServicePLC" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServicePLC.svc or ServicePLC.svc.cs at the Solution Explorer and start debugging.


    public class ServicePLC : IServicePLC
    {
       OPCServer oServer;
       OPCGroup oGroup;
        Array handles;
        String[] etiqueta = new String[2];
        
        public Counter XMLData(string valor,string serie)
        {
            etiqueta[0] = valor;
            etiqueta[1] = serie;

            set_opc("Kepware.KEPServerEX.V5", "");
            return leituraTags();



        }

        
        #region OPC Server def
        public void set_opc(String nomeInstancia, String node)
        {
            oServer = new OPCServer();
            oServer.Connect(nomeInstancia, node); // Nodo null puede ser otro 
            oServer.OPCGroups.DefaultGroupIsActive = true;
            oServer.OPCGroups.DefaultGroupDeadband = 0f;
            oServer.OPCGroups.DefaultGroupUpdateRate = 10; //em ms

            oGroup = oServer.OPCGroups.Add("Grupo 1");
            oGroup.IsSubscribed = false; //suscribir a eventos de mudanças de informação 
            oGroup.OPCItems.DefaultIsActive = false; //el item no necesita estar activo, solo sera actualkizado con el ultimo valor

            //agrega items relativos al grupo siempre es uno mas que los tags , 2 es el numero de tags

            int[] h = new int[2 + 1];


            //index siempre comienza en 1, tags es 2
            for (int i = 1; i <= 2; i++)
            {
                h[i] = oGroup.OPCItems.AddItem(etiqueta[i - 1], i).ServerHandle; //the handle is a server generated value that we use to reference the item for further operations
            }
            handles = (Array)h;
        }
        #endregion

        






        #region Tags
        public Counter leituraTags() //reads device
        {
            
            System.Array values; //valores
            System.Array errors; //erros
            object qualities = new object(); //qualidade do item
            object timestamps = new object(); //timestamp de leitura opc server
            // 2  es tags
            oGroup.SyncRead((short)OPCAutomation.OPCDataSource.OPCDevice, 2, ref handles, out values, out errors, out qualities, out timestamps);

            int temp1 = (int)values.GetValue(1);
            int temp2 = (int)values.GetValue(2);
            // temp1 + " " + temp2

            

            return new Counter { cValor= temp1, cSerie=temp2 };
        
        }
        #endregion








    }
}
