using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    [SerializeField] private float hitPoint = 3;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float radius = 1f;

    [SerializeField] private AttackData[] attacks;

    public float HitPoint { get { return hitPoint; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public AttackData[] AttackDataList { get { return attacks; } }

    public CharacterData(float hitPoint, float moveSpeed, AttackData[] attacks)
    {
        this.hitPoint = hitPoint;
        this.moveSpeed = moveSpeed;

        this.attacks = attacks;
    }
}
