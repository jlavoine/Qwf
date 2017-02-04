using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qwf.Client {
    public class TestObstaclesView : MonoBehaviour {
        public GameObstaclesView View;

        // Use this for initialization
        void Start() {
            IGameObstaclesUpdate randomUpdate = CreateRandomUpdate();
            GameObstaclesPM pm = new GameObstaclesPM( randomUpdate );

            View.Init( pm );
        }

        private IGameObstaclesUpdate CreateRandomUpdate() {
            GameObstaclesUpdate update = new GameObstaclesUpdate();
            update.Obstacles = new List<GameObstacleUpdate>();

            GameObstacleUpdate one = new GameObstacleUpdate();
            one.Id = GetRandomObstacleId();

            update.Obstacles.Add( one );

            return update;
        }

        private string GetRandomObstacleId() {
            List<string> ids = new List<string>() { "Dragon", "Blob", "WizGob", "Goblin", "Chest" };
            return ListUtils.GetRandomElement<string>( ids );
        }

        // Update is called once per frame
        void Update() {

        }
    }
}