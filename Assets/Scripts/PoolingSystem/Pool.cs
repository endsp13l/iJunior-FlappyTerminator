using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public class Pool<T> where T : MonoBehaviour, IPoolable
{
    private T _objectPrefab;
    private int _poolCapacity;
    private int _poolMaxSize;

    private ObjectPool<GameObject> _objectPool;

    public Pool(T objectPrefab, int poolCapacity, int poolMaxSize)
    {
        _objectPrefab = objectPrefab;
        _poolCapacity = poolCapacity;
        _poolMaxSize = poolMaxSize;
    }

    public event Action<T> Getted;
    public event Action<T> Released;
    public event Action Cleared;

    public void Initialize()
    {
        _objectPool = new ObjectPool<GameObject>(
            createFunc: () => CreateObject(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => ActionOnDestroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public GameObject Get() => _objectPool.Get();

    public void Clear()
    {
        Cleared?.Invoke();
        _objectPool.Clear();
    }
    
    private GameObject CreateObject()
    {
        GameObject obj = Object.Instantiate(_objectPrefab.gameObject);

        GetCurrentTypeComponent(obj).Destroyed += _objectPool.Release;
        Cleared += GetCurrentTypeComponent(obj).Clear;
        obj.SetActive(false);

        return obj;
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);

        Getted?.Invoke(GetCurrentTypeComponent(obj));
    }

    private void ActionOnRelease(GameObject obj)
    {
        obj.SetActive(false);
        Released?.Invoke(GetCurrentTypeComponent(obj));
    }

    private void ActionOnDestroy(GameObject obj)
    {
        GetCurrentTypeComponent(obj).Destroyed -= _objectPool.Release;
        Cleared -= GetCurrentTypeComponent(obj).Clear;
        Object.Destroy(obj);
    }

    private T GetCurrentTypeComponent(GameObject obj)
    {
        obj.TryGetComponent(out T component);
        return component;
    }
}