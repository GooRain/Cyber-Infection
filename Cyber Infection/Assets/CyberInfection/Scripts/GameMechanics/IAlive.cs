﻿namespace CyberInfection.GameMechanics
{
	public interface IAlive
	{
		float health { get; set; }
		void GetDamage(float damageAmount);
		void Die();
	}
}