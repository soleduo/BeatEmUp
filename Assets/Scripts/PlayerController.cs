using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using soleduo.CharacterComponent;

public class PlayerController : MonoBehaviour {

    public Character player;
    Character target = null;
    float move = 0;

    Stack<int> inputStack = new Stack<int>(3);
    LTDescr consumeInput;

    // Use this for initialization
    void Start () {
        target = null;
        move = 0;
        player.OnComboWindowOpen += () => TryConsumeAttackInput();
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    target = GetNearestTarget();
        //}

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //target = GetNearestTargetDirected(-1);
            //move = -1;
            AttackInput(-1);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            //target = GetNearestTargetDirected(1);
            //move = 1;
            AttackInput(1);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            move = 0;
    }

    private bool TryConsumeAttackInput()
    {
        if (inputStack.Count <= 0)
            return false;

        if (player.IsAttacking)
        {
            if(consumeInput != null)
                LeanTween.cancel(consumeInput.uniqueId);

            consumeInput = LeanTween.delayedCall(6 * Time.fixedDeltaTime, () => { inputStack.Clear(); consumeInput = null; });
            return false;
        }

        int i = inputStack.Pop();
        Character target = GetNearestTargetDirected(i, SessionManager.instance.EnemyList);
        player.AttackInit(i, target);

        consumeInput = null;
        return true;
    }

    private void AttackInput(int dir)
    {
        inputStack.Push(dir);
        if (consumeInput == null)
            TryConsumeAttackInput();
    }

    protected Character GetNearestTarget()
    {
        foreach (Character c in FindObjectsOfType<Character>())
        {
            if (c == player)
                continue;
            if (!CollisionCheck.CheckCollision(player.transform.position, c.transform.position, player.Data.moveRange, c.Data.radius).isCollide)
                continue;

            //Debug.Log(c.name + " is found");
            return c;
        }

        return null;
    }

    protected Character GetNearestTargetDirected(float dir, List<Character> targets)
    {
        Character nearest = null;
        float minDistance = 9999;

        foreach (Character c in targets)
        {
            if (!c.gameObject.activeInHierarchy)
                continue;
            if (c == player)
                continue;
            if ((c.transform.position.x-player.transform.position.x) * dir < 0)
                continue;
            if (!CollisionCheck.CheckCollision(player.transform.position, c.transform.position, player.Data.moveRange + player.Data.attackRange, c.Data.radius).isCollide)
                continue;

            float d = CollisionCheck.GetDistance(player.transform.position, c.transform.position);
            Debug.Log("d " + d);
            //Debug.Log(c.name + " is found");
            if(d < minDistance)
                nearest = c;
        }

        return nearest;
    }

    private void OnDrawGizmos()
    {
        if (GetNearestTarget() == null)
            Gizmos.color = Color.white * new Color(1, 1, 1, 0.3f);
        else
            Gizmos.color = Color.red * new Color(1, 1, 1, 0.3f);

        Gizmos.DrawCube(player.transform.position, new Vector2(player.Data.moveRange * 2 + 1, 5f));
    }
}
