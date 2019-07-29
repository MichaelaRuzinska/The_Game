using System.Collections;
using UnityEngine;
using RPG.CameraUI;

namespace RPG.Characters
{
    public class PlayerControl : MonoBehaviour
    {
        Character character;
        SpecialAbilities abilities;
        WeaponSystem weaponSystem;
        EnemyAI companionTarget;
        public bool PlayerIsAttacking { get; set; }

        void Start()
        {
            character = GetComponent<Character>();
            abilities = GetComponent<SpecialAbilities>();
            weaponSystem = GetComponent<WeaponSystem>();

            RegisterForMouseEvents();
        }

        void Update()
        {
            var healthPercentage = GetComponent<HealthSystem>().healthAsPercentage;
            if(healthPercentage >= Mathf.Epsilon)   //if player uses special ability
            {
                ScanForAbilityKeyDown();
            }
        }
        
        private void RegisterForMouseEvents()   //adding delegates
        {
            var cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
            cameraRaycaster.onMouseOverWalkable += OnMouseOverWalkable;
        }
        
        void OnMouseOverWalkable(Vector3 destination)
        {
            if(Input.GetMouseButton(0))
            {
                weaponSystem.StopAttacking();
                character.setDestination(destination);
                PlayerIsAttacking = false;
            }
        }
        
        private void ScanForAbilityKeyDown()
        {
            for (int keyIndex = 1; keyIndex < abilities.GetNumberOfAbilities(); keyIndex++)
            {
                if(Input.GetKeyDown(keyIndex.ToString()))   //if player hit 1-9 key, ability will be used
                {
                    abilities.AttemptSpecialAbility(keyIndex);
                }
            }
        }

        public bool IsTargetInRange(EnemyAI target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            //returns true if distance to target is smaller than weapon's attack range
            return distanceToTarget <= weaponSystem.GetCurrentWeaponConfig().GetMaxAttackRange();  
        }

        void OnMouseOverEnemy(EnemyAI enemy)    //what will happen, when player click on enemy
        {
            if(Input.GetMouseButton(0) && IsTargetInRange(enemy))
            //left mouse button is pushed and enemy is in the range for player to attack
            {
                PlayerIsAttacking = true;
                companionTarget = enemy;
                weaponSystem.AttackTarget(enemy.gameObject);
            }
            else if(Input.GetMouseButton(0) && !IsTargetInRange(enemy))
            //left mouse button is pushed, but enemy is not in the range for player to attack
            {
                PlayerIsAttacking = true;
                StartCoroutine(MoveAndAttack(enemy));
            }
            else if(Input.GetMouseButtonDown(1) && IsTargetInRange(enemy))
            //right mouse button is pushed and enemy is in the range for player to use power attack
            {
                abilities.AttemptSpecialAbility(0, enemy.gameObject);
            }
            else if (Input.GetMouseButtonDown(1) && !IsTargetInRange(enemy))
            //right mouse button is pushed, but enemy is not in the range for player to use power attack
            {
                StartCoroutine(MoveAndPowerAttack(enemy));
            }
        }

        IEnumerator moveToTarget(EnemyAI target)
        {
            character.setDestination(target.transform.position);    //setting destination to move
            while (!IsTargetInRange(target))
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }

        IEnumerator MoveAndAttack(EnemyAI target)
        {
            yield return StartCoroutine(moveToTarget(target));  //starts coroutine to move
            weaponSystem.AttackTarget(target.gameObject);   //attacking enemy
        }

        IEnumerator MoveAndPowerAttack(EnemyAI target)
        {
            yield return StartCoroutine(moveToTarget(target));  //moving to enemy
            abilities.AttemptSpecialAbility(0, target.gameObject);  //trying to use ability set on right mouse click
        }

        public EnemyAI getCompanionTarget()
        {
            return companionTarget;
        }

        public bool isPlayerAttacking()
        {
            return PlayerIsAttacking;
        }
    }
}
