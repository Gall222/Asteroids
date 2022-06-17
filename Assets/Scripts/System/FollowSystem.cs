using Leopotam.Ecs;
using Components;
using UnityEngine;

namespace Systems
{
    public class FollowSystem : IEcsRunSystem
    {
        EcsFilter<FollowComponent, MovableComponent, ActiveMovableComponent> followFilter;
        public void Run()
        {
            foreach (var i in followFilter)
            {
                ref FollowComponent followComponent = ref followFilter.Get1(i);
                ref MovableComponent movableComponent = ref followFilter.Get2(i);
                //Debug.Log(movableComponent.moveSpeed);
                if (followComponent.target)
                {
                    movableComponent.transform.position = Vector3.MoveTowards(movableComponent.transform.position,
                        followComponent.target.transform.position, movableComponent.moveSpeed * Time.deltaTime);
                }
            }
        }
    }
}