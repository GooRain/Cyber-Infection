using CyberInfection.GameMechanics;
using CyberInfection.GameMechanics.Unit.Player;
using UnityEngine;
using InventorySystem;

public class HealthPotion : MonoBehaviour
{
    [SerializeField]
    private int restoreHealth = 10;
    private AudioSource audioSource;
    private ItemModel myModel;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        myModel = GetComponent<ItemModel>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<IAlive>()?.RestoreHealth(restoreHealth);
            audioSource.Play();
            Destroy(gameObject, 0.2f);
            if (Input.GetKeyDown(KeyCode.E)) myModel.PickUp();

        }
    }
}
