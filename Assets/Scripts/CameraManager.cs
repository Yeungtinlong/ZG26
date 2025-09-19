using System;
using DG.Tweening;
using SupportUtils;
using TheGame.GM;
using UnityEngine;

namespace TheGame.CoreModule
{
    public class CameraManager : MonoBehaviour
    {
        // aspect: 2.16f (iphone 12 pro max 2778x1284 landscape)
        // aspect: 1.33f (ipad pro 12.9" 2732x2048 landscape)

        // aspect: 0.46f (iphone 12 pro max 1284x2778 portrait)
        // aspect: 0.75f (ipad pro 12.9" 2048x2732 portrait)
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _iphoneCameraSize = 7;
        [SerializeField] private float _ipadCameraSize = 7;
        [SerializeField] private ScreenOrientation _orientation;

        public Camera MainCamera { get; private set; }

        private Sequence _sequence;

        private float _maxSize
        {
            get
            {
                // var aspect = (float)Screen.width / Screen.height;
                // return aspect.MapTo(2.16f, 1.33f, 8.5f, 10f);
                float iphoneAspect = _orientation == ScreenOrientation.Portrait ? 0.46f : 2.16f;
                float ipadAspect = _orientation == ScreenOrientation.Portrait ? 0.75f : 1.33f;
                float aspect = (float)Screen.width / Screen.height;
                return aspect.MapTo(iphoneAspect, ipadAspect, _iphoneCameraSize, _ipadCameraSize);
            }
        }

        public void Set(float height)
        {
            _iphoneCameraSize = height;
            _ipadCameraSize = height;
            MainCamera.orthographicSize = _maxSize;
        }

        private float _minSize => 5.5f;
        private float _scale = 1f;

        public event Action<float> OnCameraScaleChanged;

        private float _mapWidth => GameLuaInterface.game.SceneVariants.map.Size.x;
        private float _mapHeight => GameLuaInterface.game.SceneVariants.map.Size.y;

        private bool _isMoving = false;
        private Vector3 _targetPos;

        public void MoveCamera(Vector3 targetPos)
        {
            _isMoving = true;
            _targetPos = GetSuggestCameraPos(targetPos);
        }

        public void MoveCameraVec(Vector3 targetVec)
        {
            _isMoving = true;
            _targetPos = GetSuggestCameraPos(transform.position + targetVec);
        }

        private void Update()
        {
            if (_isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * _moveSpeed);
                if (transform.position == _targetPos)
                    _isMoving = false;
            }
        }

        public Vector3 GetSuggestCameraPos(Vector3 targetPos)
        {
            float widthHalf = MainCamera.orthographicSize * MainCamera.aspect;
            float heightHalf = MainCamera.orthographicSize;

            if (targetPos.x + widthHalf > _mapWidth * 0.5f)
                targetPos = new Vector3(_mapWidth * 0.5f - widthHalf, targetPos.y, targetPos.z);

            if (targetPos.x - widthHalf < -_mapWidth * 0.5f)
                targetPos = new Vector3(-_mapWidth * 0.5f + widthHalf, targetPos.y, targetPos.z);

            if (targetPos.y + heightHalf > _mapHeight * 0.5f)
                targetPos = new Vector3(targetPos.x, _mapHeight * 0.5f - heightHalf, targetPos.z);

            if (targetPos.y - heightHalf < -_mapHeight * 0.5f)
                targetPos = new Vector3(targetPos.x, -_mapHeight * 0.5f + heightHalf, targetPos.z);

            return new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }

        public void Awake()
        {
            MainCamera = GetComponentInChildren<Camera>();
        }

        private void OnDisable()
        {
            _sequence?.Kill();
        }

        public void SetCameraScale(float targetScale, bool smooth = true)
        {
            targetScale = Mathf.Clamp01(targetScale);
            _sequence?.Kill();
            if (smooth)
            {
                _sequence = DOTween.Sequence();
                _sequence.Append(DOTween.To(() => _scale, s =>
                {
                    _scale = s;
                    MainCamera.orthographicSize = Mathf.Lerp(_minSize, _maxSize, _scale);
                    OnCameraScaleChanged?.Invoke(_scale);
                }, targetScale, 0.5f));
            }
            else
            {
                _scale = targetScale;
                MainCamera.orthographicSize = Mathf.Lerp(_minSize, _maxSize, _scale);
                OnCameraScaleChanged?.Invoke(_scale);
            }
        }

        public float GetCameraScale() => _scale;
    }
}