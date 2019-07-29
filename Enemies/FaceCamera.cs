using UnityEngine;

namespace RPG.Characters
{
    public class FaceCamera : MonoBehaviour //class for health bar to face camera
    {
        Camera cameraToLookAt;

        void Start()
        {
            cameraToLookAt = Camera.main;
        }

        void LateUpdate()
        {
            transform.LookAt(cameraToLookAt.transform);
        }
    }
}