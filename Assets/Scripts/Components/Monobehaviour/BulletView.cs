using Leopotam.Ecs;
using UnityEngine;

namespace Components
{
    public class BulletView : MonoBehaviour
    {
        public EcsEntity bulletEntity;

        public void DestroyBullet()
        {
            if(bulletEntity.IsAlive())
                bulletEntity.Destroy();
            Destroy(gameObject);
        }
    }
}