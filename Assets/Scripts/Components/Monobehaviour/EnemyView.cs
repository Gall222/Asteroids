using UnityEngine;
using Leopotam.Ecs;

namespace Components
{
    public class EnemyView : MonoBehaviour
    {
        public EcsEntity enemyEntity;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ref var hitComponent = ref enemyEntity.Get<HitComponent>();

            hitComponent.first = transform.root.gameObject;
            hitComponent.other = collision.gameObject;
        }
    }
}