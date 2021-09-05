using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace ChatComSignalR
{
    public class MyConnection : PersistentConnection
    {
        private static int QuantidadeConexoes = 0;
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var objeto = JsonConvert.DeserializeObject<dynamic>(data);
            int i;
            string text = objeto.Text;
            string from = objeto.From;
            if ((i = text.IndexOf(":")) > -1) 
            {
                var groupName = text.Substring(0, i);
                var messageOrCommand = text.Substring(i + 1);
                switch(messageOrCommand)
                {
                    case "join":
                        Groups.Add(connectionId, groupName);
                        Groups.Send(groupName, $"user: {from}({connectionId}) join in group {groupName}");
                            break;
                    case "leave":
                        Groups.Remove(connectionId, groupName);
                        Groups.Send(groupName, $"user: {from}({connectionId}) leave of the group {groupName}");

                        break;
                    default:
                        Groups.Send(groupName, messageOrCommand + $"({groupName})");
                        break;
                }
            }
            else
            {
                Connection.Broadcast($"Mensagem de {from}: {text}");
            }
 
            return base.OnReceived(request, connectionId, data);
        }

        //protected override Task OnReceived(IRequest request, string connectionId, string data)
        //{
        //    return Connection.Broadcast(data);
        //}

        //protected override Task OnConnected(IRequest request, string connectionId)
        //{
        //    Interlocked.Increment(ref QuantidadeConexoes);
        //    Connection.Broadcast("Online agora: " + QuantidadeConexoes);
        //    Connection.Send(connectionId, "Bem-vindo " + connectionId);
        //    return base.OnConnected(request, connectionId);
        //}

        //protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        //{
        //    Interlocked.Decrement(ref QuantidadeConexoes);
        //    Connection.Broadcast("Online agora: " + QuantidadeConexoes);
        //    return base.OnDisconnected(request, connectionId, stopCalled);
        //}
    }
}