using strange.extensions.command.api;
using strange.extensions.injector.api;
using strange.extensions.mediation.api;

namespace Qwf.Server {
    public class GameServerManager : StrangePackage {

        public override void MapBindings( ICommandBinder commandBinder, ICrossContextInjectionBinder injectionBinder, IMediationBinder mediationBinder ) {
            // mediationBinder.Bind<NewExampleView>().To<NewExampleMediator>();

            injectionBinder.Bind<IGameRules>().To<GameRules>();

            //Bind Commands and Signals            
            commandBinder.Bind<CreateGamePlayerSignal>().To<CreateGamePlayerCommand>();
            commandBinder.Bind<SendGamePlayerHandSignal>().To<SendGamePlayerHandCommand>();
            //commandBinder.Bind<SetupUnityNetworkingCompleteSignal>();
            //commandBinder.Bind<ClientDisconnectedSignal>();
        }

        public override void PostBindings( ICommandBinder commandBinder, ICrossContextInjectionBinder injectionBinder, IMediationBinder mediationBinder ) {

        }

        public override void Launch( ICrossContextInjectionBinder injectionBinder ) {

        }    
    }
}
