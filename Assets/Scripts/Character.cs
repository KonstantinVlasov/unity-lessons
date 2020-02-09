using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        RunningToEnemy,
        RunningFromEnemy,
        BeginAttack,
        Attack,
        BeginShoot,
        Shoot,
        BeginFist,
        Fist,
        Death
    }

    public enum Weapon
    {
        Pistol,
        Bat,
        Fist
    }

    public float runSpeed;
    public float distanceFromEnemy;
    public Character target;
    Transform targetTransform;
    public Weapon weapon;
    Animator animator;
    Vector3 originalPosition;
    Quaternion originalRotation;
    State state = State.Idle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        targetTransform = target.transform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    [ContextMenu("Attack")]
    void AttackEnemy()
    {
        if (state == State.Death || target.state == State.Death)
        {
            Debug.Log("The character " + name + " is dead, all actions are prohibited");
            return;
        }
        switch (weapon) {
            case Weapon.Bat:
                state = State.RunningToEnemy;
                break;

            case Weapon.Pistol:
                state = State.BeginShoot;
                break;

            case Weapon.Fist:
                state = State.RunningToEnemy;
                break;
        }
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state) {
            case State.Idle:
                animator.SetFloat("speed", 0.0f);
                transform.rotation = originalRotation;
                break;

            case State.RunningToEnemy:
                animator.SetFloat("speed", runSpeed);
                if (RunTowards(targetTransform.position, distanceFromEnemy))
                {
                    switch (weapon)
                    {
                        case Weapon.Bat:
                            state = State.BeginAttack;
                            break;
                        case Weapon.Fist:
                            state = State.BeginFist;
                            break;
                    }
                }
                break;

            case State.RunningFromEnemy:
                animator.SetFloat("speed", runSpeed);
                if (RunTowards(originalPosition, 0.0f))
                    state = State.Idle;
                break;

            case State.BeginAttack:
                animator.SetFloat("speed", 0.0f);
                animator.SetTrigger("attack");
                state = State.Attack;
                break;

            case State.Attack:
                animator.SetFloat("speed", 0.0f);
                break;

            case State.BeginShoot:
                animator.SetFloat("speed", 0.0f);
                animator.SetTrigger("shoot");
                state = State.Shoot;
                break;

            case State.Shoot:
                animator.SetFloat("speed", 0.0f);
                break;

            case State.BeginFist:
                animator.SetFloat("speed", 0.0f);
                animator.SetTrigger("fist");
                state = State.Fist;
                break;

            case State.Fist:
                animator.SetFloat("speed", 0.0f);
                break;

            case State.Death:
                animator.SetFloat("speed", 0.0f);
                animator.SetTrigger("death");
                break;
        }
    }

    bool RunTowards(Vector3 targetPosition, float distanceFromTarget)
    {
        Vector3 distance = targetPosition - transform.position;
        Vector3 direction = distance.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        targetPosition -= direction * distanceFromTarget;
        distance = (targetPosition - transform.position);

        Vector3 vector = direction * runSpeed;
        if (vector.magnitude < distance.magnitude) {
            transform.position += vector;
            return false;
        }

        transform.position = targetPosition;
        return true;
    }
}
