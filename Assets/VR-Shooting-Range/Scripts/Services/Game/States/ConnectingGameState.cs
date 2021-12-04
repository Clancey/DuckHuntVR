using System.Collections.Generic;
using UnityEngine;


namespace ExitGames.SportShooting
{
    public class ConnectingGameState : BaseGameState
    {
        public override void InitState()
        {
            base.InitState();
        }

        public override void FinishState()
        {
            base.FinishState();
        }

        void InitGame()
        {
            this.SetPlayerData();
            GameModel.Instance.ChangeGameState(new InitializingGameState());
        }

        void SetPlayerData()
        {
            List<int> freePositions = new List<int>();
           

            string playerName = string.Empty;

            switch (freePositions[0])
            {
                case 0:
                    playerName = "Player RED";
                    break;
                case 1:
                    playerName = "Player BLUE";
                    break;
                case 2:
                    playerName = "Player YELLOW";
                    break;
                case 3:
                    playerName = "Player GREEN";
                    break;
                case 4:
                    playerName = "Player BLACK";
                    break;
            }

            //playerInfo.Add("position", freePositions[0]);
            //playerInfo.Add("roundScore", 0);
            //playerInfo.Add("name", playerName);
        }
    }
}