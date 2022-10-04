using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        private void Awake()
        {
            
        }

        public Transform GetMovementPoint()
        {
            return transform;
        }
    }
}