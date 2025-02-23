using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;

namespace Game.Controllers
{
    public class BallsController : MonoBehaviour
    {
        [SerializeField] private BallSpawner _spawner;
        private readonly List<IClickableBall> _activeBalls = new List<IClickableBall>();

        private void OnEnable()
        {
            _spawner.BallSpawned += OnBallSpawned;
        }

        private void OnDisable()
        {
            _spawner.BallSpawned -= OnBallSpawned;
            foreach (var ball in _activeBalls)
            {
                ball.OnBallClick -= OnBallClicked;
            }
        }

        private void OnBallSpawned(IClickableBall ball)
        {
            _activeBalls.Add(ball);
            ball.OnBallClick += OnBallClicked;
        }

        private void OnBallClicked(IClickableBall clickedBall)
        {
            var connectedBalls = GetConnectedBalls(clickedBall, 1.2f);

            if (connectedBalls.Count < 3)
            {
                Debug.Log("Miss!");
                return;
            }

            foreach (var ball in connectedBalls)
            {
                ball.ExplodeBall();
            }

            _activeBalls.RemoveAll(connectedBalls.Contains);
        }

        /// <summary>
        /// Returns all balls that are connected and of the same type as the clicked ball, within the given radius.
        /// </summary>
        private List<IClickableBall> GetConnectedBalls(IClickableBall clickedBall, float radius)
        {
            List<IClickableBall> connected = new List<IClickableBall>();
            HashSet<IClickableBall> visited = new HashSet<IClickableBall>();
            Queue<IClickableBall> queue = new Queue<IClickableBall>();

            var clickedType = clickedBall.BallType;

            queue.Enqueue(clickedBall);
            visited.Add(clickedBall);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                connected.Add(current);

                // Check for adjacent balls that match the clicked ball's type.
                foreach (var other in _activeBalls)
                {
                    var distance = Vector2.Distance(current.BallPosition, other.BallPosition);

                    if (!visited.Contains(other) && other.BallType == clickedType && distance <= radius)
                    {
                        visited.Add(other);
                        queue.Enqueue(other);
                    }
                }
            }

            return connected;
        }
    }
}
