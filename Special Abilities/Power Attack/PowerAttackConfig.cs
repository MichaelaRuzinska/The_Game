using System.Collections;
using UnityEngine;

namespace RPG.Characters
{
    [CreateAssetMenu(menuName = ("RPG/Special Ability/Power Attack"))]  //for creating more abilities of this type
    public class PowerAttackConfig : AbilityConfig
    {
        [Header("Power Attack Specific")]
        [SerializeField] float extraDamage = 10f;

        public override AbilityBehaviour getBehaviourComponent(GameObject objectToAttachTo)
        {
            return objectToAttachTo.AddComponent<PowerAttackBehaviour>();
        }

        public float GetExtraDamage()
        {
            return extraDamage;
        }
    }
}
