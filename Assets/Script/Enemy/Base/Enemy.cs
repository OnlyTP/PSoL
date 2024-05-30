using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool IsFacingRight { get; set; } = false;


    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }

    public Rigidbody2D ArrowPrefab;
    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1f;
    public float StrikingDistance = 5f;



    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;

        RB = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);

    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    // State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }

    public EnemyIdleState IdleState { get; set; }

    public EnemyChaseState ChaseState { get; set; }

    public EnemyAttackState AttackState { get; set; }


    


    // Health / Die Functions
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    // Movement Functions
    public void MoveEnemy(Vector2 velocity)
    {
        RB.velocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if ((IsFacingRight && velocity.x < 0f) || (!IsFacingRight && velocity.x > 0f))
        {
            // Flip the sprite by multiplying the x component of the local scale by -1
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            IsFacingRight = !IsFacingRight;
        }
    }


    // Distance Check 
    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }




    // TODO: Animation Trigger 

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        //StateMachine.CurrentEnemyState.AnimatinTriggerEvent(triggerType);
    }

    
    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound

    }    

}
