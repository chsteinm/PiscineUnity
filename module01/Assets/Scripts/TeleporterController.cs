using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    // Reference to the destination teleport location
    public Transform teleportDestination;

    // Teleport the player to the destination when they enter the trigger
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = teleportDestination.position;
    }
}
