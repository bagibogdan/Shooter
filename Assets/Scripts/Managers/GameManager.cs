using Zenject;
using SaveSystem;
using UnityEngine;

namespace Managers
{
    public class GameManager : IInitializable
    {
        private readonly SaveController _saveController;
        private GameData _gameData; 
        
        public GameManager(SaveController saveController)
        {
            _saveController = saveController;
        }
        
        public void Initialize()
        {
            _gameData = _saveController.LoadData();
        }
    }
}