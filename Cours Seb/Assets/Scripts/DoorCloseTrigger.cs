using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    [Header("Portes à fermer")]
    public Door[] doorsToClose; // ← assigne les portes à fermer

    private bool _triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (_triggered) return; // ← ne se déclenche qu'une seule fois

        foreach (Door door in doorsToClose)
            door.Close();

        _triggered = true;
    }
}