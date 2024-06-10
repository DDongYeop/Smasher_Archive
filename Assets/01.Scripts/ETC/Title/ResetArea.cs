using UnityEngine;

public class ResetArea : MonoBehaviour
{
    private ShuttleCock _shuttleCock;

    private void Awake() 
    {
        _shuttleCock = FindObjectOfType<ShuttleCock>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
            _shuttleCock.SetStart(-1);
    }
}
