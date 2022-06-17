using UnityEngine;
using Leopotam.Ecs;
using Components;
using Random = UnityEngine.Random;
using Data;

namespace Systems
{
    public class EnemySpawnSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        EcsWorld _world;
        private StaticData _staticData;
        private SceneDataComponent _sceneData;
        private float nextSpawnTime;

        EcsFilter<MovableComponent, ActiveMovableComponent> inputEventsFilter;

        public void Init()
        {
            ScreenBorderSystem.OnReturnEnemy += ReturnEnemy;
            EnemyHitSystem.OnReturnEnemy += ReturnEnemy;
        }
        public void Run()
        {
            //Debug.Log(1);
            AddRandomEnemyToScene();
        }
        private void AddRandomEnemyToScene()
        {
            if (Time.time < nextSpawnTime) { return; }
            var spawnPoint = _sceneData.spawnPoints[Random.Range(0, _sceneData.spawnPoints.Count)];
            nextSpawnTime = Time.time + spawnPoint.spawnIntervalTime;
            if (_staticData.EnemiesEntities.Count > 0)
            {
                var currentEnemyEntity = _staticData.EnemiesEntities.Dequeue();
                ref var enemyComponent = ref currentEnemyEntity.Get<ActiveMovableComponent>();
                ref var movableComponent = ref currentEnemyEntity.Get<MovableComponent>();
                var enemyObject = movableComponent.transform.gameObject;
                enemyObject.SetActive(true);
                enemyObject.transform.position = GetEnemyPosition(spawnPoint);
                if (movableComponent.isDrifting)
                {
                    currentEnemyEntity.Get<DriftingComponent>().direction = GetEnemyDirection(spawnPoint);
                }

            }
        }
        private Vector2 GetEnemyDirection(SpawnPoint spawnPoint)
        {
            switch (spawnPoint.direction)
            {
                case SpawnPoint.Directions.top:
                    return new Vector2(Random.Range(-0.5f, 0.5f), -1);
                case SpawnPoint.Directions.left:
                    return new Vector2(1, Random.Range(-0.5f, 0.5f));
                case SpawnPoint.Directions.right:
                    return new Vector2(-1, Random.Range(-0.5f, 0.5f));
                default:
                    return new Vector2(Random.Range(-0.5f, 0.5f), 1);
            }
        }
        private Vector2 GetEnemyPosition(SpawnPoint spawnPoint)
        {
            switch (spawnPoint.direction)
            {
                case SpawnPoint.Directions.left:
                    return new Vector2(((Vector2)Camera.main.ScreenToWorldPoint(spawnPoint.transform.position)).x,
                       Random.Range(ScreenBorderSystem.Bottom, ScreenBorderSystem.Top));
                case SpawnPoint.Directions.right:
                    return new Vector2(((Vector2)Camera.main.ScreenToWorldPoint(spawnPoint.transform.position)).x,
                        Random.Range(ScreenBorderSystem.Bottom, ScreenBorderSystem.Top));
                default:
                    return new Vector2(Random.Range(ScreenBorderSystem.Left, ScreenBorderSystem.Right),
                        ((Vector2)Camera.main.ScreenToWorldPoint(spawnPoint.transform.position)).y);
            }
        }

        private void ReturnEnemy(EcsEntity enemy)
        {
            ref MovableComponent enemyMovableComponent = ref enemy.Get<MovableComponent>();
            enemyMovableComponent.transform.gameObject.SetActive(false);
            enemy.Del<ActiveMovableComponent>();
            _staticData.EnemiesEntities.Enqueue(enemy);
        }



        public void Destroy()
        {
            ScreenBorderSystem.OnReturnEnemy -= ReturnEnemy;
            EnemyHitSystem.OnReturnEnemy -= ReturnEnemy;
        }
    }
}