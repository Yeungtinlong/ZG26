using TheGame.InputSystem;
using UnityEngine;

namespace TheGame.GM
{
    /// <summary>
    /// Gameplay entry, to initialize GameManager
    /// </summary>
    public class GameplayInitializer : MonoBehaviour
    {
        private void Start()
        {
            GameManager game = FindAnyObjectByType<GameManager>();
            GameLuaInterface.game = game;
            game.Set();

            InputManager input = FindAnyObjectByType<InputManager>();
            GameLuaInterface.input = input;
            input.Set(game);

            game.ReadyGame();
        }
    }
}