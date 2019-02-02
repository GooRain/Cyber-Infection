using System;

namespace CyberInfection.Generation.Room
{
	[Flags]
	public enum RoomType : ushort
	{
		None = 0,
		Start = 1,
		Basic = 2,
		End = 4,
		Boss = 8,
		Advanced = 16,
		
		BossEnd = 12,
		AdvancedBoss = 24,
		AdvancedBossEnd = 28
	}
}