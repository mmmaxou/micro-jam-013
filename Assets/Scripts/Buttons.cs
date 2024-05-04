using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void LaunchWipLevel()
    {
        SceneManager.Instance.LaunchWipLevel();
    }
    public void LaunchLevel01()
    {
        SceneManager.Instance.LaunchLevel01();
    }
    public void LaunchLevel02()
    {
        SceneManager.Instance.LaunchLevel02();
    }
    public void LaunchMenu()
    {
        SceneManager.Instance.LaunchMenu();
    }
}
