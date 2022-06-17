using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "BulletGun", menuName = "Weapons/Bullet Gun")]
    public class BulletGunInitData : ScriptableObject
    {
        public GameObject gunPrefab;
        public GameObject bulletPrefab;
        public float reloadTime;
        public float bulletSpeed;
        public AudioClip bulletSFX;
        //[Tooltip("�������� ����� ����������")]
        //public float shootReloadDelay;
        //public int maxShootCount;
        //[Tooltip("����� �����������")]
        //public float laserReloadTime;

    }
}