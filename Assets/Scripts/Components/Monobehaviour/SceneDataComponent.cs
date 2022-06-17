using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Components
{
    public class SceneDataComponent : MonoBehaviour
    {
        public List<SpawnPoint> spawnPoints;
        public GameObject gameOverMenu;
        [SerializeField] TextMeshProUGUI laserShootCountText;
        [SerializeField] TextMeshProUGUI laserReloadText;
        [SerializeField] TextMeshProUGUI shipSpeedText;
        [SerializeField] TextMeshProUGUI shipDegreeText;
        [SerializeField] TextMeshProUGUI shipPositionText;
        [SerializeField] TextMeshProUGUI scoreText;

        public void SetLaserShootCount(string count)
        {
            laserShootCountText.text = count;
        }
        public void SetLaserReloadText(string time)
        {
            laserReloadText.text = time;
        }
        public void SetShipSpeedText(string speed)
        {
            shipSpeedText.text = speed;
        }
        public void SetShipDegreeText(string degree)
        {
            shipDegreeText.text = degree;
        }
        public void SetShipPositionText(string position)
        {
            shipPositionText.text = position;
        }
        public void SetScoreText(string points)
        {
            scoreText.text = points;
        }
    }
}