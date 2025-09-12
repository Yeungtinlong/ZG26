using TheGame.ResourceManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private RoleMenuUI _roleMenu;
        [SerializeField] private DailyMenuUI _dailyMenu;
        [SerializeField] private ShopMenuUI _shopMenu;
        
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _roleButton;
        [SerializeField] private Button _dailyButton;
        [SerializeField] private Button _shopButton;

        private void Awake()
        {
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            _startButton.onClick.AddListener(Start_OnClick);
            _roleButton.onClick.AddListener(Role_OnClick);
            _dailyButton.onClick.AddListener(Daily_OnClick);
            _shopButton.onClick.AddListener(Shop_OnClick);
            
            _roleMenu.Set(() => _roleMenu.gameObject.SetActive(false));
            _dailyMenu.Set(() => _dailyMenu.gameObject.SetActive(false));
            _shopMenu.Set(() => _shopMenu.gameObject.SetActive(false));
        }

        private void UnsubscribeFromEvents()
        {
            _startButton.onClick.RemoveListener(Start_OnClick);
            _roleButton.onClick.RemoveListener(Role_OnClick);
            _dailyButton.onClick.RemoveListener(Daily_OnClick);
            _shopButton.onClick.RemoveListener(Shop_OnClick);
        }
        
        private void Start_OnClick()
        {
            TheGameSceneManager.Instance.ChangeScene("Gameplay");
        }

        private void Role_OnClick()
        {
            _roleMenu.gameObject.SetActive(true);
        }
        
        private void Daily_OnClick()
        {
            _dailyMenu.gameObject.SetActive(true);
        }
        
        private void Shop_OnClick()
        {
            _shopMenu.gameObject.SetActive(true);
        }
    }
}