using UnityEngine;


namespace ExitGames.SportShooting
{
    public class ScoringGameState : BaseGameState
    {
        bool _isActive = false;
        float _stateLifeTime = 0;

        public override void InitState()
        {
            Debug.Log("Scoring State");
            base.InitState();
        }

        public override void FinishState()
        {
            base.FinishState();

            GameModel.Instance.ResetRoundData();
        }

        public override void ExecuteState()
        {
            base.ExecuteState();
            if (this._isActive)
            {
                this._stateLifeTime += Time.deltaTime;
            }

            if (this._stateLifeTime > GameView.Instance.WinScreenTimeout)
            {
                GameModel.Instance.ChangeGameState(new PlayingGameState());
            }
        }
    }
}