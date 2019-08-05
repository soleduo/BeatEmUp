using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    //pindah ke spawnManager nanti
    public GameObject[] characters;
    public Transform enemyContainer;
    public Transform[] spawners;
    public int poolCount = 8;

    public Dictionary<string, List<GameObject>> pool;
    public static Dictionary<string, int> spawnCount;

    //biarin di spawner
    public int minSpawnTime = 3;
    public int maxSpawnTime = 5;
    public float intervalRandomStep = .25f;

    int maxStep;
    int typeCount;

    private void Awake()
    {
        if (pool == null)
            pool = new Dictionary<string, List<GameObject>>();

        if (spawnCount == null)
            spawnCount = new Dictionary<string, int>();

        foreach(GameObject c in characters)
        {
            for (int i = 0; i < poolCount; i++)
            {
                GameObject go = Instantiate(c, enemyContainer);
                go.SetActive(false);
                go.name = c.name;

                if (pool.ContainsKey(c.name))
                    pool[c.name].Add(go);
                else
                {
                    pool.Add(c.name, new List<GameObject>() { go });
                    spawnCount.Add(c.name, 0);
                }
            }
        }

        typeCount = characters.Length;
    }

    // Use this for initialization
    void Start () {
		maxStep = Mathf.RoundToInt((maxSpawnTime - minSpawnTime) / intervalRandomStep) + 1;
        LeanTween.delayedCall(3, () =>
        {
            Spawn();
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
    float GetRandomSpawnTime()
    {
        int r = Random.Range(0, maxStep);

        float randomTime = minSpawnTime + (r * intervalRandomStep);

        return randomTime;
    }

    LTDescr spawnerCoroutine;
    void SpawnLoop()
    {
        float time = GetRandomSpawnTime();

        spawnerCoroutine = LeanTween.delayedCall(time, () => {
            Spawn();
        });
    }

    public void CancelSpawnLoop()
    {
        LeanTween.cancel(spawnerCoroutine.id);
    }

    void Spawn()
    {        
        int r = typeCount > 1 ? Random.Range(0, typeCount) : 0;
        int r2 = Random.Range(0, 2);

        //Debug.Log(characters[r].name);
        GameObject go = GetPooledCharacter(characters[r].name);
        if (go != null)
        {
            go.SetActive(true);
            go.transform.position = spawners[r2].position;
            SessionManager.instance.OnEnemySpawned(go.GetComponent<Enemy>());
            SpawnLoop();
        }
        else
        {
            if (spawnCount[go.name] >= poolCount)
                LeanTween.delayedCall(3, () => SpawnLoop());
            else
                Spawn();
        }
    }

    public GameObject GetPooledCharacter(string name)
    {
        List<GameObject> p = pool[name];

        foreach(GameObject go in p)
        {
            if (go.activeInHierarchy)
                continue;

            spawnCount[name]++;
            return go;
        }

        return null;
    }

    public static void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
        spawnCount[go.name]--;
    }
}
