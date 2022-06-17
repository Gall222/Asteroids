using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class BulletInitData : ScriptableObject
    {
        //public GameObject bulletPrefab;
        //public float speed;
        public static BulletInitData LoadFromAssets()
        {
            return Resources.Load("Data/BulletInitData") as BulletInitData;
        }
    }
}