using UnityEngine;
using Zenject;

namespace UI
{
    public class UICanvasLookAtCamera : MonoBehaviour
    {
        private UIObjectsController _uiObjectsController;
        private Quaternion _lookRotation = Quaternion.identity;

        [Inject]
        public void Construct(UIObjectsController uiObjectsController)
        {
            _uiObjectsController = uiObjectsController;
        }
        
        private void Awake()
        {
            _uiObjectsController = FindObjectOfType<UIObjectsController>();
        }
        
        private void Start()
        {
            _lookRotation = _uiObjectsController.LookDirection;
            transform.rotation = _lookRotation;
        }

        private void OnEnable()
        {
            _lookRotation = _uiObjectsController.LookDirection;
            transform.rotation = _lookRotation;
        }

        private void LateUpdate()
        {
            transform.rotation = _lookRotation;
        }
    }
}
