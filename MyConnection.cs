using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ChatComSignalR
{
    public class MyConnection : PersistentConnection
    {
        private static int QuantidadeConexoes = 0;
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            return Connection.Broadcast(data);
        }

        protected override Task OnConnected(IRequest request, string connectionId)
        {
            Interlocked.Increment(ref QuantidadeConexoes);
            Connection.Broadcast("Online agora: " + QuantidadeConexoes);
            Connection.Send(connectionId, "Bem-vindo " + connectionId);
            return base.OnConnected(request, connectionId);
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            Interlocked.Decrement(ref QuantidadeConexoes);
            Connection.Broadcast("Online agora: " + QuantidadeConexoes);
            return base.OnDisconnected(request, connectionId, stopCalled);
        }
    }
}