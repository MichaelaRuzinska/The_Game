using UnityEngine;

namespace RPG.Core
{
    [RequireComponent(typeof(SphereCollider))]
    public class MainMusicChanger : MonoBehaviour
    {
        [SerializeField] AudioClip musicToChangeTo;
        [SerializeField] float radius;

        AudioClip mainAudioClip;
        GameObject Music;

        void Start()
        {
            Music = GameObject.FindGameObjectWithTag("MainMusic");
            mainAudioClip = Music.GetComponent<AudioSource>().clip;
            transform.GetComponent<SphereCollider>().radius = radius;
        }

        void OnTriggerEnter(Collider collider)
        {
            changeAndPlayMusic(collider);
        }

        void changeAndPlayMusic(Collider collider)
        {
            var colliderTag = collider.tag; //getting tag of collider that interacted with this object
            var audioSource = Music.GetComponent<AudioSource>();    //getting component where main music is player
            if (colliderTag == "Player") //condition is done only when player interacts with object
            {
                audioSource.clip = musicToChangeTo; //changing main music
                audioSource.Play(); //playing new main music
            }
        }

        void OnTriggerExit(Collider collider)
        {
            var colliderTag = collider.tag;
            var audioSource = Music.GetComponent<AudioSource>();
            if (colliderTag == "Player")
            {
                audioSource.clip = mainAudioClip;
                audioSource.Play();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}