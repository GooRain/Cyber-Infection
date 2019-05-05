namespace CyberInfection.GameMechanics.Entity
{
    public class DamageHandler
    {
        private IAlive _unit;

        public void GetDamage(float damageAmount)
        {
            _unit.health -= damageAmount;
        }
    }
}