using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    Character player;
    BlinkObject blink;

    public delegate void SpawnEvent();

    private void Awake()
    {
        if (blink == null)
            blink = mesh.GetComponent<BlinkObject>();
    }

    // Use this for initialization
    void Start () {
        player = GameManager.GetPlayer();

        onDeath += Death;

        Initialize();
	}

    private void OnEnable()
    {
        health = data.hitPoint;
    }
    float remainingDistance;
	// Update is called once per frame
	void Update () {
        remainingDistance = CollisionCheck.GetDistance(transform.position, player.transform.position);

        if(remainingDistance < data.attackRange)
        {
            //AttackInit(player);
            return;
        }

        //jalan
        Movement(CollisionCheck.GetDirection(transform.position, player.transform.position).x);

        //if(remainingDistance > maxAttackRange)
        //{
        //    //matiin movdir y;
        //}
	}

    public void Death()
    {
        //SessionManager.instance.OnEnemyDied(this);
        SessionManager.instance.OnEnemyDied(this);

        LeanTween.delayedCall(2f, () =>
        {
            Spawner.ReturnToPool(gameObject);
        });
    }

    public void SetActive(bool isActive)
    {
        if (enabled == isActive)
            return;

        enabled = isActive;
        blink?.SetActive(!isActive);
    }

}
