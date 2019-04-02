using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.Components
{
    public interface IShootComponent
    {
        void Shoot(Vector2 direction);
    }
}