using Game.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Controllers;

namespace Game.Boot
{
    public class GameplayBoot : MonoBehaviour
    {
        [SerializeField] private BallsListSO _ballsListSO;
        [SerializeField] private ServiceLocatorSO _serviceLocatorSO;
        [SerializeField] private GameEventAggregator _gameEventAggregator;

        [SerializeField] private Transform _ballsContainer;

        private BallsController _ballsController;
        private BallSpawner _ballSpawner;

        void Start()
        {
            BindObjects();
        }

        private void BindObjects()
        {
            _ballSpawner = new BallSpawner(_ballsListSO, _gameEventAggregator, _ballsContainer);
            _ballsController = new BallsController(_gameEventAggregator);
        }

        private void OnDestroy()
        {
            _ballsController.Dispose();
            _ballSpawner.Dispose();
        }
    }
}