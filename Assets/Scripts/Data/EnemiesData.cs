using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    [CreateAssetMenu]
    public class EnemiesData : ScriptableObject
    {
        public List<EnemyInPullData> enemies;
        //public static EnemiesData LoadFromAssets()
        //{
        //    return Resources.Load("Data/EnemiesData") as EnemiesData;
        //}
    }
}