using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class PlayerInitData : ScriptableObject
    {
        public GameObject playerPref;
        public float rotateSpeed = 2f;
        public float maxSpeed = 5f;
        public float speedIncreaseStep = 2f;


        public static PlayerInitData LoadFromAssets()
        {
            return Resources.Load("Data/PlayerInitData") as PlayerInitData;
        }
    }
}