using System.Collections;
using UnityEngine;

namespace RPG.Characters
{
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(WeaponSystem))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] float chaseRadius = 6f;
        [SerializeField] WaypointContainer patrolPath;
        [SerializeField] float waypointTolerance = 2f;
        [SerializeField] float timeToWait = 5f;

        Character character;
        float currentWeaponRange;
        PlayerControl player = null;
        float distanceToPlayer;
        int nextWaypointIndex;


        enum State { attacking, idle, patrolling, chasing}  //declaring states that enemy can be in
        State state = State.idle;   //default state

        void Start()
        {
            player = FindObjectOfType<PlayerControl>();
            character = GetComponent<Character>();
        }
        
        void Update()
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position); //distance from enemy to player
            WeaponSystem weaponSystem = GetComponent<WeaponSystem>();
            currentWeaponRange = weaponSystem.GetCurrentWeaponConfig().GetMaxAttackRange(); //weapon range 

            bool inWeaponRadius = distanceToPlayer <= currentWeaponRange;
            bool inChaseRadius = distanceToPlayer > currentWeaponRange && distanceToPlayer <= chaseRadius;
            bool outsideChaseRadius = distanceToPlayer > chaseRadius;

            if (outsideChaseRadius) //patrolling state
            {
                StopAllCoroutines();
                weaponSystem.StopAttacking();
                StartCoroutine(Patrol());
            }
            if(inChaseRadius)   //chasing state
            {
                StopAllCoroutines();
                weaponSystem.StopAttacking();
                StartCoroutine(ChasePlayer());
            }
            if(inWeaponRadius)   //attacking state
            {
                StopAllCoroutines();
                state = State.attacking;
                weaponSystem.AttackTarget(player.gameObject);   //attacking
            }
        }

        IEnumerator Patrol()
        {
            state = State.patrolling;
            while (patrolPath != null)  //moving according to waypoints
            {
                Vector3 nextWaypoint = patrolPath.transform.GetChild(nextWaypointIndex).position;   
                character.setDestination(nextWaypoint);
                CycleWaypointWhenClose(nextWaypoint);
                yield return new WaitForSecondsRealtime(timeToWait);
            }
        }

        private void CycleWaypointWhenClose(Vector3 nextWaypoint)  
        {
            if (Vector3.Distance(transform.position, nextWaypoint) < waypointTolerance)
            {
                nextWaypointIndex = (nextWaypointIndex + 1) % patrolPath.transform.childCount;
            }
        }

        IEnumerator ChasePlayer()
        {
            state = State.chasing;
            while(distanceToPlayer >= currentWeaponRange)   //chasing when the distance to player is bigger then enemy's weapon range
            {
                character.setDestination(player.transform.position);
                yield return new WaitForEndOfFrame();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, currentWeaponRange);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
        }
    }
}