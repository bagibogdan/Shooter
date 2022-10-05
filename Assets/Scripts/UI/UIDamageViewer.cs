using UnityEngine;
using Zenject;

namespace UI
{
    public class UIDamageViewer : MonoBehaviour
    {
        private UIObjectsController _uiObjectsController;
        private GameObject _effect;
    
        [Inject]
        public void Construct(UIObjectsController uiObjectsController)
        {
            _uiObjectsController = uiObjectsController;
        }

        public async void DamageValueEffect(int value)
        {
            _effect = _uiObjectsController.GetDamageViewObject(gameObject.transform);
            await _effect.GetComponent<UIDamageEffect>().Play(value);
        }
    }
}
