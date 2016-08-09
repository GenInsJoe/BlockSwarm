using UnityEngine;

public class DestroyPlayer : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillerObj")
        {
            if (other.name != "Laser")
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }

    }
}
