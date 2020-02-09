using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInParent<Character>();
    }

    void AttackHit()
    {
        character.target.SetState(Character.State.Death);
    }

    void AttackEnd()
    {
        character.SetState(Character.State.RunningFromEnemy);
    }

    void ShootHit()
    {
        character.target.SetState(Character.State.Death);
    }

    void ShootEnd()
    {
        character.SetState(Character.State.Idle);
    }

    void FistHit()
    {
        character.target.SetState(Character.State.Death);
    }

    void FistEnd()
    {
        character.SetState(Character.State.RunningFromEnemy);
    }
}
