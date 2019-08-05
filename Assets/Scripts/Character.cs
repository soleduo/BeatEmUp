using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using soleduo.CharacterComponent;

public enum CharacterState
{
    idle,
    attacking,
    hit,
    dying
}

public class Character : MonoBehaviour
{


    public float health = 3;
    public float damage = 1;
    public float knockback;

    int defaulStep = 5;
    public int step = 5;
    public float attackDelay = .01f;
    public float attackRange = 1.2f;
    public float maxAttackRange = 4f;

    public float radius = 1;

    public AttackData[] attackDataList;
    private Attack[] attacks;
    private Movement movement;
    [SerializeField]private AnimationController animator;

    protected delegate void OnDeathDelegate();
    protected OnDeathDelegate onDeath;

    protected float defaultMoveSpeed = 1.5f;
    float speed;

    protected soleduo.CharacterComponent.TargetManager targetManager;
    public soleduo.CharacterComponent.TargetManager Targetting { get { return targetManager; } }

    public GameObject mesh;

    private void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        attacks = new Attack[attackDataList.Length];
        for (int i = 0; i < attackDataList.Length; i++)
        {
            attacks[i] = new Attack(this, attackDataList[i]);
        }

        movement = new Movement(transform, defaultMoveSpeed);
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
        DoAttack += () =>
        {
            isAttacking = !attacks[attackCount].TryAttack(dir) || (attackCount >= attacks.Length);
            DoAttack = null;
        };

        movement.MoveDone += () => { animator.SetTrigger("action" + (attackCount + 1)); };
        Movement(dir, target, defaulStep);
    }

    public void StartAttack()
    {
        DoAttack?.Invoke();
        attackCount++;
    }

    public void AttackEnd()
    {
        isAttacking = false;
        attackCount = 0;
    }

    protected void SetSpeed(Character target)
    {
        speed = (target.transform.position - transform.position).magnitude / attackDelay;
    }

    protected void AttackMove(Character target)
    {
        step--;

        Vector2 dir = (target.transform.position - transform.position).normalized;

        transform.position += (Vector3)dir * speed * Time.fixedDeltaTime;
        Debug.Log("Moving to target " + speed);

        if(step == 1)
            target.ApplyHit(damage, (target.transform.position - transform.position).normalized * knockback);

        if (step > 0)
            LeanTween.delayedCall(Time.fixedDeltaTime, () => { AttackMove(target); });
        else
        {            
            step = defaulStep;
            isAttacking = false;
        }
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
            targetPosition = transform.position + Vector3.right * dir * maxAttackRange;
        else
            targetPosition = target.transform.position + Vector3.right * -dir * attackRange;

        movement.Move(targetPosition, frameCount);
    }


    public void ApplyHit(float damage, Vector2 knockForce)
    {
        health -= damage;

        if (health <= 0)
        {
            new Knockback(gameObject, knockForce * 3f, 6);
            onDeath?.Invoke();
            return;
        }

        new Knockback(gameObject, knockForce, 6);
    }

    //int knockbackDefaultStep = 4;
    //int knockbackStep = 4;
    //public void Knockback(Vector2 contact)
    //{
    //    transform.position += Vector3.right * -1 * Mathf.Sign(contact.x) * knockback / knockbackDefaultStep;
    //    knockbackStep--;
    //    Debug.Log(name + " knockback");

    //    if (knockbackStep > 0)
    //        LeanTween.delayedCall(Time.fixedDeltaTime, () => { Knockback(contact); });
    //    else
    //        knockbackStep = knockbackDefaultStep;
    //}
}
