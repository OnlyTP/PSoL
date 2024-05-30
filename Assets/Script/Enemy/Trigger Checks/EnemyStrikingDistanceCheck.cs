using UnityEngine;

public class EnemyStrikingDistanceCheck : MonoBehaviour
{
    private Enemy _enemy;  

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
        if (_enemy == null)
        {
            Debug.LogError("Enemy component not found on parent GameObjects");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered striking distance.");
            _enemy.SetStrikingDistanceBool(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has exited striking distance.");
            _enemy.SetStrikingDistanceBool(false);
        }
    }
}
