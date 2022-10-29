using UnityEngine;

public class Reflector : MonoBehaviour
{
    public Transform parent;

    public void Awake() {
        parent = transform.parent.parent;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && other.transform != parent){
            Debug.Log(other.name);
            other.transform.Rotate(Vector3.up * 180, Space.Self);
            other.GetComponent<Projectile>().box.parent = parent;
        }
    }
}
