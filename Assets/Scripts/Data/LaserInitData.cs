using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Laser", menuName = "Weapons/Laser")]
    public class LaserInitData : ScriptableObject
    {
        public GameObject laserPrefab;
        public float distance;
        //[Tooltip("Задержка между выстрелами")]
        public float attackTime;
        public int maxShootCount;
        //[Tooltip("Время перезарядки")]
        public float reloadTimerDelay;
        public float reloadTimerMax;
        public AudioClip laserSFX;
    }
    
}