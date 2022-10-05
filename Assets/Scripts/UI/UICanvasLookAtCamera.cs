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
    
        protected void Awake()
        {
            _uiObjectsController = FindObjectOfType<UIObjectsController>();
        }

        protected void OnEnable()
        {
            _lookRotation = _uiObjectsController.LookDirection;
            transform.rotation = _lookRotation;
        }

        protected void LateUpdate()
        {
            transform.rotation = _lookRotation;
        }
    }
}
