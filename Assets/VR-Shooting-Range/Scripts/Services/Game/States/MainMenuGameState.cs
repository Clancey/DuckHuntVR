using UnityEngine;


namespace ExitGames.SportShooting
{
    public class MainMenuGameState : BaseGameState
    {
        public override void InitState()
        {
            base.InitState();

            GameModel.Instance.BuildPlayer();

            foreach (SpawnPoint s in Object.FindObjectsOfType<SpawnPoint>())
            {
                s.RestoreDefaults();
            }
        }
    }
}