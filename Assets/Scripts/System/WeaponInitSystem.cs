using Components;
using Leopotam.Ecs;
using Data;
using UnityEngine;
using System;

namespace Systems
{
    public class WeaponInitSystem : IEcsInitSystem
    {
        EcsWorld _world = null;
        private StaticData _staticData;
        private SceneDataComponent _sceneData;
        public void Init()
        {
            var spawnedPlayerPrefab = GameObject.Find("Player");
            LaserCreate(spawnedPlayerPrefab.transform.Find("Laser Position"));
            BulletGunCreate(spawnedPlayerPrefab.transform.Find("Left Gun Position"));
            BulletGunCreate(spawnedPlayerPrefab.transform.Find("Right Gun Position"));

        }

        private void BulletGunCreate(Transform position)
        {
            var bulletGun = _world.NewEntity();
            ref BulletGunComponent bulletComponent = ref bulletGun.Get<BulletGunComponent>();
            var BulletGunData = _staticData.bulletGunInitData;
            var spawnedGun = GameObject.Instantiate(BulletGunData.gunPrefab, Vector3.zero, Quaternion.identity.normalized);
            spawnedGun.transform.parent = position;
            spawnedGun.transform.localPosition = Vector3.zero;
            bulletComponent.transform = spawnedGun.transform;
            bulletComponent.bulletPrefab = BulletGunData.bulletPrefab;
            bulletComponent.reloadTime = BulletGunData.reloadTime;
            bulletComponent.projectileSpeed = BulletGunData.bulletSpeed;
            bulletComponent.bulletSFX = BulletGunData.bulletSFX;
        }

        private void LaserCreate(Transform position)
        {
            var laser = _world.NewEntity();
            ref LaserComponent laserComponent = ref laser.Get<LaserComponent>();
            var LaserData = _staticData.laserInitData;
            var spawnedLaser = GameObject.Instantiate(LaserData.laserPrefab, Vector3.zero, Quaternion.identity.normalized);
            spawnedLaser.transform.parent = position;
            spawnedLaser.transform.localPosition = Vector3.zero;
            laserComponent.attackTime = LaserData.attackTime;
            laserComponent.maxShootCount = LaserData.maxShootCount;
            laserComponent.reloadTimerDelay = LaserData.reloadTimerDelay;
            laserComponent.reloadTimerMax = laserComponent.reloadTimer = LaserData.reloadTimerMax;
            laserComponent.transform = spawnedLaser.transform;
            laserComponent.lineRenderer = spawnedLaser.transform.GetComponent<LineRenderer>();
            laserComponent.distance = LaserData.distance;
            laserComponent.damageLine = spawnedLaser.transform.Find("Damage Line");
            laserComponent.laserSFX = LaserData.laserSFX;
            _sceneData.SetLaserShootCount($"{laserComponent.anableShootCount}");
            _sceneData.SetLaserReloadText($"{laserComponent.reloadTimer}");
            //Debug.Log(laserComponent.lineRenderer);
        }

    }
}