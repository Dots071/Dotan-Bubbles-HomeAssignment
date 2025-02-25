using Game.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Game.Services;

public class TestPool : MonoBehaviour
{
    [SerializeField] private BallsListSO _ballsListSO;
    [SerializeField] private AssetReferencesSO _assetReferences;

    private ServiceLocatorSO _serviceLocatorSO;

    public List<GameObject> Balls = new List<GameObject>();

    private ObjectPoolSO _poolSO;



    private async void Start()
    {

        _serviceLocatorSO = await AddressablesService.LoadAssetAsync<ServiceLocatorSO>(_assetReferences.ServiceLocatorSO);

        Debug.Log($"[TestPool] ServiceLocator ID: {_serviceLocatorSO.GetInstanceID()}");

        SpawnBall().Forget();
    }

    private async UniTaskVoid SpawnBall()
    {
        _serviceLocatorSO.LogRegisteredServices();
        await UniTask.Delay(10000);
        Debug.Log("Delay finished!");
        _poolSO = _serviceLocatorSO.GetService<ObjectPoolSO>();

        var ball = _poolSO.GetObject(gameObject.transform);
        var random = Random.Range(0, 5);
        ball.transform.position = new Vector3((float)random, (float)random, 0);
        Balls.Add(ball);

    }

    private void CreatePool()
    {
        ObjectPoolSO runtimePool = ScriptableObject.CreateInstance<ObjectPoolSO>();
        runtimePool.prefab = _ballsListSO._ballPrefabs[0];
        runtimePool.initialPoolSize = 20;
        runtimePool.PrewarmPool();

        _serviceLocatorSO.RegisterService(runtimePool);
    }
}
