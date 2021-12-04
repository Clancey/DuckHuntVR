using UnityEngine;

namespace ExitGames.SportShooting
{
    public class NonUiButton : MonoBehaviour
    {
        public void EndMatch()
        {
           
            GameModel.Instance.ChangeGameState(new ScoringGameState());
        }

        public void LeaveMatch()
        {
            GameController.Instance.InitMainMenu(); 
        }
    }
}