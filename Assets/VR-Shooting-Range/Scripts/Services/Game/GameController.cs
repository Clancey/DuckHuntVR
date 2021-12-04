using UnityEngine;


namespace ExitGames.SportShooting
{
    public class GameController : MonoBehaviour
    {
        public bool IsInGame { get; private set; }
        public static GameController Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            this.InitMainMenu();
        }

        public void InitMainMenu()
        {
            IsInGame = false;
            GameModel.Instance.ChangeGameState(new MainMenuGameState());
        }

        public void StartMultiplayerGame()
        {
            IsInGame = true;
            GameModel.Instance.ChangeGameState(new InitializingGameState());
        }
    }
}