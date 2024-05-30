using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform _playerTransform;

    private float _timer;
    private float _timeBetweenShots = 3f;

    private float _exitTimer;
    private float _timeTillExit = 3f;
    private float _distanceToCountExit = 3f;

    private float _arrowspeed = 50f;
    public EnemyAttackState( Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }
    
    public override void AnimatinTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimatinTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;  // Ensure player is tagged correctly in the Editor

        if (_playerTransform == null)
        {
            Debug.LogError("Player transform is not assigned.");
        }
    }


    public override void ExitState()
    {
        base.ExitState();
    }

    private float _distanceCheckTimer = 0f;
    private float _distanceCheckInterval = 0.5f; // Check every 0.5 seconds

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.MoveEnemy(Vector2.zero);

        float distanceToPlayer = Vector2.Distance(_playerTransform.position, enemy.transform.position);

        // Check if within shooting interval and striking distance
        _timer += Time.deltaTime;
        if (_timer > _timeBetweenShots && distanceToPlayer <= enemy.StrikingDistance)
        {
            _timer = 0f;
            ShootArrow();
        }

        // Delayed distance checking for state transition
        _distanceCheckTimer += Time.deltaTime;
        if (_distanceCheckTimer >= _distanceCheckInterval)
        {
            _distanceCheckTimer = 0f; // Reset timer
            CheckDistanceAndChangeState();
        }
    }

    private void ShootArrow()
    {
        Vector2 dir = (_playerTransform.position - enemy.transform.position).normalized;
        Rigidbody2D arrow = GameObject.Instantiate(enemy.ArrowPrefab, enemy.transform.position, Quaternion.identity);
        arrow.velocity = dir * _arrowspeed;
        Debug.Log("Arrow fired at speed: " + _arrowspeed);
    }

    private void CheckDistanceAndChangeState()
    {
        if (_playerTransform == null || enemy == null)
        {
            Debug.LogError("One or more required components are not assigned.");
            return; // Exit the method if there is no player or enemy assigned
        }

        if (Vector2.Distance(_playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeTillExit)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }
        else
        {
            _exitTimer = 0f;
        }
    }



    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

  
}
