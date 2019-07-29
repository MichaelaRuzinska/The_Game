using UnityEngine;

namespace RPG.Characters
{
    public class AreaEffectBehaviour : AbilityBehaviour //child class of AbilityBehaviour
    {
        Character character;

        private void Start()
        {
            character = GetComponent<Character>();
        }
        public override void Use(GameObject target) //overriding USE method in AbilityBehaviour
        {
            PlayAbilitySound();
            DealRadialDamage();
            PlayParticleEffect();
            PlayAbilityAnimation();
        }

        private void DealRadialDamage()
        {
            //creating sphere where the damage will be taken and saving objects, which were hit in the sphere
            RaycastHit[] hits = Physics.SphereCastAll(
                            transform.position,
                            (config as AreaEffectConfig).GetRadius(),   //to know that it is using config from AreaEffectConfig script
                            Vector3.up,
                             (config as AreaEffectConfig).GetRadius()
                            );
            float inteligenceAddition = character.getInteligence() / 0.5f;
            foreach (RaycastHit hit in hits)
            {
                var damageable = hit.collider.gameObject.GetComponent<HealthSystem>();  //getting health system of hit object
                bool hitPlayer = hit.collider.gameObject.GetComponent<PlayerControl>(); // ability will not hurt the player
                if (damageable != null && !hitPlayer) 
                {
                    float damageToDeal = (config as AreaEffectConfig).GetDamageToEachTarget() + inteligenceAddition;  //getting damage to deal to characters
                    damageable.TakeDamage(damageToDeal);    //character will take damage
                }
            }
        }
    }
}
