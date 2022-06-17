using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Enemy Info/Enemy Data", fileName = "Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public GameObject enemyPrefab;
        public EnemiesType enemyType;
        public int scorePoints;
        public AudioClip destroySFX;
        public enum EnemiesType
        {
            Meteor,
            UFO,
            Shatter
        }
    }
}