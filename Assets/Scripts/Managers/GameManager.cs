using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Enemy;
using Level;
using Player;
using Zenject;
using SaveSystem;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action<int> OnCoinsChanged;

        public int ItemsCostMultiplier { get; set; }
        public int Level => _gameData.level;
        
        private const int COINS_ADD_VALUE = 100;
        
        private SaveController _saveController;
        private PlayerController _playerController;
        private LevelController _levelController;
        private EnemiesController _enemiesController;
        private UIManager _uiManager;
        private FirebaseManager _firebaseManager;
        private IronSourceManager _ironSourceManager;
        private GameData _gameData = new GameData();

        public int Coins
        {
            get => _gameData.coins;
            private set
            {
                if (value == _gameData.coins)
                    return;

                _gameData.coins = value;
                OnCoinsChanged?.Invoke(value);
            }
        }
        
        [Inject]
        public void Construct(SaveController saveController,
            PlayerController playerController,
            LevelController levelController,
            EnemiesController enemiesController,
            UIManager uiManager,
            FirebaseManager firebaseManager)
        {
            _saveController = saveController;
            _levelController = levelController;
            _playerController = playerController;
            _enemiesController = enemiesController;
            _uiManager = uiManager;
            _firebaseManager = firebaseManager;
        }
        
        public void Awake()
        {
            _ironSourceManager = FindObjectOfType<IronSourceManager>();
            _uiManager.ShowBlackPanel();
            _gameData = _saveController.LoadData();
            Coins = _gameData.coins;
            _playerController.OnLose += OnLose;
            _playerController.OnWin += OnWin;
        }

        private async void Start()
        {
            _uiManager.Initialize();
            await _firebaseManager.Initialize();
            await _firebaseManager.RemoteConfigItemsCostMultiplier();
            await _uiManager.ShowStartPanel();
            await _uiManager.ShowInfoPanel();
        }

        public void OnDisable()
        {
            _playerController.OnLose -= OnLose;
            _playerController.OnWin -= OnWin;
            _saveController.SaveData(_gameData);
        }

        private void OnDestroy()
        {
            _saveController.SaveData(_gameData);
        }

        public async void StartGame()
        {
            _levelController.GenerateLevel(_gameData.level);
            _enemiesController.CreateEnemies(_gameData.level);
            _playerController.gameObject.transform.position = _levelController.StartPosition;
            _playerController.Restart();
            _playerController.ActivateMoving();
            await _uiManager.ShowGamePanel();
        }
        
        public void OnAddCoins()
        {
            Coins += COINS_ADD_VALUE;
        }

        public async void OnRestart()
        {
            _enemiesController.ClearEnemies();
            _enemiesController.CreateEnemies(_gameData.level);
            _playerController.gameObject.transform.position = _levelController.StartPosition;
            _playerController.Restart();
            _playerController.ActivateMoving();
            await _uiManager.ShowGamePanel();
        }
        
        private async void OnWin()
        {
            _gameData.level++;
            _saveController.SaveData(_gameData);
            _enemiesController.ClearEnemies();
            _levelController.ClearLevel();
            await _uiManager.ShowWinPanel();
            _firebaseManager.LevelUpEvent(_gameData.level);
            _ironSourceManager.ShowInterstitial();
        }
        
        private async void OnLose()
        {
            await UniTask.Delay(3000);
            await _uiManager.ShowLosePanel();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                _saveController.SaveData(_gameData);
            }
        }
    }
}