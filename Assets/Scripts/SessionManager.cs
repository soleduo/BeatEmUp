using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager instance;

    [SerializeField] List<Spawner> spawners;

    private Dictionary<Enemy, int> enemyList;
    public Dictionary<Enemy, int> EnemyList { get { return enemyList; } }
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

        enemyList = new Dictionary<Enemy, int>();
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
        if (e.transform.position.x > 0)
            enemyCountRight++;
        else
            enemyCountLeft++;

        enemyList.Add(e, (int)Mathf.Sign(e.transform.position.x));
        e.SetActive(true);
    }

    private void RemoveEnemy(Enemy e)
    {
        if (enemyList[e] > 0)
            enemyCountRight--;
        else
            enemyCountLeft--;

        enemyList.Remove(e);
        e.SetActive(false);
    }

}

public class WaveData
{
    public int enemyCount;
}
