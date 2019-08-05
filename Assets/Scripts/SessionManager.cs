using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager instance;

    [SerializeField] List<Spawner> spawners;

    private List<Character> enemyList;
    public List<Character> EnemyList { get { return enemyList; } }
    private int enemyCountLeft;
    private int enemyCountRight;

    private WaveData[] waves;
    private int waveCount;

    public delegate void EnemyCountEvent(Enemy e);
    public EnemyCountEvent OnEnemySpawned;
    public EnemyCountEvent OnEnemyDied;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;

        enemyList = new List<Character>();
    }

    void Start()
    {
        OnEnemySpawned += AddEnemy;
        OnEnemyDied += RemoveEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    private void AddEnemy(Enemy e)
    {
        enemyList.Add(e);
        e.SetActive(true);
    }

    private void RemoveEnemy(Enemy e)
    {
        enemyList.Remove(e);
        e.SetActive(false);
    }

}

public class WaveData
{
    public int enemyCount;
}
