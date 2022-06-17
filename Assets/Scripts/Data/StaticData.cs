using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        public GameObject playerPrefab;
        public EnemiesData enemiesData;
        public BulletGunInitData bulletGunInitData;
        public LaserInitData laserInitData;
        public EnemyData shatterInitData;
        public DestroyableData meteorDestroyableData;
        public int score;

        public Queue<EcsEntity> EnemiesEntities = new Queue<EcsEntity>();
    }

}