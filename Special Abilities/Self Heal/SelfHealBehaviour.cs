using UnityEngine;

namespace RPG.Characters
{
    public class SelfHealBehaviour : AbilityBehaviour   //child class of abilitybehaviour
    {
        PlayerControl player = null;
        Character character;

        void Start()
        {
            player = GetComponent<PlayerControl>();
            character = GetComponent<Character>();
        }

        public override void Use(GameObject target) //overwriting parent method
        {
            PlayAbilitySound();
            Healing();
            PlayParticleEffect();
            PlayAbilityAnimation();
        }

        private void Healing()
        {
            float inteligenceAddition = character.getInteligence() / 0.5f;
            var playerHealth = player.GetComponent<HealthSystem>();
            playerHealth.Heal((config as SelfHealConfig).GetHealing() + character.getInteligence());
        }
    }
}
