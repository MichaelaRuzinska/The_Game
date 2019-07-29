using UnityEngine;

namespace RPG.Core
{
    [RequireComponent(typeof(SphereCollider))]
    public class SunChanger : MonoBehaviour
    {
        [SerializeField] Color nightColor;
        [SerializeField] float changingRadius;
        [SerializeField] float nightLightIntensity;

        float dayLightIntensity;
        Color dayColor;
        GameObject sun;
        Light sunLight;
        bool isDay = true;

        // Use this for initialization
        void Start()
        {
            sun = GameObject.FindGameObjectWithTag("Sun");
            sunLight = sun.GetComponent<Light>();
            dayColor = sunLight.color;
            dayLightIntensity = sunLight.intensity;
            transform.GetComponent<SphereCollider>().radius = changingRadius;
        }

        void OnTriggerEnter(Collider collider)
        {
            var colliderTag = collider.tag;
            if (colliderTag == "Player")
            {
                dayNightChange();
            }
        }

        void OnTriggerExit(Collider collider)
        {
            var colliderTag = collider.tag;
            if (colliderTag == "Player")
            {
                dayNightChange();
            }
        }

        private void dayNightChange()
        {
            if (isDay)
            {
                sunLight.color = nightColor;
                sunLight.intensity = nightLightIntensity;
                isDay = false;
            }
            else
            {
                sunLight.color = dayColor;
                sunLight.intensity = dayLightIntensity;
                isDay = true;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 255f, 0, .5f);
            Gizmos.DrawWireSphere(transform.position, changingRadius);
        }
    }
}
