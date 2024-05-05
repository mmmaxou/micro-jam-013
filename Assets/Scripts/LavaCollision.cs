using UnityEngine;

public class LavaCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.IndexOf("House") > -1)
        {
            SceneManager.Instance.LaunchGameOver();
        }
        if (collision.gameObject.name.IndexOf("Forge") > -1)
        {
            SceneManager.Instance.LaunchGameEnding();
        }
    }
}
