using NUnit.Framework;
using NSubstitute;
using UnityEngine.Networking;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestClientRelay : QwfUnitTest {

        [Test]
        public void WhenCreating_SubscribesToMessages() {
            ClientRelay systemUnderTest = new ClientRelay();

            MyMessenger.Instance.Received().AddListener<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, Arg.Any<Callback<ClientTurnAttempt>>() );
            MyMessenger.Instance.Received().AddListener<IUnityNetworkWrapper>( ClientMessages.NETWORK_CLIENT_CREATED, Arg.Any<Callback<IUnityNetworkWrapper>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            ClientRelay systemUnderTest = new ClientRelay();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, Arg.Any<Callback<ClientTurnAttempt>>() );
            MyMessenger.Instance.Received().RemoveListener<IUnityNetworkWrapper>( ClientMessages.NETWORK_CLIENT_CREATED, Arg.Any<Callback<IUnityNetworkWrapper>>() );
        }

        [Test]
        public void WhenRegisteringMessages_ExpectedMessagesHandled() {
            IUnityNetworkWrapper mockNetwork = Substitute.For<IUnityNetworkWrapper>();
            ClientRelay systemUnderTest = new ClientRelay();
            systemUnderTest.OnNetworkClientCreated( mockNetwork );
                        
            mockNetwork.Received().RegisterMessageHandler( NetworkMessages.UpdatePlayerHand, Arg.Any<NetworkMessageDelegate>() );
            mockNetwork.Received().RegisterMessageHandler( NetworkMessages.UpdateObstacles, Arg.Any<NetworkMessageDelegate>() );
            mockNetwork.Received().RegisterMessageHandler( NetworkMessages.UpdateTurn, Arg.Any<NetworkMessageDelegate>() );
            mockNetwork.Received().RegisterMessageHandler( NetworkMessages.UpdateScore, Arg.Any<NetworkMessageDelegate>() );
            mockNetwork.Received().RegisterMessageHandler( NetworkMessages.UpdateGameOver, Arg.Any<NetworkMessageDelegate>() );
        }
    }
}