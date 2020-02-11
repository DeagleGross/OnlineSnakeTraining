using Autofac;
using SafeboardSnake.Core.Engine;

namespace SafeboardSnake.WebApi.IOC
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Registered GameService as main class defining snake-game behaviour
            // It is registered as single-instance not to initialize
            // new GameService instance every gameboardController call

            builder.RegisterType<GameService>().AsSelf().SingleInstance();
        }
    }
}
