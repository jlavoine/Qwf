
using System;
using System.Collections.Generic;

namespace Qwf.Client {
    public class ClientPlayer : IGamePlayer {
        private string mId;
        public string Id { get { return mId; } private set { mId = value; } }

        public ClientPlayer( string i_id ) {
            Id = i_id;
        }        

        public void DrawToFillHand() {
            throw new NotImplementedException();
        }

        public List<IGamePiece> GetHeldPieces() {
            throw new NotImplementedException();
        }

        public List<IGamePiece> GetUndrawnPieces() {
            throw new NotImplementedException();
        }

        public bool IsGamePieceHeld( IGamePiece i_piece ) {
            throw new NotImplementedException();
        }

        public void RemovePieceFromHand( IGamePiece i_piece ) {
            throw new NotImplementedException();
        }
    }
}