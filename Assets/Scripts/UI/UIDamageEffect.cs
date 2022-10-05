using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIDamageEffect : MonoBehaviour, IPoolInitializable
    {
        private const float MOVING_OFFSET = 2f;
        private const float MOVING_SPEED = 1f;
        private const float MIN_ALPHA = 0f;
        private const float MAX_ALPHA = 1f;
        private const float EFFECT_DURATION = 0.3f;

        private Vector3 _objectStartPosition;
        private Pool _pool;
        private Transform _position;
        private CanvasGroup _canvas;
        private TextMeshProUGUI _damageText;
        private float _startYPosition;
        private float _yPosition;

        private void Awake()
        {
            _canvas = GetComponentInChildren<CanvasGroup>();
            _damageText  = GetComponentInChildren<TextMeshProUGUI>();
            _startYPosition = gameObject.transform.position.y;
            _objectStartPosition = new Vector3(0f, _startYPosition, 0f);
        }

        public void PoolInitialize(Pool pool)
        {
            _pool = pool;
        }

        public void ReturnToPool()
        {
            _pool.ReturnObject(gameObject);
        }

        public async UniTask Play(int damageValue)
        {
            _canvas.alpha = MAX_ALPHA;
            _damageText.text = $"-{damageValue}";
            _position = transform;
            _position.localPosition = _objectStartPosition;
            await Move();
            await FadeOut();
            ReturnToPool();
        }
    
        private async UniTask Move()
        {
            while (_position.localPosition.y < _startYPosition + MOVING_OFFSET)
            {
                _yPosition = _position.localPosition.y;
                _yPosition += MOVING_SPEED * Time.deltaTime;
                _position.localPosition = new Vector3(0f, _yPosition, 0f);
                await UniTask.Yield();
            }
        }

        private async UniTask FadeOut()
        {
            await _canvas.DOFade(MIN_ALPHA, EFFECT_DURATION);
        }
    }
}
