using System;
using UnityEngine;

namespace ExitGames.SportShooting
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField]
        bool useNonVrPlayerInEditor;

        [SerializeField]
        GameObject oculusPlayerPrefab;


        [SerializeField]
        GameObject _nonVrPlayerPrefab;

        [SerializeField]
        Transform _playerSpawnPoints;

        private GameObject _playerPrefab;

        private void Awake()
        {
#if UNITY_EDITOR
            if (useNonVrPlayerInEditor)
            {
                _playerPrefab = _nonVrPlayerPrefab;
                return;
            }
#else
            useNonVrPlayerInEditor = false;
#endif

#if UNITY_STANDALONE
            _playerPrefab = oculusPlayerPrefab;
#endif
        }

        public Transform PlayerSpawnPoints
        {
            get
            {
                return _playerSpawnPoints;
            }
        }

        public void Build()
        {
            if (GameModel.Instance.ActiveGameState is InitializingGameState)
            {
                BuildPlayerForGame();
            }
            else if (GameModel.Instance.ActiveGameState is MainMenuGameState)
            {
                BuildPlayerForMenu();
            }
        }

        public void BuildPlayerForGame()
        {
            if (GameModel.Instance.CurrentPlayer != null)
            {
                GameObject.DestroyImmediate(GameModel.Instance.CurrentPlayer.GameObject);
            }

            int positionIndex = 0;
            Vector3 spawnPoint = PlayerSpawnPoints.GetChild(positionIndex).position;
            
            if (useNonVrPlayerInEditor)
            {
                spawnPoint.y += 2;
            }

            GameObject go = Instantiate(_playerPrefab, spawnPoint, Quaternion.identity);
            GameModel.Instance.CurrentPlayer = (IShootingRangePlayer) go.GetComponent(typeof(IShootingRangePlayer));

            //Initialize UI
            if (!useNonVrPlayerInEditor)
            {
                GameModel.Instance.CurrentPlayer.GameSetup();
            }
        }

        public void BuildPlayerForMenu()
        {
            if (GameModel.Instance.CurrentPlayer != null)
            {
                Destroy(GameModel.Instance.CurrentPlayer.GameObject);
            }
            
            Vector3 spawnPoint = PlayerSpawnPoints.GetChild(0).position;
            
            if (useNonVrPlayerInEditor)
            {
                spawnPoint.y += 2;
            }

            GameObject go = GameObject.Instantiate(_playerPrefab, spawnPoint, Quaternion.identity) as GameObject;
            GameModel.Instance.CurrentPlayer = (IShootingRangePlayer) go.GetComponent(typeof(IShootingRangePlayer));
            
            //Initializing UI
            if (!useNonVrPlayerInEditor)
            {
                GameModel.Instance.CurrentPlayer.MenuSetup();
            }
            GameView.Instance.ShowMainMenuPanel();
        }

        public static Color GetColor(int position)
        {
            switch (position)
            {
                case 0: return Color.red;
                case 1: return Color.blue;
                case 2: return Color.yellow;
                case 3: return Color.green;
                case 4: return Color.black;
                default: return Color.grey;
            }
        }
    }
}
