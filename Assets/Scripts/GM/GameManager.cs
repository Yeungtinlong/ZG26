using System;
using System.Collections.Generic;
using System.Linq;
using MBF;
using UnityEngine;
using TheGame.CoreModule;
using TheGame.ResourceManagement;
using TheGame.UI;
using Object = UnityEngine.Object;

namespace TheGame.GM
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// 本句游戏的黑板，一局Gameplay所关注的数据都在这里了
        /// </summary>
        private SceneVariants _sceneVariants;

        public SceneVariants SceneVariants => _sceneVariants;

        private BulletManager _bullet;
        public BulletManager Bullet => _bullet;

        private AoeManager _aoe;
        public AoeManager Aoe => _aoe;

        private TimelineManager _timeline;
        public TimelineManager Timeline => _timeline;

        private DamageManager _damage;
        public DamageManager Damage => _damage;

        private CharacterManager _character;
        public CharacterManager Character => _character;

        private CameraManager _camera;
        public CameraManager Camera => _camera;

        private TurnManager _turn;
        public TurnManager Turn => _turn;

        private ReadyArea _readyArea;

        private GameControlState _gameState = GameControlState.None;

        private readonly Dictionary<string, int> _gameAssets = new Dictionary<string, int>();

        public Dictionary<string, int> GameAssets => _gameAssets;

        private float _gameSpeed = 1.0f;

        /// <summary>
        /// 影响游戏速度的乘数
        /// </summary>
        private readonly Dictionary<string, float> _gameSpeedFactors = new Dictionary<string, float>();

        private float GameSpeed => _gameSpeed;

        public event Action<DropInfo> OnDropCreated;
        public static event Action<GameResult, int> OnGameOver;
        public static event Action<int> OnTurnChanged;

        /// <summary>
        /// Gameplay资产发生变化
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        public static event Action<string, int, int> OnGameAssetChanged;

        private void Awake()
        {
            ValidateRequiredComponents();
        }

        private void OnDestroy()
        {
            ClearGameSpeedFactors();
        }

        private void ValidateRequiredComponents()
        {
            _bullet = GetComponent<BulletManager>();
            _aoe = GetComponent<AoeManager>();
            _timeline = GetComponent<TimelineManager>();
            _damage = GetComponent<DamageManager>();
            _character = GetComponent<CharacterManager>();
            _camera = GetComponentInChildren<CameraManager>();
            _turn = GetComponent<TurnManager>();
            _readyArea = GetComponentInChildren<ReadyArea>();
        }

        public void Set()
        {
            _sceneVariants = new SceneVariants();

            LevelModel levelModel = LuaToCsBridge.LevelTable[GameRuntimeData.Instance.SelectedLevel];
            _bullet.Set(_sceneVariants);
            _aoe.Set(_sceneVariants);
            _character.Set(_sceneVariants);
            _damage.Set(_sceneVariants);
            _turn.Set(_sceneVariants, TurnManager_OnTurnChanged, TurnManager_OnGameOver);

            CreateMap(levelModel.mapId);
            _camera.Set(_sceneVariants.map.Size.y / 2f);
        }

        private void TurnManager_OnTurnChanged(TurnManager turnManager)
        {
            OnTurnChanged?.Invoke(turnManager.CurrentTurn);
        }

        private void TurnManager_OnGameOver(GameResult gameResult)
        {
            if (_gameState == GameControlState.InGame)
            {
                GameOver(gameResult);
            }
        }

        private void GameOver(GameResult gameResult)
        {
            int level = GameRuntimeData.Instance.SelectedLevel;
            if (gameResult == GameResult.Win)
            {
                _gameState = GameControlState.GameOver;

                LevelModel levelModel = LuaToCsBridge.LevelTable[GameRuntimeData.Instance.SelectedLevel];
                if (GameRuntimeData.Instance.SelectedLevel > GameRuntimeData.Instance.PassedLevel)
                {
                    gameResult = GameResult.NewWin;
                    levelModel.rewards?.ForEach(r => GameRuntimeData.Instance.GetItem(r.id, r.count));
                }

                GameRuntimeData.Instance.PassedLevel = Mathf.Max(GameRuntimeData.Instance.SelectedLevel,
                    GameRuntimeData.Instance.PassedLevel);
                GameRuntimeData.SaveGame();

                SetPause(true);

                // TODO: 游戏结束用时间唤起UI
                // UIManager.Instance.OpenUI<GameOverPanelUI>().Set(
                //     levelConfig,
                //     true,
                //     () =>
                //     {
                //         SetPause(false);
                //         GameState.GotoMainScene();
                //     },
                //     null,
                //     () =>
                //     {
                //         SetPause(false);
                //         GameState.GotoGameScene();
                //     }
                // );
            }
            else if (gameResult == GameResult.Lose)
            {
                _gameState = GameControlState.GameOver;
                SetPause(true);
                // TODO: 游戏结束用时间唤起UI
                // UIManager.Instance.OpenUI<GameOverPanelUI>().Set(
                //     LuaToCsBridge.LevelTable[$"{GameRuntimeData.Instance.SelectedLevel}"],
                //     false, () =>
                //     {
                //         SetPause(false);
                //         GameState.GotoMainScene();
                //     }, () =>
                //     {
                //         SetPause(false);
                //         GameState.GotoGameScene();
                //     },
                //     null
                // );
            }

            OnGameOver?.Invoke(gameResult, level);
        }

        public void GetGameAsset(string id, int count)
        {
            _gameAssets.TryAdd(id, 0);
            int oldValue = _gameAssets[id];
            _gameAssets[id] = oldValue + count;
            OnGameAssetChanged?.Invoke(id, _gameAssets[id], oldValue);
        }

        public void GetGameAsset(ItemStack itemStack) => GetGameAsset(itemStack.id, itemStack.count);

        public bool LoseGameAsset(string id, int count)
        {
            int oldValue = _gameAssets[id];
            if (oldValue < count) return false;
            _gameAssets[id] = oldValue - count;
            OnGameAssetChanged?.Invoke(id, _gameAssets[id], oldValue);
            return true;
        }

        public bool LoseGameAsset(ItemStack itemStack) => LoseGameAsset(itemStack.id, itemStack.count);

        public int CountOfGameAsset(string id) => _gameAssets.GetValueOrDefault(id, 0);


        public bool CanAfford(ItemStack items)
        {
            return CountOfGameAsset(items.id) >= items.count;
        }

        public CharacterState CreateCharacter(string chaId, int side, int grade)
        {
            return _character.CreateCharacter(chaId, side, grade);
        }

        public void RemoveCharacter(CharacterState character)
        {
            _character.RemoveCharacter(character);
        }

        private void CreateMap(string mapId)
        {
            _sceneVariants.map =
                Instantiate(
                        ResLoader.LoadAsset<GameObject>(PathHelper.GetPrefabPath($"Maps/{mapId}")))
                    .GetComponent<Map>();
            _sceneVariants.map.Set();
        }

        public bool StartGame()
        {
            List<MapGrid> playerSideGrids = FindObjectsByType<MapGrid>(FindObjectsSortMode.None)
                .Where(g => g.Side == 0)
                .ToList();

            // 未上阵角色
            if (playerSideGrids.Where(g => !g.IsReadyGrid).All(g => g.Character == null))
            {
                UIManager.Instance.OpenUI<MessagePopupUI>().Set("敌军大呼：无人敢战？\n——请上阵至少1个角色", 1f);
                return false;
            }

            // 上阵角色>7
            if (playerSideGrids.Count(g => !g.IsReadyGrid && g.Character != null) > 7)
            {
                UIManager.Instance.OpenUI<MessagePopupUI>().Set("敌军大惊：以众暴寡？\n——上阵角色不能大于7个", 1f);
                return false;
            }

            // 上阵角色>7
            if (playerSideGrids.Count(g =>
                    !g.IsReadyGrid && g.Character != null &&
                    LuaToCsBridge.CharacterTable[g.Character.id].CharacterType == CharacterType.Support) > 1)
            {
                UIManager.Instance.OpenUI<MessagePopupUI>().Set("我军疑惑：听谁号令？\n——不能上阵多于1个主公", 1f);
                return false;
            }

            _gameState = GameControlState.InGame;

            CloseReadyArea();
            _turn.StartCycle();
            return true;
        }

        public void ReadyGame()
        {
            _gameState = GameControlState.ReadyGame;
            ClearGameSpeedFactors();

            SpawnEnemies();
            OpenReadyArea();
            SetActorsFace();
            ApplyStrategy();
        }

        private void ApplyStrategy()
        {
            string strategyId = GameRuntimeData.Instance.SelectedStrategy;
            if (string.IsNullOrEmpty(GameRuntimeData.Instance.SelectedStrategy))
                return;

            StrategyModel strategyModel = LuaToCsBridge.StrategyTable[strategyId];
            UIManager.Instance.OpenUI<MessagePopupUI>().Set(strategyModel.description);
            strategyModel.effect.doEvent?.Invoke(null, strategyModel.effect.eventParams);
        }

        private void OpenReadyArea()
        {
            List<ChaInstance> chaInstances = GameRuntimeData.Instance.ChaInstances.Values.Where(c => c.owned).ToList();
            for (int i = 0; i < chaInstances.Count; i++)
            {
                AddCharacterToGrid(CreateCharacter(chaInstances[i].id, 0, chaInstances[i].grade), _readyArea.Grids[i]);
            }
        }

        private void CloseReadyArea()
        {
            for (int i = 0; i < _readyArea.Grids.Count; i++)
            {
                if (_readyArea.Grids[i].Character != null)
                {
                    RemoveCharacter(_readyArea.Grids[i].Character);
                }
            }

            _readyArea.gameObject.SetActive(false);
        }

        private void SpawnEnemies()
        {
            List<MapGrid> npcSideGrids = Object.FindObjectsByType<MapGrid>(FindObjectsSortMode.None)
                .Where(g => g.Side == 1).OrderBy(m => m.name).ToList();

            LevelModel levelModel = LuaToCsBridge.LevelTable[GameRuntimeData.Instance.SelectedLevel];
            for (int i = 0; i < levelModel.gridInfos.Count; i++)
            {
                MapGridInfo info = levelModel.gridInfos[i];
                AddCharacterToGrid(CreateCharacter(info.chaId, 1, info.grade), npcSideGrids[info.gridIndex]);
            }
        }

        private void SetActorsFace()
        {
            _sceneVariants.characters.ForEach(cs => cs.SetFace(cs.side == 0 ? Vector3.right : Vector3.left));
        }

        public void AddCharacterToGrid(CharacterState cs, MapGrid grid)
        {
            if (cs.Grid != null)
                RemoveCharacterFromGrid(cs.Grid);
            if (grid.Character != null)
                RemoveCharacterFromGrid(grid);

            cs.Grid = grid;
            cs.OnDie += CharacterState_OnDie;

            grid.Character = cs;
            cs.transform.position = grid.transform.position;
        }

        public void RemoveCharacterFromGrid(MapGrid grid)
        {
            if (grid.Character == null) return;

            grid.Character.OnDie -= CharacterState_OnDie;
            grid.Character.Grid = null;
            grid.Character = null;
        }

        private void CharacterState_OnDie(CharacterState cs)
        {
            RemoveCharacterFromGrid(cs.Grid);
        }

        public void FixedUpdate()
        {
            if (_gameState != GameControlState.InGame)
                return;

            _bullet.LogicTick();
            _aoe.LogicTick();
            _timeline.LogicTick();
            _damage.LogicTick();
            _character.LogicTick();
        }

        public void SetGameSpeedFactor(string id, float effectorSpeed)
        {
            _gameSpeedFactors[id] = effectorSpeed;
            RecheckGameSpeed();
        }

        public void RemoveGameSpeedFactor(string id)
        {
            _gameSpeedFactors.Remove(id);
            RecheckGameSpeed();
        }

        public void ClearGameSpeedFactors()
        {
            _gameSpeedFactors.Clear();
            RecheckGameSpeed();
        }

        public void RecheckGameSpeed()
        {
            _gameSpeed = 1.0f;
            foreach (var (key, value) in _gameSpeedFactors)
                _gameSpeed *= value;

            Time.timeScale = _gameSpeed;
        }

        public void SetPause(bool pause)
        {
            if (pause)
            {
                SetGameSpeedFactor("Pause", 0f);
            }
            else
            {
                RemoveGameSpeedFactor("Pause");
            }
        }
    }
}