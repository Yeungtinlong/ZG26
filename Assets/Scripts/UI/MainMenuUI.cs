using TheGame.GM;
using TheGame.ResourceManagement;
using TMPro;
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
        [SerializeField] private TMP_Text _levelText;
        
        [SerializeField] private Button _roleButton;
        [SerializeField] private Button _dailyButton;
        [SerializeField] private Button _shopButton;

        private void OnEnable()
        {
            SubscribeToEvents();
            RefreshUI();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            _startButton.onClick.AddListener(Start_OnClick);
            _roleButton.onClick.AddListener(Role_OnClick);
            _dailyButton.onClick.AddListener(Daily_OnClick);
            _shopButton.onClick.AddListener(Shop_OnClick);
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
            _roleMenu.Set(() => _roleMenu.gameObject.SetActive(false));
        }

        private void Daily_OnClick()
        {
            _dailyMenu.gameObject.SetActive(true);
            _dailyMenu.Set(() => _dailyMenu.gameObject.SetActive(false));
        }

        private void Shop_OnClick()
        {
            _shopMenu.gameObject.SetActive(true);
            _shopMenu.Set(() => _shopMenu.gameObject.SetActive(false));
        }

        private void RefreshUI()
        {
            _levelText.text = $"开始第{GameRuntimeData.Instance.SelectedLevel}关";
        }
    }
}