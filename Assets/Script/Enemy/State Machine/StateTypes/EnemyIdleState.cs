using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector2 _targetPos;
    private Vector2 _direction;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimatinTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimatinTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        SetNewTargetPosition();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private void SetNewTargetPosition()
    {
        _targetPos = GetRandomPointInCircle();
        Vector2 enemyPosition2D = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
        _direction = (_targetPos - enemyPosition2D).normalized;
    }

    public override void FrameUpdate()
    {

        Vector2 enemyPosition2D = new Vector2(enemy.transform.position.x, enemy.transform.position.y);

        if (enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
        // Move the enemy towards the target position each frame
        enemy.MoveEnemy(_direction * enemy.RandomMovementSpeed);

        // Check if the enemy has reached or is close to the target position
        if ((enemyPosition2D - _targetPos).sqrMagnitude < 0.1f)  // Consider a slightly larger threshold
        {
            SetNewTargetPosition();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private Vector2 GetRandomPointInCircle()
    {
        // Using Random.insideUnitCircle to get a point within a circle in 2D
        return (Vector2)enemy.transform.position + UnityEngine.Random.insideUnitCircle * enemy.RandomMovementRange;
    }
}
