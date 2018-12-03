using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameMechanic.CameraMechanic
{
    public class CameraController : MonoBehaviour
    {
        [FormerlySerializedAs("dampTime")] [SerializeField]
        private float _dampTime = 0.2f;
        private Vector3 _velocity = Vector3.zero;
        private float _cameraZ = 0;
        private Transform _player;
        private Camera _camera;

        // что то типа такого пока что, пока других размеров комнат нет.
        private Vector2 _maxXAndY = new Vector2(1.15f,1.18f); 
        private Vector2 _minXAndY = new Vector2(-0.4f,-0.15f); 

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _cameraZ = transform.position.z;
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (_player)
            {
                Vector3 delta = _player.transform.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _cameraZ));
                Vector3 destination = transform.position + delta;
                destination.z = _cameraZ;
                destination.x = Mathf.Clamp(delta.x, _minXAndY.x, _maxXAndY.x);
                destination.y = Mathf.Clamp(delta.y, _minXAndY.y, _maxXAndY.y);
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _dampTime);
            }
        }
    }
}
