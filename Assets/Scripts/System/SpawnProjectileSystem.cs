using Components;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class SpawnProjectileSystem : IEcsRunSystem
    {
        private EcsFilter<BulletGunComponent, SpawnProjectile> _filter;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var weapon = ref _filter.Get1(i);
                // Создаем GameObject пули и ее сущность
                var projectileGO = Object.Instantiate(weapon.bulletPrefab, weapon.transform.position, weapon.transform.rotation);
                var bulletView = projectileGO.GetComponent<BulletView>();
                var projectileEntity = _ecsWorld.NewEntity();
                bulletView.bulletEntity = projectileEntity;
                ref MovableComponent bulletMovableComponent = ref projectileEntity.Get<MovableComponent>();
                ref ActiveMovableComponent bulletActiveComponent = ref projectileEntity.Get<ActiveMovableComponent>();
                ref DriftingComponent bulletDriftingComponent = ref projectileEntity.Get<DriftingComponent>();
                ref DestroyInBorder bulletDestroyComponent = ref projectileEntity.Get<DestroyInBorder>();
                bulletDriftingComponent.direction = weapon.transform.up;
                bulletMovableComponent.moveSpeed = weapon.projectileSpeed;
                bulletMovableComponent.transform = projectileGO.transform;
                
                ref var entity = ref _filter.GetEntity(i);
                entity.Del<SpawnProjectile>();
            }
        }
    }
}