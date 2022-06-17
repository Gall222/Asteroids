using Leopotam.Ecs;
using Components;
using UnityEngine;

namespace Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        EcsFilter<InputEventComponent> inputEventsFilter = null;
        EcsFilter<LaserComponent> laserFilter = null;
        EcsFilter<BulletGunComponent> bulletGunsFilter = null;
        public PlayerInput playerInput = new PlayerInput();
        public PlayerInputSystem()
        {
            playerInput.Enable();
        }
        public void Run()
        {
            var moveDirection = playerInput.Player.Moving.ReadValue<Vector2>();

            if (moveDirection != null)
            {
                AddSpeed(moveDirection);
            }
            LaserShoot();
            BulletGunShoot();
        }
        private void AddSpeed(Vector2 moveDirection)
        {
            foreach (var i in inputEventsFilter)
            {
                ref InputEventComponent inputComponent = ref inputEventsFilter.Get1(i);
                inputComponent.direction = moveDirection;
                
                if (moveDirection.y > 0)
                {
                    if (inputComponent.currentSpeed < inputComponent.maxSpeed)
                    {
                        inputComponent.currentSpeed += inputComponent.speedIncreaseStep * Time.deltaTime;
                    }
                }
                else
                {
                    inputComponent.currentSpeed -= inputComponent.speedIncreaseStep / 2 * Time.deltaTime;
                    if (inputComponent.currentSpeed < 0)
                        inputComponent.currentSpeed = 0;
                }
            }    
        }
        private void LaserShoot()
        {
            foreach (var i in inputEventsFilter)
            {
                ref LaserComponent laserComponent = ref laserFilter.Get1(i);
                laserComponent.isShooting = playerInput.Player.LaserShoot.IsPressed();
            }
        }
        private void BulletGunShoot()
        {
            foreach (var i in bulletGunsFilter)
            {
                ref BulletGunComponent bulletGunComponent = ref bulletGunsFilter.Get1(i);
                bulletGunComponent.isShooting = playerInput.Player.BulletShoot.IsPressed();
                
            }
        }
    }
}