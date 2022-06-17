using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Enemy Info/Destroyable Data", fileName = "Destroyable Data")]
    public class DestroyableData : ScriptableObject
    {
        public int shattersCount;
    }
}