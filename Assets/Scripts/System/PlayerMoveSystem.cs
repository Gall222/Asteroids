using Leopotam.Ecs;
using Components;
using UnityEngine;
using System;

namespace Systems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private SceneDataComponent _sceneData;
        EcsFilter<MovableComponent, InputEventComponent> playerMoveFilter;

        public void Run()
        {
            foreach (var i in playerMoveFilter)
            {
                ref MovableComponent movableComponent = ref playerMoveFilter.Get1(i);
                ref InputEventComponent inputComponent = ref playerMoveFilter.Get2(i);

                PlayerMove(movableComponent, inputComponent);
                PlayerRotate(movableComponent, inputComponent);
            }
        }

        private void PlayerMove(MovableComponent movableComponent, InputEventComponent inputComponent)
        {
            movableComponent.transform.Translate(movableComponent.transform.up * inputComponent.currentSpeed * Time.deltaTime, Space.World);
            movableComponent.isMoving = inputComponent.currentSpeed > 0;
            _sceneData.SetShipSpeedText($"Speed: {Math.Round(inputComponent.currentSpeed, 1)}");
            _sceneData.SetShipPositionText($"Position: x:{Math.Round(movableComponent.transform.position.x, 1)}, y:{Math.Round(movableComponent.transform.position.y, 1)}");
        }
        private void PlayerRotate(MovableComponent movableComponent, InputEventComponent inputComponent)
        {
            if (inputComponent.direction.x < 0)
            {
                movableComponent.transform.Rotate(Vector3.forward * inputComponent.rotateSpeed * Time.deltaTime);
            }
            else if (inputComponent.direction.x > 0)
            {
                movableComponent.transform.Rotate(-Vector3.forward * inputComponent.rotateSpeed * Time.deltaTime);
            }
            _sceneData.SetShipDegreeText($"Degree: {Math.Round(movableComponent.transform.eulerAngles.z, 1)}");
        }
    }
}