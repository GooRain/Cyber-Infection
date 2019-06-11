using System;
using UnityEngine;

namespace CyberInfection.Generation.Tiles
{
    public enum DoorDir
    {
        Right = 0,
        Top = 1,
        Left = 2,
        Bottom = 3
    }

    public static class DoorDirExt
    {
        public static DoorDir Invert(this DoorDir dir)
        {
            switch(dir)
            {
                case DoorDir.Right:
                    return DoorDir.Left;
                case DoorDir.Top:
                    return DoorDir.Bottom;
                case DoorDir.Left:
                    return DoorDir.Right;
                case DoorDir.Bottom:
                    return DoorDir.Top;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }
        
        public static Vector3Int GetMoveDir(this DoorDir dir)
        {
            switch(dir)
            {
                case DoorDir.Right:
                    return Vector3Int.up;
                case DoorDir.Top:
                    return Vector3Int.up;
                case DoorDir.Left:
                    return Vector3Int.left;
                case DoorDir.Bottom:
                    return Vector3Int.down;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }
    }
}