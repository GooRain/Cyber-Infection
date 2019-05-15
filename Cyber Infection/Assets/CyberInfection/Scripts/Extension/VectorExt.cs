using UnityEngine;

namespace CyberInfection.Extension
{
    public static class VectorExt
    {
        public static Vector2 OnlyXZ(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }
        
        public static Vector2 OnlyXY(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static Quaternion DirectionToRotation(this Vector2 dir)
        {
            return Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, Vector3.forward);
        }
    }
}