using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : BaseStateManager
{
    public Rigidbody2D rb;
    public Animator animator;

    [HideInInspector]
    public Transform target;

    public BaseState EnemyGuardState;
    public BaseState EnemyChargeState;

    void Awake()
    {
        EnemyGuardState = new EnemyGuardState(this);
        EnemyChargeState = new EnemyChargeState(this);
    }

    public override void Start()
    {
        base.Start();
    }

    public override BaseState GetInitialState()
    {
        return EnemyGuardState;
    }

}