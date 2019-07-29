using UnityEngine;

namespace RPG.Characters
{
    public abstract class AbilityConfig : ScriptableObject  //parent class for all abilities' configs
    {
        [Header("Special Ability General")]
        [SerializeField] float energyCost = 10f;
        [SerializeField] GameObject particlePrefab = null;
        [SerializeField] AnimationClip abilityAnimation;
        [SerializeField] AudioClip[] audioClips = null;

        protected AbilityBehaviour behaviour = null;

        public abstract AbilityBehaviour getBehaviourComponent(GameObject objectToAttachTo);

        public void AttachAbilityTo(GameObject objectToAttachTo)
        {
            AbilityBehaviour behaviourComponent = getBehaviourComponent(objectToAttachTo);
            behaviourComponent.SetConfig(this);
            behaviour = behaviourComponent;
        }

        public void Use(GameObject target)
        {
            behaviour.Use(target);
        }


        //simple getters
        public float GetEnergyCost()
        {
            return energyCost;
        }

        public GameObject getParticlePrefab()
        {
            return particlePrefab;
        }

        public AudioClip getRandomAbilitySound()
        {
            return audioClips[Random.Range(0, audioClips.Length)];
        }

        public AnimationClip getAbilityAnimation()
        {
            return abilityAnimation;
        }
    }
}
