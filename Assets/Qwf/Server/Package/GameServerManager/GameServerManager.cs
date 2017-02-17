#if ENABLE_PLAYFABSERVER_API
using strange.extensions.command.api;
using strange.extensions.injector.api;
using strange.extensions.mediation.api;

namespace Qwf.Server {
    public class GameServerManager : StrangePackage {

        public override void MapBindings( ICommandBinder commandBinder, ICrossContextInjectionBinder injectionBinder, IMediationBinder mediationBinder ) {            
            mediationBinder.Bind<CreateGameBoardView>().To<CreateGameBoardMediator>();
            mediationBinder.Bind<CreateGameManagerView>().To<CreateGameManagerMediator>();
            mediationBinder.Bind<GameManagerView>().To<GameManagerMediator>();
            mediationBinder.Bind<ClientTurnAttemptView>().To<ClientTurnAttemptMediator>();
            mediationBinder.Bind<GameOverView>().To<GameOverMediator>();

            injectionBinder.Bind<GameBoardCreatedSignal>().ToSingleton();
            injectionBinder.Bind<PlayerAddedSignal>().ToSingleton();
            injectionBinder.Bind<GameManagerCreatedSignal>().ToSingleton();
            injectionBinder.Bind<PlayerTurnProcessedSignal>().ToSingleton();
            injectionBinder.Bind<PostPlayerTurnProcessedSignal>().ToSingleton();

            injectionBinder.Bind<IGameRules>().To<GameRules>();
            injectionBinder.Bind<IScoreKeeper>().To<ScoreKeeper>().ToSingleton();
            injectionBinder.Bind<IGameManager>().To<GameManager>().ToSingleton();

            //Bind Commands and Signals            
            commandBinder.Bind<CreateGamePlayerSignal>().To<CreateGamePlayerCommand>();
            commandBinder.Bind<PlayerTurnProcessedSignal>().To<SendGameUpdatesToPlayersCommand>();
            commandBinder.Bind<SendGamePlayerHandSignal>().To<SendGamePlayerHandCommand>();
            commandBinder.Bind<SendPlayerTurnUpdateSignal>().To<SendPlayerTurnUpdateCommand>();
            //commandBinder.Bind<SetupUnityNetworkingCompleteSignal>();
            //commandBinder.Bind<ClientDisconnectedSignal>();
        }

        public override void PostBindings( ICommandBinder commandBinder, ICrossContextInjectionBinder injectionBinder, IMediationBinder mediationBinder ) {

        }

        public override void Launch( ICrossContextInjectionBinder injectionBinder ) {

        }    
    }
}
#endif
