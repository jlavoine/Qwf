using MyLibrary;

namespace Qwf.Client {
    public class PlayerManager : IPlayerManager {
        public const string GOLD_KEY = "G1";

        private static IPlayerManager mInstance;
        public static IPlayerManager Instance {
            get {
                if ( mInstance == null ) {
                    mInstance = new PlayerManager();
                }

                return mInstance;
            }
            set {
                mInstance = value; // testing only!
            }
        }

        private IPlayerData mData;
        public IPlayerData Data { get { return mData; } set { mData = value; } }

        public PlayerManager() {}

        public void Init( IPlayerData i_data ) {
            Data = i_data;

            IQwfBackend backend = BackendManager.Instance.GetBackend<IQwfBackend>();
            backend.GetVirtualCurrency( GOLD_KEY, OnCurrencyRequest );
        }

        public void OnCurrencyRequest( int i_currency ) {
            Data.Gold = i_currency;
        }
    }
}