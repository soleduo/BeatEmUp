using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public float hitPoint = 3;
    public float moveSpeed = 1.5f;
    public float moveRange = 2.5f;
    public float attackRange = 1.25f;
    public float radius = 1f;

    public AttackData[] attacks;

}
