using UnityEngine;

namespace CyberInfection.UI.MainMenu
{
    public class GearsRotation : MonoBehaviour
    {
        [SerializeField]
        private float _rotationalSpeed;

        private void Update()
        {
            transform.Rotate(0, 0, _rotationalSpeed * Time.deltaTime);
        }
    }
}
