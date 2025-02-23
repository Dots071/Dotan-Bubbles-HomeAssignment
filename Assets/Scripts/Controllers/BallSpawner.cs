using Game.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Controllers
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab;

        [SerializeField] private float spawnInterval = 1f;
        [SerializeField] private int maxBalls = 60;

        [SerializeField] private float spawnMinX = -8f;
        [SerializeField] private float spawnMaxX = 8f;
        [SerializeField] private float spawnY = 6f;

        [SerializeField] private Transform spawnContainer;

        public event Action<IClickableBall> BallSpawned;

        // Track the number of balls currently active.
        private int currentBallCount = 0;

        private void Start()
        {
            StartCoroutine(SpawnBallsRoutine());
        }

        private IEnumerator SpawnBallsRoutine()
        {
            // Continue attempting to spawn balls indefinitely (or until the spawner is disabled)
            while (true)
            {
                if (currentBallCount < maxBalls)
                {
                    SpawnBall();
                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnBall()
        {
            float spawnX = UnityEngine.Random.Range(spawnMinX, spawnMaxX);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

            GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity, spawnContainer);

            var ball = newBall.GetComponent<IClickableBall>();
            BallSpawned?.Invoke(ball);
            // Optionally, initialize the ball's properties (color, type, etc.) here.
            // For example, if your BallModel is attached on a BallController script on the prefab:
            // newBall.GetComponent<BallController>()?.InitializeRandom();

            currentBallCount++;

            // Optionally, subscribe to an event from the ball when it is removed (or deactivated)
            // so that you can decrement currentBallCount.
            // For example:
            // BallView ballView = newBall.GetComponent<BallView>();
            // if(ballView != null)
            // {
            //    ballView.OnRemoved += () => currentBallCount--;
            // }
        }
    }

}
