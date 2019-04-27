using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    //creating a list that uses the pool class.
    public List<Pool> pools;

    //Creating a dictionary with a queue that stores game objects.
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    /* Pool class 
    *
    The pool stores
    tag; tag to identify different pools.
    prefab; set which prefab the object pool will use.
    size; size of the object pool.
    *
    */

    // make the pool class visible in the unity inspector.
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton

    public static ObjectPool Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    void Start()
    {
        //New empty dictionary
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //going through each pool that exist in the pool list.
        foreach (Pool pool in pools)
        {
            //Create a queue of game objects.
            Queue<GameObject> objectPool = new Queue<GameObject>();


            // fill the object pool with game objects
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            //the filled object pool gets added to the dictionary.
            poolDictionary.Add(pool.tag, objectPool);
        }
    }


    /*
     * 
     * method that spawns game objects
     * tag to check witch object to spawn.
     * pos; where the object should be spawned.
     * rot; the rotation of the game object.
     * 
     */
    public GameObject SpawnFromPool (string tag, Vector3 pos, Quaternion rot)
    {
        //Failsafe if given tag doesn't correspond with a existing queue.
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        //removes the object that will spawn from the queue
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        //gameobject created by the object pool becomes active.
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;

        //Add the object back in the queue
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
