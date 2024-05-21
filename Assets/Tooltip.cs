using UnityEngine;
using TMPro;  // Include this for TextMeshPro
using System.Collections;

public class InteractiveObject : MonoBehaviour
{
    public TextMeshProUGUI messageText;  // Assign this in the Inspector
    private float delay = 3.0f;  // Time in seconds before message disappears after touch ends

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is the player or something you can interact with
        if (other.CompareTag("Player"))  // Make sure the player tag is set appropriately
        {
            messageText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(HideMessageAfterDelay());
        }
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        messageText.gameObject.SetActive(false);
    }
}
