using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagers
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
