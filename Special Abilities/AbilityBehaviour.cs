using System.Collections;
using UnityEngine;

namespace RPG.Characters
{
    public abstract class AbilityBehaviour : MonoBehaviour  //parent class to all abilities' behaviours
    {
        protected AbilityConfig config;

        const float particle_clean_up_delay = 2f;
        const string attack_trigger = "Attack";
        const string default_attack_state = "DEFAULT ATTACK";

        public void SetConfig(AbilityConfig configToSet)
        {
            config = configToSet;
        }

        protected void PlayParticleEffect()
        {
            var particlePrefab = config.getParticlePrefab();    
            //setting particles to be on a right place with right rotation
            var particleObject = Instantiate(
                particlePrefab, 
                transform.position, 
                particlePrefab.transform.rotation
                );
            particleObject.transform.parent = transform;
            particleObject.GetComponent<ParticleSystem>().Play();   //playing
            StartCoroutine(DestroyParticleWhenFinished(particleObject));
        }

        IEnumerator DestroyParticleWhenFinished(GameObject particlePrefab)
        {
            while (particlePrefab.GetComponent<ParticleSystem>().isPlaying) //waiting while the effect is still playing
            {
                yield return new WaitForSeconds(particle_clean_up_delay);
            }
            Destroy(particlePrefab);    //destroying particles
            yield return new WaitForEndOfFrame();
        }

        protected void PlayAbilityAnimation()
        {
            var animatorOverrideController = GetComponent<Character>().getOverrideControler();
            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;    //setting override animator as animator controller
            animatorOverrideController[default_attack_state] = config.getAbilityAnimation();    //getting ability's animation to controller
            animator.SetTrigger(attack_trigger); 
        }

        protected void PlayAbilitySound()   //simple sound methon
        {
            var abilitySound = config.getRandomAbilitySound();
            var audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(abilitySound);
        }

        public abstract void Use(GameObject target = null); //it is overwriten in every ability script

    }
}
