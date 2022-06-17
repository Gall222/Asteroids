using Leopotam.Ecs;
using System;
using Components;
using UnityEngine;
using Random = UnityEngine.Random;
using Data;

namespace Systems
{
    public class GameInitSystem : IEcsInitSystem
    {
    public static Action LaserFire;

    EcsWorld _world = null;
        private StaticData _staticData; 

        public void Init()
    {
            Time.timeScale = 1;
            _staticData.score = 0;
            var player = _world.NewEntity();
            ref InputEventComponent inputComponent = ref player.Get<InputEventComponent>();
            ref MovableComponent movableComponent = ref player.Get<MovableComponent>();
            ref AnimationComponent animationComponent = ref player.Get<AnimationComponent>();

            var playerData = PlayerInitData.LoadFromAssets();
            var spawnedPlayerPrefab = GameObject.Instantiate(playerData.playerPref, Vector3.zero, Quaternion.identity.normalized);
            spawnedPlayerPrefab.name = "Player";
            animationComponent.animator = spawnedPlayerPrefab.transform.GetComponent<Animator>();
        
            movableComponent.transform = spawnedPlayerPrefab.transform;
            inputComponent.rotateSpeed = playerData.rotateSpeed;
            inputComponent.speedIncreaseStep = playerData.speedIncreaseStep;
            inputComponent.maxSpeed = playerData.maxSpeed;

            _staticData.playerPrefab = spawnedPlayerPrefab;
    }




    }
}