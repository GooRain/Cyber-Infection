using UnityEngine;

namespace CyberInfection.Extension.Utility
{
    public static class AnimatorHash
    {
        public static readonly int Horizontal = Animator.StringToHash("Horizontal");
        public static readonly int Vertical = Animator.StringToHash("Vertical");
        public static readonly int Magnitude = Animator.StringToHash("Magnitude");
    }
}