using System.Collections.Generic;
using System.Linq;
using SupportUtils;
using TheGame.ResourceManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheGame.UI
{
    public enum UILayer
    {
        /// <summary>
        /// 未定义
        /// </summary>
        Normal,

        /// <summary>
        /// 不可再后退，各场景的根UI
        /// </summary>
        Menu,

        /// <summary>
        /// 从Menu中打开
        /// </summary>
        Panel,

        /// <summary>
        /// 询问、消息提示等。
        /// </summary>
        Popup,

        /// <summary>
        /// 总是置顶
        /// </summary>
        Overlay,

        /// <summary>
        /// 拦截层
        /// </summary>
        Blocker,

        /// <summary>
        /// 拦截层之上
        /// </summary>
        AboveBlocker,
    }

    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance => _instance;

        private readonly Dictionary<string, BaseUI> _uiDict = new Dictionary<string, BaseUI>();
        private readonly Dictionary<UILayer, Transform> _layers = new Dictionary<UILayer, Transform>();

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
            UILayerLocator[] locators = GetComponentsInChildren<UILayerLocator>();
            for (int i = 0; i < locators.Length; i++)
            {
                _layers.Add(locators[i].Layer, locators[i].transform);
            }
        }

        public T OpenUI<T>(params object[] parameters) where T : BaseUI
        {
            string typeName = typeof(T).Name;
            if (!_uiDict.ContainsKey(typeName))
            {
                var prefab = ResLoader.LoadAsset<GameObject>($"Prefabs/UI/{typeName}.prefab");
                T prefabComponent = prefab.GetComponent<T>();
                var instanceComponent = Object.Instantiate(prefabComponent, _layers[prefabComponent.Layer]);
                instanceComponent.uiManager = this;
                _uiDict.Add(typeName, instanceComponent);
            }

            T uiInstance = (T)_uiDict[typeName];
            uiInstance.transform.SetParent(_layers[uiInstance.Layer]);
            uiInstance.gameObject.SetActive(true);
            uiInstance.OnOpen();

            if (uiInstance is IOneShotUI)
            {
                _uiDict.Remove(typeName);
            }

            // if (parameters is { Length: 1 } && uiInstance is IDataContainer dataContainer)
            // {
            //     dataContainer.SetData(parameters[0]);
            // }

            IDOTweenAnimatable[] animatables = uiInstance.GetComponents<IDOTweenAnimatable>();
            if (animatables.Length > 0)
            {
                animatables[0].Show(uiInstance.OnOpened);
                for (int i = 1; i < animatables.Length; i++)
                    animatables[i].Show();
            }
            else
            {
                uiInstance.OnOpened();
            }

            return uiInstance;
        }

        public void CloseUI<T>() where T : BaseUI
        {
            string typeName = typeof(T).Name;
            if (!_uiDict.TryGetValue(typeName, out var uiInstance))
                return;

            CloseUI(uiInstance);
        }

        public void CloseUI<T>(T uiInstance, bool instant = false) where T : BaseUI
        {
            string typeName = uiInstance.GetType().Name;
            if (_uiDict.ContainsKey(typeName))
                _uiDict.Remove(typeName);

            if (uiInstance == null)
                return;

            uiInstance.OnClose();
            if (!instant)
            {
                IDOTweenAnimatable[] animatables = uiInstance.GetComponents<IDOTweenAnimatable>();
                if (animatables.Length > 0)
                {
                    for (int i = 1; i < animatables.Length; i++)
                        animatables[i].Hide();

                    animatables[0].Hide(ManageableUI_OnClosed);
                }
                else
                {
                    ManageableUI_OnClosed();
                }
            }
            else
            {
                ManageableUI_OnClosed();
            }

            void ManageableUI_OnClosed()
            {
                uiInstance.OnClosed();
                Object.Destroy(uiInstance.gameObject);
            }
        }

        public void CloseAllUI()
        {
            var keys = _uiDict.Keys.ToList();

            foreach (var key in keys)
            {
                CloseUI(_uiDict[key], true);
            }
        }
    }
}