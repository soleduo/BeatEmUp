using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState
{
    public event System.Action OnStateEnter;
    public event System.Action OnStateEnd;

    protected Character character;

    public CharacterState(Character owner)
    {
        character = owner;
    }

    public abstract void Update();
}
