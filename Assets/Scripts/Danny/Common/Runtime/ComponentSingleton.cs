using UnityEngine;

namespace SupportUtils
{
    public class ComponentSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static object _lock = new object();
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = FindObjectOfType<T>();
                            if (_instance == null)
                            {
                                GameObject go = new GameObject(typeof(T).Name);
                                _instance = go.AddComponent<T>();
                                DontDestroyOnLoad(go);
                            }
                        }
                    }
                }

                return _instance;
            }
        }
    }
}