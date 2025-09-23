using System.Linq;
using DG.Tweening;
using MBF;
using TheGame.GM;
using TheGame.ResourceManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class LevelMenuUI : MonoBehaviour, INavigationMenu
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private TMPShadowText _levelInspector;
        [SerializeField] private ReadyArea _readyArea;

        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        [SerializeField] private MissionMenuUI _missionMenu;
        [SerializeField] private Button _missionButton;

        public NavigationMenuType Type => NavigationMenuType.Level;

        public void Set()
        {
            SetDefaultSelectLevel();
            RefreshLevel();
            RefreshRoles();
        }

        private void RefreshRoles()
        {
            MapGrid[] mapGrids = _readyArea.GetComponentsInChildren<MapGrid>();
            ChaInstance[] ownedRoles = GameRuntimeData.Instance.ChaInstances.Values.Where(cha => cha.owned).ToArray();
            for (int i = 0; i < mapGrids.Length; i++)
            {
                if (i >= ownedRoles.Length)
                {
                    mapGrids[i].gameObject.SetActive(false);
                    continue;
                }

                ChaInstance chaInstance = ownedRoles[i];

                if (mapGrids[i].Character != null)
                {
                    Destroy(mapGrids[i].Character.gameObject);
                    mapGrids[i].Character = null;
                }

                LCharacterConfig chaConfig = LuaToCsBridge.CharacterTable[chaInstance.id];
                GameObject roleObj = Instantiate(
                    ResLoader.LoadAsset<GameObject>(PathHelper.GetPrefabPath($"Characters/{chaConfig.Prefab}")),
                    mapGrids[i].transform);

                roleObj.transform.position = mapGrids[i].transform.position;
                mapGrids[i].Character = roleObj.GetComponent<CharacterState>();
            }
        }

        private void RefreshLevel()
        {
            int selectedLevelId = GameRuntimeData.Instance.SelectedLevel;
            _levelInspector.Set($"{selectedLevelId}.{LuaToCsBridge.LevelTable[selectedLevelId].name}");
            _leftButton.gameObject.SetActive(selectedLevelId > 1);
            _rightButton.gameObject.SetActive(selectedLevelId < GameRuntimeData.Instance.PassedLevel + 1);
        }

        private void SetDefaultSelectLevel()
        {
            GameRuntimeData.Instance.SelectedLevel = GameRuntimeData.Instance.PassedLevel + 1;
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            _startGameButton.onClick.AddListener(StartGame_OnClick);
            _leftButton.onClick.AddListener(Left_OnClick);
            _rightButton.onClick.AddListener(Right_OnClick);
            _missionButton.onClick.AddListener(Mission_OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _startGameButton.onClick.RemoveListener(StartGame_OnClick);
            _leftButton.onClick.RemoveListener(Left_OnClick);
            _rightButton.onClick.RemoveListener(Right_OnClick);
            _missionButton.onClick.RemoveListener(Mission_OnClick);
        }

        private void Mission_OnClick()
        {
            _missionMenu.gameObject.SetActive(true);
            _missionMenu.Set(menu => menu.gameObject.SetActive(false));
        }

        private void StartGame_OnClick()
        {
            TheGameSceneManager.Instance.ChangeScene("Gameplay");
        }

        private void Left_OnClick()
        {
            if (GameRuntimeData.Instance.SelectedLevel == 1)
                return;

            GameRuntimeData.Instance.SelectedLevel--;
            RefreshLevel();
            _levelInspector.transform.DOKill();
            _levelInspector.transform.localPosition = Vector3.zero;
            _levelInspector.transform.DOLocalMoveX(-50f, 0.05f).SetLoops(2, LoopType.Yoyo);
        }

        private void Right_OnClick()
        {
            if (GameRuntimeData.Instance.SelectedLevel == GameRuntimeData.Instance.PassedLevel + 1)
                return;

            GameRuntimeData.Instance.SelectedLevel++;
            RefreshLevel();
            _levelInspector.transform.DOKill();
            _levelInspector.transform.localPosition = Vector3.zero;
            _levelInspector.transform.DOLocalMoveX(50f, 0.05f).SetLoops(2, LoopType.Yoyo);
        }
    }
}