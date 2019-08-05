using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackData", order = 1)]
public class AttackData : ScriptableObject
{
    public float damage;
    public float knockback;
    public Vector2 hitbox;

    public AttackAnimationFrameData frameData;
}

[System.Serializable]
public class AttackAnimationFrameData
{
    public int anticipation;
    public int recovery;
}
