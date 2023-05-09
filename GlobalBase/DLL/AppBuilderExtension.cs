using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace GlobalBase.DLL
{
    /// <summary>
    /// 核心代码，非作者请勿触碰！
    /// </summary>
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder UseSubscribe(this IApplicationBuilder appBuilder, string subscriptionIdPrefix, Assembly assembly)
        {
            try
            {
                var services = appBuilder.ApplicationServices.CreateScope().ServiceProvider;

                var lifeTime = services.GetService<IHostApplicationLifetime>();
                var bus = services.GetService<IBus>();
                lifeTime.ApplicationStarted.Register(() =>
                {
                    var subscriber = new AutoSubscriber(bus, subscriptionIdPrefix);
                    subscriber.Subscribe(assembly.GetTypes());
                    subscriber.SubscribeAsync(assembly.GetTypes());
                });

                lifeTime.ApplicationStopped.Register(() => bus.Dispose());
            }
            catch (Exception err)
            {

            }
            return appBuilder;
        }
    }
}
