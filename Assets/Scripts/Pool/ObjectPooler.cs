using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    static public ObjectPooler instance;

    public Dictionary<string,Queue< GameObject>> poolDictionary;
    [SerializeField]
    private List<Pool> _pools;

    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;

    }

    [SerializeField]
    GameObject parentOfPooledObjs;



    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.SetParent(parentOfPooledObjs.transform);
            }
            poolDictionary.Add(pool.tag, objectPool);   
        }
    }
    public GameObject SpawnFromPool(string tag , Vector3 position , Quaternion rotation)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;


        IPooledObject _pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        if(_pooledObject != null)
        {
            _pooledObject.OnObjectSpawn();
        }
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
