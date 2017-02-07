using strange.extensions.command.api;
using strange.extensions.injector.api;
using strange.extensions.mediation.api;

namespace Qwf.Server {
    public class GameServerManager : StrangePackage {

        public override void MapBindings( ICommandBinder commandBinder, ICrossContextInjectionBinder injectionBinder, IMediationBinder mediationBinder ) {            
            mediationBinder.Bind<CreateGameBoardView>().To<CreateGameBoardMediator>();
            mediationBinder.Bind<CreateGameManagerView>().To<CreateGameManagerMediator>();

            injectionBinder.Bind<GameBoardCreatedSignal>().ToSingleton();
            injectionBinder.Bind<PlayerAddedSignal>().ToSingleton();

            injectionBinder.Bind<IGameRules>().To<GameRules>();
            injectionBinder.Bind<IScoreKeeper>().To<ScoreKeeper>().ToSingleton();

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
