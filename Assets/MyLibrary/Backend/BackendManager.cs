
namespace MyLibrary {
    public static class BackendManager {
        private static IBasicBackend mBackend;

        public static void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;
        }

        public static T GetBackend<T>() {
            return (T) mBackend;
        }
    }
}