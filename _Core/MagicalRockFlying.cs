using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class MagicalRockFlying : MonoBehaviour
    {   
        [SerializeField] float yRotationsPerMinute = 1f;
        [SerializeField] AnimationCurve myCurve;
        Vector3 startPosition;
        void Start()
        {
            startPosition = transform.position;
        }
        void LateUpdate()
        {
            float yDegreesPerFrame = Time.deltaTime / 60 * 360 * yRotationsPerMinute;
            transform.RotateAround(transform.position, transform.up, yDegreesPerFrame);
            transform.position = new Vector3(startPosition.x, startPosition.y + myCurve.Evaluate((Time.time % myCurve.length)), startPosition.z);
        }
    }
}
