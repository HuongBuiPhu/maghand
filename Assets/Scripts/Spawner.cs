using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public static Spawner INSTANCE;

    [Header("Space when object be spawned from pool")]
    [SerializeField]
    private Transform tranform_space = null;
    [Header("object prefabs custom")]
    [SerializeField]
    private ObjectPool[] object_pools = null;


    private Dictionary<string, Tuple<GameObject, GameObject, Queue<GameObject>>> pools;

    private void Awake() {
        INSTANCE = this;
        pools = new Dictionary<string, Tuple<GameObject, GameObject, Queue<GameObject>>>();
        foreach (ObjectPool object_pool in object_pools) {
            GameObject pool = new GameObject("Pool " + object_pool.tag);
            pool.transform.parent = this.transform;

            Queue<GameObject> queue_object = new Queue<GameObject>();
            for (int i = 0; i <= 20; i++) {
                GameObject object_clone = Instantiate(object_pool.prefab);
                object_clone.name = object_pool.tag + i.ToString();
                object_clone.transform.parent = pool.transform;
                object_clone.SetActive(false);
                queue_object.Enqueue(object_clone);
            }
            pools.Add(object_pool.tag, new Tuple<GameObject, GameObject, Queue<GameObject>>(pool, object_pool.prefab, queue_object));
        }
    }

    public void EnqueueObjectToPool(GameObject game_object, string tag) {
        if (!pools.ContainsKey(tag)) throw new Exception("No pool with tag: " + tag);
        game_object.transform.parent = this.pools[tag].first.transform;
        this.pools[tag].third.Enqueue(game_object);
        game_object.SetActive(false);
    }

    public GameObject DequeueObjectFromPool(string tag, Action<GameObject> action = null) {
        if (!pools.ContainsKey(tag)) throw new Exception("No pool with tag: " + tag);
        GameObject game_object = null;
        Queue<GameObject> queue_object = pools[tag].third;
        if (queue_object.Count < 1) {
            game_object = Instantiate(pools[tag].second);
        } else {
            game_object = queue_object.Dequeue();
        }
        game_object.transform.parent = tranform_space;
        if (null != action) action(game_object);
        game_object.SetActive(true);
        return game_object;
    }

}


[Serializable]
public struct ObjectPool {
    public string tag;
    public GameObject prefab;
}

public class Tuple<T, K, O> {

    public T first;
    public K second;
    public O third;

    public Tuple() {
        this.first = default;
        this.second = default;
        this.third = default;
    }

    public Tuple(T first, K second, O third) {
        this.first = first;
        this.second = second;
        this.third = third;
    }
}
