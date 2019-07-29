using UnityEngine;

namespace RPG.Core
{
    public class SpinMe : MonoBehaviour //simple class for rotating the object in all axes
    {
        [SerializeField] float xRotationsPerMinute = 1f;
        [SerializeField] float yRotationsPerMinute = 1f;
        [SerializeField] float zRotationsPerMinute = 1f;

        void Update()
        {
            float xDegrees = Time.deltaTime / 60 * 360 * xRotationsPerMinute;
            transform.RotateAround(transform.position, transform.right, xDegrees);

            float yDegrees = Time.deltaTime / 60 * 360 * yRotationsPerMinute;
            transform.RotateAround(transform.position, transform.up, yDegrees);

            float zDegrees = Time.deltaTime / 60 * 360 * zRotationsPerMinute;
            transform.RotateAround(transform.position, transform.forward, zDegrees);
        }
    }
}
