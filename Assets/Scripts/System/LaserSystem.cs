using Leopotam.Ecs;
using Components;
using UnityEngine;

namespace Systems
{
    public class LaserSystem : IEcsRunSystem
    {
        EcsFilter<LaserComponent> _laserFilter;
        private SceneDataComponent _sceneData;
        public void Run()
        {
            foreach (var i in _laserFilter)
            {
                ref LaserComponent laserComponent = ref _laserFilter.Get1(i);
                if (laserComponent.isShooting && !laserComponent.isReloading && laserComponent.anableShootCount > 0)
                {
                    laserComponent.laserOn = true;
                    laserComponent.timeToStop = Time.time + laserComponent.attackTime;
                    laserComponent.isReloading = true;
                    laserComponent.anableShootCount--;
                    _sceneData.SetLaserShootCount($"{laserComponent.anableShootCount}");
                    AudioSource.PlayClipAtPoint(laserComponent.laserSFX, new Vector3());
                }

                LaserShoot(ref laserComponent);
                if (laserComponent.anableShootCount < laserComponent.maxShootCount)
                    LaserReload(ref laserComponent);
                    
                    
            }
        }

        private void LaserReload(ref LaserComponent laserComponent)
        {

            if (Time.time < laserComponent.nextReloadTime) { return; }

            if (laserComponent.reloadTimer > 0)
            {
                laserComponent.reloadTimer--;
            }
            else
            {
                laserComponent.reloadTimer = laserComponent.reloadTimerMax;
                laserComponent.anableShootCount++;
                _sceneData.SetLaserShootCount($"{laserComponent.anableShootCount}");
            }
            _sceneData.SetLaserReloadText($"{laserComponent.reloadTimer}");
            laserComponent.nextReloadTime = Time.time + laserComponent.reloadTimerDelay;
        }

        private void LaserShoot(ref LaserComponent laserComponent)
        {
            if (laserComponent.laserOn)
            {
                Draw2DRay(ref laserComponent, laserComponent.transform.position, laserComponent.transform.up * laserComponent.distance);
                if (Time.time >= laserComponent.timeToStop)
                {
                    StopFire(ref laserComponent);
                }
            }
        }

        //рисуем линию лазера
        private void Draw2DRay(ref LaserComponent laserComponent,Vector2 startPos, Vector2 endPos)
        {
            laserComponent.lineRenderer.enabled = true;
            laserComponent.lineRenderer.SetPosition(0, startPos);
            laserComponent.lineRenderer.SetPosition(1, endPos);
            laserComponent.damageLine.gameObject.SetActive(true);
            laserComponent.damageLine.gameObject.transform.localScale = new Vector2(1, laserComponent.distance);
        }
        private void StopFire(ref LaserComponent laserComponent)
        {
            laserComponent.lineRenderer.enabled = false;
            laserComponent.damageLine.gameObject.SetActive(false);
            laserComponent.laserOn = false;
            laserComponent.isReloading = false;
        }

    }
}