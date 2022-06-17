using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Laser", menuName = "Weapons/Laser")]
    public class LaserInitData : ScriptableObject
    {
        public GameObject laserPrefab;
        public float distance;
        //[Tooltip("�������� ����� ����������")]
        public float attackTime;
        public int maxShootCount;
        //[Tooltip("����� �����������")]
        public float reloadTimerDelay;
        public float reloadTimerMax;
        public AudioClip laserSFX;
    }
    
}