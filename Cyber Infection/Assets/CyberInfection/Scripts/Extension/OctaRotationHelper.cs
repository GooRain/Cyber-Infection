using System;
using UnityEngine;

namespace CyberInfection.Extension
{
    [Flags]
    public enum OctaDirection : sbyte
    {
        Right = 0,
        RightUp = 1,
        Up = 2,
        LeftUp = 4,
        Left = 8,
        LeftDown = 16,
        Down = 32,
        RightDown = 64
    }
    
    public static class OctaRotationHelper
    {
        public static float GetAngle(Vector3 from, Vector3 to)
        {
            var diff = to - from;
            var angleBetween = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            
            angleBetween %= 360;
            if (angleBetween < 0)
            {
                angleBetween += 360;
            }

            return angleBetween;
        }
        /// <summary>
        /// Rotating 8 Ways and returning frame index
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static int RotateFrame(Vector3 from, Vector3 to)
        {
            var diff = to - from;
            var angleBetween = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
 
            angleBetween += 22.5f;
 
            angleBetween %= 360;
            if (angleBetween < 0)
            {
                angleBetween += 360;
            }
 
            return (int) angleBetween / 45;
        }
    }
}