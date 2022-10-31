using UnityEngine;

public class HermitVine : MonoBehaviour
{
    LayerMask player;
    private void Awake() => player = LayerMask.GetMask("Player");

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == player){} // Pull them towards us
        else{} // Move towards    
    }
}
