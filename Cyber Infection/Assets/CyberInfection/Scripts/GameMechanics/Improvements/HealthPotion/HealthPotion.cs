using CyberInfection.GameMechanics;
using CyberInfection.GameMechanics.Unit.Player;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField]
    private int restoreHealth = 10;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<IAlive>()?.RestoreHealth(restoreHealth);
            audioSource.Play();
            Destroy(gameObject, 0.2f);
        }
    }
}
