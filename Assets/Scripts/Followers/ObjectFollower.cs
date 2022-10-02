using UnityEngine;

namespace Followers
{
    public abstract class ObjectFollower : MonoBehaviour
    {
        protected Transform _followedObjectTransform;
    
        private Vector3 _offset;

        private void Start()
        {
            _offset = _followedObjectTransform.position - transform.position;
        }

        private void LateUpdate()
        {
            transform.position = _followedObjectTransform.position - _offset;
        }
    }
}
