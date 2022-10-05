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
        public void Construct(Pool damageViewPool, CinemachineVirtualCamera virtualCamera)
        {
            _damageViewPool = damageViewPool;
            _camera = virtualCamera;
        }
    
        private void Awake()
        {
            _damageViewPool.Initialize();
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
            _playerObject = FindObjectOfType<PlayerController>().gameObject;

            foreach (var uiObject in _viewObjects)
            {
                uiObject.gameObject.SetActive(false);
            }
        }

        public void ViewObjectsActivate()
        {
            PlayerViewRotationCalculate();

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
