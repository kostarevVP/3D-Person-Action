using WKosArch.Domain.Contexts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrapLoader : MonoBehaviour
{
    void Awake()
    {
        // Check if an object with the class ProjectContext exists in the scene
        ProjectContext projectContext = FindFirstObjectByType<ProjectContext>();

        // If it doesn't exist, load the first scene from the game's build
        if (projectContext == null)
        {
            LoadFirstScene();
        }
    }

    void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }
}
