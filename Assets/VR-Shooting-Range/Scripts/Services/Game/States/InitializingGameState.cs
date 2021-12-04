namespace ExitGames.SportShooting
{
    public class InitializingGameState : BaseGameState
    {
        public override void InitState()
        {
            base.InitState();

            GameModel.Instance.BuildPlayer();
            this.SpawnUI();

            GameModel.Instance.ChangeGameState(new PlayingGameState());
        }

        void SpawnUI()
        {
            GameView.Instance.ShowGamePopupPanel("GOOD LUCK!");
            GameView.Instance.ShowScoringPanel();
        }
    }
}