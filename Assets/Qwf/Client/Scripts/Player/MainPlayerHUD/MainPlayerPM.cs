using MyLibrary;

namespace Qwf.Client {
    public class MainPlayerPM : GenericViewModel {
        public const string GOLD_PROPERTY = "Gold";

        public MainPlayerPM( IPlayerData i_data ) {
            SetGoldProperty( i_data );
        }

        public void Dispose() {

        }

        private void SetGoldProperty( IPlayerData i_data ) {
            ViewModel.SetProperty( GOLD_PROPERTY, i_data.Gold.ToString() );
        }
    }
}