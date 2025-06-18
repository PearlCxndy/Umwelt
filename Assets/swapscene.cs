using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LoadCubeScene();
        }
    }

    void LoadCubeScene()
    {
        SceneManager.LoadScene(0); // Loads scene at index 0
        // Alternatively: SceneManager.LoadScene("Cube");
    }
}
