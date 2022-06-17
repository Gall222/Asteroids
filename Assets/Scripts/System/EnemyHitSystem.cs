using Components;
using Data;
using Leopotam.Ecs;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems
{
    public class EnemyHitSystem : IEcsRunSystem
    {
        EcsWorld _world;
        private StaticData _staticData;
        public SceneDataComponent sceneData;

        public static Action<EcsEntity> OnReturnEnemy;

        private EcsFilter<HitComponent> _hitsFilter;

        public void Run()
        {

            foreach (var i in _hitsFilter)
            {
                ref var hitComponent = ref _hitsFilter.Get1(i);

                ref var enemyEntity = ref _hitsFilter.GetEntity(i);
                
                //если пуля, то уничтожить ее
                if (hitComponent.other.CompareTag("Bullet"))
                {
                    var bullet = hitComponent.other.GetComponent<BulletView>();
                    bullet.DestroyBullet();
                }
                //столкновение с игроком - конец игры, в других случаях - возврат в пулл
                else if (hitComponent.other.CompareTag("Player"))
                {
                    Time.timeScale = 0;
                    sceneData.gameOverMenu.SetActive(true);
                }

                    if (hitComponent.first.CompareTag("Destroyable") && !hitComponent.other.CompareTag("Laser"))
                    {
                        ref DestroyableComponent enemyDestroyableComponent = ref enemyEntity.Get<DestroyableComponent>();
                        
                        for (int j = 0; j < enemyDestroyableComponent.shattersCount; j++)
                        {
                            CreateShatters(ref enemyEntity);
                        }
                    }

                OnEnemyHit(enemyEntity);
                //если осколок, то удаляем сущность и объект, иначе возвращаем в пулл и удаляем метку HitComponent
                if (hitComponent.first.CompareTag("Shatter"))
                    DestroyEnemy(ref enemyEntity, hitComponent.first);
                else
                {
                    ReturnEnemy(ref enemyEntity);
                    enemyEntity.Del<HitComponent>();
                }
                   
            }


        }
        private void OnEnemyHit(EcsEntity enemyEntity)
        {
            ref EnemyComponent enemyComponent = ref enemyEntity.Get<EnemyComponent>();
            AudioSource.PlayClipAtPoint(enemyComponent.destroySFX, new Vector3());
            _staticData.score += enemyComponent.scorePoints;
            sceneData.SetScoreText(_staticData.score.ToString());

        }

        private void CreateShatters(ref EcsEntity enemyEntity)
        {
            ref MovableComponent meteorMovableComponent = ref enemyEntity.Get<MovableComponent>();
            ref DriftingComponent meteorDriftingComponent = ref enemyEntity.Get<DriftingComponent>();
            var shatter = _world.NewEntity();
            ref MovableComponent shatterMovableComponent = ref shatter.Get<MovableComponent>();
            ref DriftingComponent shatterDriftingComponent = ref shatter.Get<DriftingComponent>(); 
            ref DestroyInBorder shatterDestroyComponent = ref shatter.Get<DestroyInBorder>(); 
            ref AnimationComponent shatterAnimationComponent = ref shatter.Get<AnimationComponent>();
            ref ActiveMovableComponent newShatterComponent = ref shatter.Get<ActiveMovableComponent>();
            ref EnemyComponent enemyComponent = ref shatter.Get<EnemyComponent>();
            enemyComponent.scorePoints = _staticData.shatterInitData.scorePoints;
            enemyComponent.destroySFX = _staticData.shatterInitData.destroySFX;
            var meteorPos = meteorMovableComponent.transform.position;
            var spawnedShatterPrefab = GameObject.Instantiate(_staticData.shatterInitData.enemyPrefab,
                new Vector2(Random.Range(meteorPos.x - 1, meteorPos.x + 1), Random.Range(meteorPos.y - 1, meteorPos.y + 1)),
                Quaternion.identity.normalized);
            var enemyView = spawnedShatterPrefab.GetComponent<EnemyView>();
            enemyView.enemyEntity = shatter;
            shatterMovableComponent.transform = spawnedShatterPrefab.transform;
            shatterMovableComponent.moveSpeed = meteorMovableComponent.moveSpeed * 1.5f;
            shatterAnimationComponent.animator = spawnedShatterPrefab.transform.GetComponent<Animator>();
            shatterDriftingComponent.direction = meteorDriftingComponent.direction;
        }

        private void ReturnEnemy(ref EcsEntity enemyEntity)
        {
            OnReturnEnemy?.Invoke(enemyEntity);
        }
        private void DestroyEnemy(ref EcsEntity enemyEntity, GameObject obj)
        {
            GameObject.Destroy(obj);
            enemyEntity.Destroy();
        }

    }
}