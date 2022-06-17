using Leopotam.Ecs;
using Components;
using UnityEngine;

namespace Systems
{
    public class DriftingSystem : IEcsRunSystem
    {
        EcsFilter<DriftingComponent, MovableComponent, ActiveMovableComponent> enemyFilter;
        public void Run()
        {
            Moving();
        }

        private void Moving()
        {
            foreach (var i in enemyFilter)
            {
                ref DriftingComponent driftingComponent = ref enemyFilter.Get1(i);
                ref MovableComponent movableComponent = ref enemyFilter.Get2(i);
                movableComponent.transform.Translate(driftingComponent.direction * movableComponent.moveSpeed * Time.deltaTime, Space.World);
            }
        }

    }
}