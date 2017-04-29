using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour {

    Dictionary<string, Queue<ObjectInstance>> poolDictionary = new Dictionary<string, Queue<ObjectInstance>> ();

    static PoolManager _instance;

    public static PoolManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PoolManager> ();
            }
            return _instance;
        }
    }

	public void CreatePool (GameObject prefab, int poolSize) {
		string poolKey = prefab.name;
        GameObject poolHolder = new GameObject (prefab.name + "Pool");
        poolHolder.transform.parent = transform;
        if (!poolDictionary.ContainsKey (poolKey)) {
            poolDictionary.Add (poolKey, new Queue<ObjectInstance> ());
            for (int i = 0; i < poolSize; i++) {
                ObjectInstance newObject = new ObjectInstance (Instantiate (prefab) as GameObject);
                poolDictionary[poolKey].Enqueue (newObject);
                newObject.SetParent (poolHolder.transform);
            }
        }
    }

    public GameObject ReuseObject (GameObject prefab, Vector3 position, Quaternion rotation) {
		string poolKey = prefab.name;
        if (poolDictionary.ContainsKey (poolKey)) {
            ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue ();
            poolDictionary[poolKey].Enqueue (objectToReuse);
            objectToReuse.Reuse (position, rotation);
            return objectToReuse.transform.gameObject;
        }
        return null;
    }

    public class ObjectInstance {
        GameObject gameObject;
        public Transform transform;
        bool hasPoolObjectComponent;
        PoolObject poolObjectScript;

        public ObjectInstance (GameObject objectInstance) {
            gameObject = objectInstance;
            transform = gameObject.transform;
            gameObject.SetActive (false);
            if (gameObject.GetComponent<PoolObject> ()) {
                hasPoolObjectComponent = true;
                poolObjectScript = gameObject.GetComponent<PoolObject> ();
            }
        }

        public void Reuse (Vector3 position, Quaternion rotation) {
            gameObject.SetActive (true);
            transform.position = position;
            transform.rotation = rotation;
            if (hasPoolObjectComponent) {
                poolObjectScript.OnObjectReuse ();
            }
        }

        public void SetParent (Transform parent) {
            transform.SetParent (parent);
        }
    }
}
