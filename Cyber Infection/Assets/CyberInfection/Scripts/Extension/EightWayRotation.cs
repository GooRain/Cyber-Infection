using UnityEngine;

namespace CyberInfection.Extension
{
    public static class EightWayRotation
    {
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