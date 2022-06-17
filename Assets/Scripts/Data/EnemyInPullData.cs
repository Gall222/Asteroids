using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Enemy Info/Enemy In Pull", fileName = "Enemy In Pull")]
    public class EnemyInPullData : EnemyData
    {
        public float defaultSpeed;
        public int countInPull;
    }
}