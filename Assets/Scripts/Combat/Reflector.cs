using UnityEngine;

public class Reflector : MonoBehaviour
{
    LayerMask player;

    public Transform parent;

    public void Awake() {
        player = LayerMask.GetMask("Player");
        parent = transform.parent.parent;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == player){
            other.transform.Rotate(Vector3.up * 180, Space.Self);
            other.GetComponent<Projectile>().box.parent = parent;
        }
    }
}
