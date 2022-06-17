using Components;
using Data;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class EnemyInitSystem : IEcsInitSystem
    {
        EcsWorld _world = null;
        private StaticData _staticData;
        private List<EcsEntity> tempEnemiesList = new List<EcsEntity>();

        public void Init()
        {
            //var enemiesDataList = EnemiesData.LoadFromAssets();
            _staticData.EnemiesEntities = new Queue<EcsEntity>();
            var enemiesDataList = _staticData.enemiesData;
            foreach (var enemyData in enemiesDataList.enemies)
            {
                for (int i = 0; i < enemyData.countInPull; i++)
                {
                    CreateEnemy(enemyData);
                }
            }
            int count = tempEnemiesList.Count;
            for (int i = 0; i < count; i++)
            {
                var randomIndex = Random.Range(0, tempEnemiesList.Count);
                _staticData.EnemiesEntities.Enqueue(tempEnemiesList[randomIndex]);
                tempEnemiesList.Remove(tempEnemiesList[randomIndex]);
            }
        }
        
        private void CreateEnemy(EnemyInPullData enemyData)
        {
            var enemy = _world.NewEntity();
            var spawnedEnemyPrefab = GameObject.Instantiate(enemyData.enemyPrefab, Vector3.zero, Quaternion.identity.normalized);
            var enemyView = spawnedEnemyPrefab.GetComponent<EnemyView>();
            ref MovableComponent enemyMovableComponent = ref enemy.Get<MovableComponent>();
            enemyView.enemyEntity = enemy;
            enemyMovableComponent.transform = spawnedEnemyPrefab.transform;
            enemyMovableComponent.moveSpeed = enemyData.defaultSpeed;
            AddEnemyComponents(ref enemy,ref spawnedEnemyPrefab, enemyData.enemyType);
            spawnedEnemyPrefab.gameObject.SetActive(false);
            ref EnemyComponent enemyComponent = ref enemy.Get<EnemyComponent>();
            enemyComponent.scorePoints = enemyData.scorePoints;
            enemyComponent.destroySFX = enemyData.destroySFX;
            tempEnemiesList.Add(enemy);


        }
        private void AddEnemyComponents(ref EcsEntity enemyEntity,ref GameObject enemyObj, EnemyData.EnemiesType type)
        {
            switch (type)
            {
                case EnemyData.EnemiesType.Meteor:
                    enemyEntity.Get<MovableComponent>().isDrifting = true;
                    ref AnimationComponent enemyAnimationComponent = ref enemyEntity.Get<AnimationComponent>();
                    enemyAnimationComponent.animator = enemyObj.transform.GetComponent<Animator>();
                    ref DriftingComponent enemyDriftingComponent = ref enemyEntity.Get<DriftingComponent>();
                    ref DestroyableComponent enemyDestroyableComponent = ref enemyEntity.Get<DestroyableComponent>();
                    enemyDestroyableComponent.shattersCount = _staticData.meteorDestroyableData.shattersCount;
                    break;
                case EnemyData.EnemiesType.UFO:
                    ref FollowComponent enemyFollowComponent = ref enemyEntity.Get<FollowComponent>();
                    enemyFollowComponent.target = _staticData.playerPrefab.transform;
                    break;
                case EnemyData.EnemiesType.Shatter:
                    enemyEntity.Get<MovableComponent>().isDrifting = true;
                    ref DriftingComponent shatterDriftingComponent = ref enemyEntity.Get<DriftingComponent>();
                    ref AnimationComponent shatterAnimationComponent = ref enemyEntity.Get<AnimationComponent>();
                    enemyAnimationComponent.animator = enemyObj.transform.GetComponent<Animator>();
                    break;
            }
        }

    }
}