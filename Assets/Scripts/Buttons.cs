using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void LaunchMain()
    {
        SceneManager.Instance.LaunchMain();
    }
    public void LaunchMenu()
    {
        SceneManager.Instance.LaunchMenu();
    }
}
