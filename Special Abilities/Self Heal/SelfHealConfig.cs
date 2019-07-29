using UnityEngine;

namespace RPG.Characters
{
    [CreateAssetMenu(menuName = ("RPG/Special Ability/Self Heal"))] //for creating more abilities for healing
    public class SelfHealConfig : AbilityConfig
    {
        [Header("Self Heal Specific")]
        [SerializeField] float healing = 10f;

        public override AbilityBehaviour getBehaviourComponent(GameObject objectToAttachTo)
        {
            return objectToAttachTo.AddComponent<SelfHealBehaviour>();
        }

        public float GetHealing()
        {
            return healing;
        }
    }
}