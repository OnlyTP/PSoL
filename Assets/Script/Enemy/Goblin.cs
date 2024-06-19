using UnityEngine;

public class Goblin : MonoBehaviour
{
    public float timeBetweenHits;
    public float movementSpeed;
    public float chaseRadius;
    public float attackRadius;
    public int damage;

    private GameObject player;
    private Animator anim;
    private bool readyToAttack = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        if (player == null)
            Debug.Log("Player Doesn't Have Tag");


    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= chaseRadius)
            {
                // Move towards the player if within chase radius
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
                anim.SetBool("isRunning", true); // Assuming you have a running animation
                anim.SetBool("isIdle", false);
                FlipSprite(); // Adjust sprite orientation based on player position
            }
            else
            {
                // Idle if the player is outside the chase radius
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }

            // Attack if within attack radius and ready
            if (distanceToPlayer <= attackRadius && readyToAttack)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        anim.SetBool("isAttacking", true); // Trigger attack animation
        player.GetComponent<PlayerStats>().TakeDamage(damage);
        readyToAttack = false;
        Invoke("ResetAttack", timeBetweenHits);
    }

    private void ResetAttack()
    {
        anim.SetBool("isAttacking", false); // Reset attack animation
        readyToAttack = true;
    }

    private void FlipSprite()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Flip sprite to face left
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Normal orientation (facing right)
        }
    }
}
