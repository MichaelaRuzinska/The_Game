using UnityEngine;

namespace RPG.CameraUI
{
    public class CameraFollow : MonoBehaviour
    {
        GameObject player;

        [SerializeField] float rotation_value = -3f; //how fast is camera rotating
       // private float x;
        private float y;
        private Vector3 rotateValue;
        
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");  
        }

        void Update()
        {
            transform.position = player.transform.position;
            //camera follows player’s position
            if (Input.GetMouseButton(1)) //if player clicks right mouse button
            {
                y = Input.GetAxis("Mouse X"); //how much should camera rotate  
                rotateValue = new Vector3(0, y * rotation_value, 0);
                //calculating rotation value 
                transform.eulerAngles = (transform.eulerAngles - rotateValue);
                //rotating camera in 3D space through Euler Angles
            }
        }

    }
}

