using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public struct LaserComponent
    {
        public Transform transform;
        public LineRenderer lineRenderer;
        public Transform damageLine;
        public AudioClip laserSFX;
        public float distance;
        public bool isShooting;
        public bool isReloading;
        public bool laserOn;
        public float timeToStop;
        public float attackTime;
        public float reloadTimer;
        public float reloadTimerDelay;
        public float reloadTimerMax;
        public int maxShootCount;
        public int anableShootCount;
        public float nextReloadTime;
        
        

       
    }
}