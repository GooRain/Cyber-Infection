using CyberInfection.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberInfection.SceneManagers
{
    public class LoadSceneManager : MonoBehaviour
    {
        private void Start()
        {
            LoadMenu();
        }

        private void LoadMenu()
        {
            SceneManager.LoadSceneAsync(SceneName.Menu);
        }
    }
}
