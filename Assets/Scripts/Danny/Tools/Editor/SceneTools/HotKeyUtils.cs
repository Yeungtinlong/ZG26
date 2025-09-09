#if UNITY_EDITOR
using SupportUtils;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class HotKeyUtils : ComponentSingleton<HotKeyUtils>
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.RightAlt))
            {
                EditorApplication.isPaused = !EditorApplication.isPaused;
            }
        }
    }
}

#endif