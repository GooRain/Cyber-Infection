using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberInfection.UI.Radar
{
    public class RadarCamera : MonoBehaviour
    {
        public static RadarCamera instance { get; private set; }
        private void Awake()
        {
            instance = this;
        }
        public void LookMinichar(GameObject minichar)
        {
            transform.position = minichar.transform.position;
        }
    }
}
