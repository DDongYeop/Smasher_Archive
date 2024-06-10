using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField] private PoolingListSO _poolingList;
    private Dictionary<string, Pool<PoolableMono>> _pools = new Dictionary<string, Pool<PoolableMono>>();

    private void Awake() 
    {
        if (Instance != null)
            Debug.LogError("Multiple PoolManager is running");
        Instance = this;

        foreach(var list in _poolingList.PoolList)
            CreatePool(list.Prefab, list.Count);

        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene(1);
    }

    public void CreatePool(PoolableMono prefab, int count = 10)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, transform, count);
        _pools.Add(prefab.gameObject.name, pool); //프리팹의 이름으로 풀을 만든다.
    }

    public PoolableMono Pop(string prefabName)
    {
        if(!_pools.ContainsKey(prefabName))
        {
            Debug.LogError($"Prefab does not exist on pool : {prefabName}");
            return null;
        }

        PoolableMono item = _pools[prefabName].Pop();
        item.Init();
        return item;
    }

    public void Push(PoolableMono obj)
    {
        _pools[obj.name].Push(obj);
    }
}