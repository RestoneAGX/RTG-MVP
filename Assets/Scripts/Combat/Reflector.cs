using UnityEngine;

public class Reflector : MonoBehaviour
{
    LayerMask player;

    public void Awake() => player = LayerMask.GetMask("Player");

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == player)
            other.transform.Rotate(Vector3.up * 180, Space.Self);
    }
}
