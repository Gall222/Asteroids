using Leopotam.Ecs;
using Components;

namespace Systems
{
    public class AnimationSystem : IEcsRunSystem
    {
        EcsFilter<AnimationComponent, MovableComponent> animationsFilter;
        public void Run()
        {
            foreach (var i in animationsFilter)
            {
                ref AnimationComponent animationComponent = ref animationsFilter.Get1(i);
                ref MovableComponent moveComponent = ref animationsFilter.Get2(i);

                //animationComponent.animator.SetBool("IsMoving", moveComponent.isMoving);

            }
        }
    }
}