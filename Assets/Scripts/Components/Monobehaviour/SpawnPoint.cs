using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class SpawnPoint : MonoBehaviour
    {
        public Directions direction;
        public float spawnIntervalTime = 3f;
        public enum Directions
        {
            top,
            bottom,
            left,
            right
        }
    }
}