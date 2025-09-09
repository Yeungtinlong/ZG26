using TheGame.Common;
using TheGame.GM;
using TheGame.ResourceManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheGame
{
    public class Bootstrapper : MonoBehaviour
    {
        private async void Start()
        {
            await ResLoader.Init();
            
            LuaManager.Init();
            
            LuaToCsBridge.LoadLuaConfigs();
            
            if(!GameRuntimeData.SaveExists())
                GameRuntimeData.NewGame();
            else
                GameRuntimeData.LoadGame();
            
            
            await ResLoader.LoadSceneAsync("PersistentResources", LoadSceneMode.Additive, false);
            await TheGameSceneManager.Instance.ChangeScene("MainMenu");
        }
    }
}