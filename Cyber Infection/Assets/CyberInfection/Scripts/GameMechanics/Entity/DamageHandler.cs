namespace CyberInfection.GameMechanics.Entity
{
    public class DamageHandler
    {
        private IAlive _unit;

        public void GetDamage(int damageAmount)
        {
            _unit.health -= damageAmount;
        }
    }
}