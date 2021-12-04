using System.Collections.Generic;
using UnityEngine;


namespace ExitGames.SportShooting
{
    public class GameModel : MonoBehaviour
    {
        #region Trap Properties

        public float TrapSpawnInterval
        {
            get { return this._trapFactory.TrapSpawnInterval; }
        }

        [SerializeField]
        TrapFactory _trapFactory;

        [SerializeField]
        int _trapsPerRound;

        int _currentRoundTraps = 0;

        public bool IsRoundEnded
        {
            get { return this._currentRoundTraps >= this._trapsPerRound; }
        }

        #endregion


        #region Player Properties

        [SerializeField]
        PlayerFactory _playerFactory;

        #endregion


        #region Gameplay Properties

        public Dictionary<int, int> ScoreBoard { get; set; }

        public Dictionary<int, int> PlayersPositions { get; set; }


        BaseGameState _activeGameState;

        public BaseGameState ActiveGameState
        {
            get { return this._activeGameState; }
        }

        public IShootingRangePlayer CurrentPlayer { get; set; }

        public static GameModel Instance { get; set; }

        #endregion


        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            // Execute current game state each frame
            if (this._activeGameState != null)
            {
                this._activeGameState.ExecuteState();
            }
        }

        public void ChangeGameState(BaseGameState newState)
        {
            if (this._activeGameState != null)
            {
                this._activeGameState.FinishState();
            }

            this._activeGameState = newState;
            this._activeGameState.InitState();
        }

        public void CountScoreToPlayer(int playerID, int scorePrice)
        {
            //foreach (Player player in PhotonNetwork.PlayerList)
            //{
            //    if (player.ActorNumber == playerID)
            //    {
            //        var updatedProperties = player.CustomProperties;
            //        updatedProperties["roundScore"] = (int)updatedProperties["roundScore"] + scorePrice;
            //        player.SetCustomProperties(updatedProperties);
            //    }
            //}

        }

        public void ResetRoundData()
        {
            this._currentRoundTraps = 0;
        }

        public void BuildTrap()
        {
            this._trapFactory.Build();
            this._currentRoundTraps++;
        }

        public void BuildPlayer()
        {
            this._playerFactory.Build();
        }
    }
}