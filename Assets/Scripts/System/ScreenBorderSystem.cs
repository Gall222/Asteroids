using Components;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Systems
{
    public class ScreenBorderSystem : IEcsRunSystem
    {
        public static Action<EcsEntity> OnReturnEnemy;

        EcsFilter<MovableComponent, InputEventComponent> playerInputFilter;
        EcsFilter<MovableComponent, ActiveMovableComponent> enemiesFilter;
        EcsFilter<MovableComponent, DestroyInBorder> destroyInBorderFilter;

        private static float _top;
        private static float _bottom;
        private static float _left;
        private static float _right;
        private static float _maxTop;
        private static float _maxBottom;
        private static float _maxLeft;
        private static float _maxRight;

        public static float Top => _top;
        public static float Bottom => _bottom;
        public static float Left => _left;
        public static float Right =>_right;
        public static float MaxTop => _maxTop;
        public static float MaxBottom => _maxBottom;
        public static float MaxLeft => _maxLeft;
        public static float MaxRight => _maxRight;
        public ScreenBorderSystem()
        {
            var camera = Camera.main;
            var zero = camera.ScreenToWorldPoint(Vector3.zero);
            var topRight = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight));
            _top = topRight.y;
            _bottom = zero.y;
            _right = topRight.x;
            _left = zero.x;
            _maxTop = _top * 2;
            _maxBottom = _bottom * 2;
            _maxLeft = _left * 2;
            _maxRight = _right * 2;
        }

        public void Run()
        {
            PlayerTeleport();
            EnemyReturn();
            DestroyInBorder();
        }

        private void DestroyInBorder()
        {
            foreach (var i in destroyInBorderFilter)
            {
                ref MovableComponent movableComponent = ref destroyInBorderFilter.Get1(i);
                ref DestroyInBorder destroyComponent = ref destroyInBorderFilter.Get2(i);
                var position = movableComponent.transform.position;

                if (IsOutOfMaxBorders(position))
                {
                    ref var entity = ref destroyInBorderFilter.GetEntity(i);
                    GameObject.Destroy(movableComponent.transform.gameObject);
                    entity.Destroy();
                }
            }
        }

      
        private void PlayerTeleport()
        {
            foreach (var i in playerInputFilter)
            {
                ref MovableComponent moveComponent = ref playerInputFilter.Get1(i);

                var position = moveComponent.transform.position;
                if (position.x > _right)
                {
                    position.x = _left;
                }
                else if (position.x < _left)
                {
                    position.x = _right;
                }
                if (position.y > _top)
                {
                    position.y = _bottom;
                }
                else if (position.y < _bottom)
                {
                    position.y = _top;
                }
                moveComponent.transform.position = (Vector2)position;
            }
        }
        private void EnemyReturn()
        {
            foreach (var i in enemiesFilter)
            {
                ref MovableComponent moveComponent = ref enemiesFilter.Get1(i);
                ref ActiveMovableComponent enemyComponent = ref enemiesFilter.Get2(i);

                var position = moveComponent.transform.position;

                if (IsOutOfMaxBorders(position))
                {
                    ref var entity = ref enemiesFilter.GetEntity(i);
                    OnReturnEnemy?.Invoke(entity);
                }
            }
        }
        private bool IsOutOfMaxBorders(Vector2 position)
        {
            return position.x > _maxRight || position.x < _maxLeft || position.y > _maxTop || position.y < _maxBottom;
        }

    }
}