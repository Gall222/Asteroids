using Components;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class Loader : MonoBehaviour
    {
        public StaticData staticData;
        public SceneDataComponent sceneData;
        EcsWorld world;
        EcsSystems systems;

        void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);

            systems
                .Add(new GameInitSystem())
                .Add(new WeaponInitSystem())
                .Add(new EnemyInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerMoveSystem())
                .Add(new AnimationSystem())
                .Add(new FollowSystem())
                .Add(new DriftingSystem())
                .Add(new ScreenBorderSystem())
                .Add(new LaserSystem())
                .Add(new BulletGunSystem())
                .Add(new EnemySpawnSystem())
                .Add(new SpawnProjectileSystem())
                .Add(new EnemyHitSystem())
                .Inject(staticData)
                .Inject(sceneData)
                .Init();

        }


        void Update()
        {
            systems.Run();
        }

        private void OnDestroy()
        {
            systems.Destroy();
            world.Destroy();
        }
    }
}