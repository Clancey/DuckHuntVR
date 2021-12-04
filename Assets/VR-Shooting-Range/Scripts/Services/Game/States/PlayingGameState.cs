using UnityEngine;


namespace ExitGames.SportShooting
{
    public class PlayingGameState : BaseGameState
    {
        float _lastTrapSpawnTime = 0f;

        public override void InitState()
        {
            base.InitState();

            
        }

        public override void FinishState()
        {
            base.FinishState();

        }

        public override void ExecuteState()
        {
            base.ExecuteState();
          
            this.AttemptSpawnTrap();
        }

        void AttemptSpawnTrap()
        {
            if (this._lastTrapSpawnTime < 0f)
            {
                if (GameModel.Instance.IsRoundEnded)
                {
                    GameModel.Instance.ChangeGameState(new ScoringGameState());
                }
                else
                {
                    GameModel.Instance.BuildTrap();
                    this._lastTrapSpawnTime = GameModel.Instance.TrapSpawnInterval;
                }
            }

            this._lastTrapSpawnTime -= Time.deltaTime;
        }

      

        void ShowObjectHitMessage(int playerId, bool positive)
        {
           
            var objectName = positive ? "Baloon" : "Bomb";
            var message = $"You Shot the {objectName}";

            GameView.Instance.ShowStatusPopupPanel(message, 2.0f);
        }
    }
}