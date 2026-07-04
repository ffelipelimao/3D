using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUtils : MonoBehaviour
{
    public void LoadSceneByIndex(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void LoadScene(string i)
    {
        SceneManager.LoadScene(i);
    }
}
