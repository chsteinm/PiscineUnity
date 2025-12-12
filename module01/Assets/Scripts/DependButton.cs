using UnityEngine;

public class DependButton : MonoBehaviour
{
    private GameObject door;

    // Open the door dependent of which player is interacting with it
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            string playerName = player.name;
            door = GetDoorForPlayer(playerName);
            if (door != null)
            {
                door.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            string playerName = player.name;
            if (door != null)
            {
                door.SetActive(true);
            }
        }
    }

    private GameObject GetDoorForPlayer(string playerName)
    {
        // Use a naming convention to find the corresponding door
        string doorName = playerName + "Door";
        GameObject door = GameObject.Find(doorName);
        return door;
    }

}
