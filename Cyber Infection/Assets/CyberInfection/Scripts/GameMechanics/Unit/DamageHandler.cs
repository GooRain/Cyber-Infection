namespace CyberInfection.GameMechanics.Unit
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