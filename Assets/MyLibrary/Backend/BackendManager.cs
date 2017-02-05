﻿
namespace MyLibrary {
    public class BackendManager : IBackendManager {
        private IBasicBackend mBackend;

        private static IBackendManager mInstance;
        public static IBackendManager Instance {
            get {
                if ( mInstance == null ) {
                    mInstance = new BackendManager();
                }
                return mInstance;
            }
            set {
                mInstance = value;  // testing only!!!
            }
        }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;
        }

        public T GetBackend<T>() {
            return (T) mBackend;
        }

        public string GetPlayerId() {
            return mBackend.PlayerId;
        }
    }
}