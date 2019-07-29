using System.Collections;
using UnityEngine;

namespace RPG.Characters
{
    [RequireComponent(typeof(Character))]
    public class Companion : MonoBehaviour
    {
        [SerializeField] float attackDamage = 1f;
        [SerializeField] float attackRadius = 3f;
        [SerializeField] float timeBetweenAttack = 3f;
        
        PlayerControl player;
        Character companion;
        EnemyAI enemy;
        float followingRadius = 1000f;
        float distanceToPlayer;
        enum State { Following, Attacking, Wait }
        State state = State.Wait;

        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<PlayerControl>();
            companion = GetComponent<Character>();
        }

        // Update is called once per frame
        void Update()
        {
            var isPlayerAttacking = player.isPlayerAttacking();
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer <= followingRadius && state != State.Following)
            {
                StartCoroutine(FollowingPlayer());
            }
            if (distanceToPlayer <= followingRadius && isPlayerAttacking == true && state != State.Attacking)
            {
                state = State.Attacking;
                enemy = player.getCompanionTarget();    //finding enemy
                StartCoroutine(AttackingTarget(enemy));
            }
        }

        IEnumerator FollowingPlayer()
        {
            companion.setDestination(player.transform.position);
            yield return new WaitForEndOfFrame();
        }

        IEnumerator AttackingTarget(EnemyAI enemy)
        {
            print("here");
            var distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if (distanceToEnemy <= attackRadius)
            {
                var enemyHealthSystem = enemy.GetComponent<HealthSystem>();
                enemyHealthSystem.TakeDamage(randomDamage());
            }
            else if (distanceToEnemy > attackRadius)
            {
                companion.setDestination(enemy.transform.position);
            }
            yield return timeBetweenAttack;
        }

        float randomDamage()
        {
            return Random.Range(attackDamage/2, attackDamage);
        }
    }
}
