using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class BulletGunSystem : IEcsRunSystem
    {
        EcsWorld _world;
        EcsFilter<BulletGunComponent> bulletGunsFilter;

        public void Run()
        {
            foreach (var i in bulletGunsFilter)
            {
                ref BulletGunComponent bulletGunComponent = ref bulletGunsFilter.Get1(i);
                if (bulletGunComponent.isShooting && Time.time >= bulletGunComponent.nextShootTime)
                {
                    bulletGunComponent.nextShootTime = Time.time + bulletGunComponent.reloadTime;
                    ref var entity = ref bulletGunsFilter.GetEntity(i);
                    ref var spawnProjectile = ref entity.Get<SpawnProjectile>();
                    AudioSource.PlayClipAtPoint(bulletGunComponent.bulletSFX, new Vector3());
                }
            }
        }

    }
}