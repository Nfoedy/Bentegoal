using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PostSound : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Ball")) return;

        audioSource.PlayOneShot(hitSound);
    }

}
