namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
    public interface IDamager
    {
        void Damage(IDamagable target);
    }
}