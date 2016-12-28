using UnityEngine;

public class DestroyPlayer : MonoBehaviour {
    public GameObject shield;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillerObj" || other.tag == "ActiveBlock")
        {
            if (!shield.activeSelf)
            {
                Destroy(gameObject);
            }
            else if (other.tag == "KillerObj" && other.name != "Laser")
            {
                shield.SetActive(false);
            }

            if (other.name != "Laser")
            {
                Destroy(other.gameObject);
            }
        }

    }
}
