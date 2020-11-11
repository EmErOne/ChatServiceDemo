using System;
using System.Threading.Tasks;
using ChatService.Server.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ChatService.Server.Startup))]

namespace ChatService.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Weitere Informationen zum Konfigurieren Ihrer Anwendung finden Sie unter https://go.microsoft.com/fwlink/?LinkID=316888.
            // NuGet Microsoft.AspNet.SignalR
            //       Microsoft.AspNet.SignalR.SelfHost
            //       Microsoft.EntityFrameworkCore.Sqlite   
            app.MapSignalR();
            ChatServerContext.Init();           
        }        
    }
}
