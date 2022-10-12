using UnityEngine;

public class Reflector : MonoBehaviour
{
    LayerMask player;

    public void Awake() => player = LayerMask.GetMask("Player");

    private void OnTriggerEnter(Collider other) {
        if (other.layer == player)
            other.GetComponent<Rigidbody>().AddForce(forceDir, ForceMode.Impulse);
    }
}
