using System.Collections.Generic;
using Cinemachine;
using Player;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIObjectsController : MonoBehaviour
    {
        public Quaternion LookDirection => _lookDirection;
    
        private readonly List<UICanvasLookAtCamera> _viewObjects = new List<UICanvasLookAtCamera>();
        private CinemachineVirtualCamera _camera;
        private Pool _damageViewPool;
        private GameObject _playerObject;
        private Quaternion _lookDirection = Quaternion.identity;

        [Inject]
        public void Construct(Pool damageViewPool,
            CinemachineVirtualCamera virtualCamera,
            PlayerController playerController)
        {
            _damageViewPool = damageViewPool;
            _camera = virtualCamera;
            _playerObject = playerController.gameObject;
        }
    
        private void Awake()
        {
            _damageViewPool.Initialize();
            PlayerViewRotationCalculate();
        }

        private void PlayerViewRotationCalculate()
        {
            var cameraPosition = _camera.transform.position;
            var lookDirection = (_playerObject.transform.position - cameraPosition).normalized;
            _lookDirection = Quaternion.LookRotation(lookDirection);
        }
    
        public GameObject GetDamageViewObject(Transform parent)
        {
            return _damageViewPool.GetObject(parent);
        }
    
        public void UpdateViewObjectsList()
        {
            _viewObjects.AddRange(FindObjectsOfType<UICanvasLookAtCamera>(true));

            foreach (var uiObject in _viewObjects)
            {
                uiObject.gameObject.SetActive(false);
            }
        }

        public void ViewObjectsActivate()
        {
            foreach (var uiObject in _viewObjects)
            {
                uiObject.gameObject.SetActive(true);
            }
        }
    
        public void ViewObjectsDeactivate()
        {
            foreach (var uiObject in _viewObjects)
            {
                uiObject.gameObject.SetActive(false);
            }        
        }
    }
}
