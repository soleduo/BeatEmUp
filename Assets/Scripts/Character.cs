using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using soleduo.CharacterComponent;

public class Character : MonoBehaviour
{
    protected float health = 3;
    protected int defaulStep = 5;

    [SerializeField]
    protected CharacterData data;

    private Attack[] attacks;
    private Movement movement;
    [SerializeField]private AnimationController animator;

    protected delegate void OnDeathDelegate();
    protected OnDeathDelegate onDeath;

    protected soleduo.CharacterComponent.TargetManager targetManager;
    public soleduo.CharacterComponent.TargetManager Targetting { get { return targetManager; } }

    public GameObject mesh;
    public CharacterData Data { get { return data; } }

    public event System.Action OnComboWindowOpen;

    private void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        attacks = new Attack[data.attacks.Length];
        for (int i = 0; i < data.attacks.Length; i++)
        {
            attacks[i] = new Attack(this, data.attacks[i]);
            attacks[i].OnAttackConnects += AttackConnects;
            attacks[i].OnAttackEnds += WaitForAttackEnds;
        }

        movement = new Movement(transform, data.moveSpeed);
        targetManager = new TargetManager(this);
    }

    bool isAttacking;
    public bool IsAttacking { get { return isAttacking; } }
    int attackCount;

    public event System.Action DoAttack;

    public void AttackInit(float dir, Character target = null)
    {
        if (isAttacking || attackCount >= attacks.Length)
            return;

        isAttacking = true;
        if (isWaitingForAttackEnds != null)
        {
            Debug.Log("Attack End is in queue, cancelling." + isWaitingForAttackEnds.uniqueId);
            LeanTween.cancel(isWaitingForAttackEnds.uniqueId);
        }

        if (waitForCombo != null)
            LeanTween.cancel(waitForCombo.uniqueId);

        movement.MoveDone += () => {
            animator.SetTrigger("action" + (attackCount + 1));
            attacks[attackCount].StartAttack(dir);
        };
        Movement(dir, target, defaulStep);
    }

    LTDescr waitForCombo;
    private void AttackConnects(bool isConnected)
    {
        Debug.Log("Attack Connects " + isConnected);

        attackCount++;
        isAttacking = !isConnected;

        waitForCombo = FrameUtility.WaitForFrame(15, () => { isAttacking = true; });
    }

    LTDescr isWaitingForAttackEnds;
    private void WaitForAttackEnds(int frameCount)
    {
        isWaitingForAttackEnds = FrameUtility.WaitForFrame(frameCount, () =>
        {
            isAttacking = false;
            attackCount = 0;
            Debug.Log("Attack Ends");
            isWaitingForAttackEnds = null;
            
        });

        OnComboWindowOpen?.Invoke();
        Debug.Log("Start Waiting " + isWaitingForAttackEnds.uniqueId);
    }

    protected void Movement(float dir, Character target = null, int frameCount = -1)
    {
        Vector3 targetPosition;

        if (dir > 0)
        {
            transform.localScale = Vector3.one;
        }
        else
            transform.localScale = new Vector3(-1, 1, 1);

        if (target == null)
            targetPosition = transform.position + Vector3.right * dir * data.moveRange;
        else
            targetPosition = target.transform.position + Vector3.right * -dir * data.attackRange;

        movement.Move(targetPosition, frameCount);
    }


    public void ApplyHit(float damage, Vector2 knockForce)
    {
        health -= damage;

        if (health <= 0)
        {
            new Knockback(gameObject, knockForce + knockForce.normalized * 3f, 6);
            onDeath?.Invoke();
            return;
        }

        new Knockback(gameObject, knockForce, 6);
    }
}
