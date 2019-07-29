using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    public class PowerAttackBehaviour : AbilityBehaviour    //child class of abilityBehaviour
    {

        public override void Use(GameObject target) //overwriting parent method
        {
            PlayAbilitySound();
            DealDamage(target);
            PlayParticleEffect();
            PlayAbilityAnimation();
        }

        public void DealDamage(GameObject target)
        {
            transform.LookAt(target.transform);
            float damageToDeal = (config as PowerAttackConfig).GetExtraDamage();    //dealing damage
            target.GetComponent<HealthSystem>().TakeDamage(damageToDeal);   //lowering health
        }
    }
}
