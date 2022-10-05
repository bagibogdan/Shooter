using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameObject _obstaclePrefab;
        [SerializeField] private GameObject _movementPoint;

        private const float START_OBSTACLE_Z_OFFSET = 10f;
        private const float START_OBSTACLE_X_OFFSET = 5f;
        private const float OBSTACLES_Z_STEP = 12f;
        
        private List<Transform> _movementPoints = new List<Transform>();
        private Transform _playingAreaTransform;
        private Transform _levelTransform;
        private Vector3 _playingAreaScale;
        private Vector3 _levelPosition;

        private void Awake()
        {
            _playingAreaTransform = GetComponentInChildren<PlayingAreaComponent>().gameObject.transform;
            _playingAreaScale = _playingAreaTransform.localScale;
            _levelTransform = gameObject.transform;
            _levelPosition = _levelTransform.position;
            GenerateLevel();
        }
        
        public void GenerateLevel()
        {
            int levelIndex = 1;
            if (levelIndex <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(levelIndex));
            }
            
            UnityEngine.Random.InitState(levelIndex);
            
            ObstaclesGenerate();
        }

        private void ObstaclesGenerate()
        {
            var xLeftEdge = _levelPosition.x;
            var zDownEdge = _levelPosition.z;
            var zUpEdge = _levelPosition.z + _playingAreaScale.z;
            var levelWidth = _playingAreaScale.x;
            var levelLength = _playingAreaScale.z;

            var obstaclesZOffset = (levelLength - START_OBSTACLE_Z_OFFSET) / OBSTACLES_Z_STEP;
            var currentPositionX = xLeftEdge + START_OBSTACLE_X_OFFSET;
            var currentPositionZ = zDownEdge + START_OBSTACLE_Z_OFFSET;

            var obstaclesLinesCount = 1;

            while (currentPositionZ < zUpEdge)
            {
                var obstaclesXCount = Mathf.CeilToInt(Random.Range(2f, 6f));
                var obstaclesXStep = (levelWidth - START_OBSTACLE_X_OFFSET) / obstaclesXCount;

                for (int i = 0; i < obstaclesXCount; i++)
                {
                    var currentPosition = new Vector3((currentPositionX + (i * obstaclesXStep)), 0f, currentPositionZ);
                    Transform obstacle;
                    
                    if (obstaclesLinesCount % 2 != 0)
                    {
                        obstacle = Instantiate(_obstaclePrefab).transform;
                        obstacle.position = currentPosition;
                    }
                    else
                    {
                        obstacle = Instantiate(_movementPoint).transform;
                        obstacle.position = currentPosition;
                        _movementPoints.Add(obstacle.transform);
                    }
                    
                    obstacle.SetParent(_playingAreaTransform);
                }

                currentPositionZ += obstaclesZOffset;
                obstaclesLinesCount++;
            }
        }
        
        public Vector3 GetMovementPoint()
        {
            if (_movementPoints.Count < 0) return Vector3.zero;
            
            var point = _movementPoints[Random.Range(0, _movementPoints.Count)];
            _movementPoints.Remove(point);
            return point.position;
        }
    }
}