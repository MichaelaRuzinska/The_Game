using UnityEngine;

namespace RPG.Core
{
    [RequireComponent(typeof(SphereCollider))]
    public class AudioTrigger : MonoBehaviour
    {
        [Header("Sound options")]
        [SerializeField] AudioClip clip;
        [SerializeField] [Range(0.01f, 1f)] float volume;
        [SerializeField] [Range(0.01f, 256f)] int priority;

        [Header("Configuration")]
        [SerializeField] bool isNPC = false;
        [SerializeField] float radius = 2f;
        [SerializeField] bool isOneTimeOnly = true;

        bool hasPlayed = false;
        AudioSource audioSource;

        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = clip;
            transform.GetComponent<SphereCollider>().radius = radius;
        }

        void OnTriggerEnter(Collider collider)
        {
            var colliderTag = collider.tag;
            if (colliderTag == "Player")
            {
                RequestPlayAudioClip();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (!isNPC)
            {
                audioSource.Stop();
            }
            else { }
        }

        void RequestPlayAudioClip()
        {
            if (isOneTimeOnly && hasPlayed)
            {
                return;
            }
            else if (audioSource.isPlaying == false)
            {
                audioSource.priority = priority;
                audioSource.volume = volume;
                audioSource.Play();
                hasPlayed = true;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 255f, 0, .5f);
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}