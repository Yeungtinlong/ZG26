using TheGame.ResourceManagement;
using UnityEngine;

namespace TheGame
{
    public class AppInitializer : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            TheGameSceneManager.Instance.ChangeScene("Bootstrap");
        }
    }
}
