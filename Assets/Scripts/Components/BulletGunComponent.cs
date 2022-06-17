using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public struct BulletGunComponent
    {
        public GameObject bulletPrefab;
        public Transform transform;
        public AudioClip bulletSFX;
        public bool isShooting;
        public float reloadTime;
        public float nextShootTime;
        public float projectileSpeed;

    }
}