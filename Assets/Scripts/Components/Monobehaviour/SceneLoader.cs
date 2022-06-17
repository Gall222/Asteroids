using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ToStartScene()
    {
        SceneManager.LoadScene(0);
    }
}
