using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEventHandler : MonoBehaviour
{
    public Character character;

    public void StartAttack()
    {
        character.StartAttack();
    }

    public void AttackEnd()
    {
        character.AttackEnd();
    }
}
