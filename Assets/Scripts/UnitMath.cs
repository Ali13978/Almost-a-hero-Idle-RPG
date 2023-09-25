using System;

public class UnitMath
{
	public static double GetGoldToDropForPower(double power)
	{
		return GameMath.PowDouble(UnitMath.GOLD_POW_ADV, power);
	}

	public static double GetDamageForPower(double power)
	{
		return GameMath.PowDouble(UnitMath.DAMAGE_POW_ADV, power);
	}

	public static double GetEnemyHealthForPower(double power)
	{
		double health_POW_ADV = UnitMath.HEALTH_POW_ADV;
		double num = GameMath.Lerp(0.35, 1.0, GameMath.Clamp((power - 10.0) / 190.0, 0.0, 1.0));
		return num * GameMath.PowDouble(health_POW_ADV, power);
	}

	public static double GetGoldToDropForPowerGog(double power)
	{
		return GameMath.PowDouble(UnitMath.GOLD_POW_GOG, power);
	}

	public static double GetDamageForPowerGog(double power)
	{
		return GameMath.PowDouble(UnitMath.DAMAGE_POW_GOG, power);
	}

	public static double GetHealthForPowerGog(double power)
	{
		return GameMath.PowDouble(UnitMath.HEALTH_POW_GOG, power);
	}

	public static double TOTEM_DAMAGE_INC = 1.3;

	public static double GOLD_POW_ADV = 1.27;

	public static double DAMAGE_POW_ADV = 1.45;

	public static double HEALTH_POW_ADV = 1.45;

	public static double GOLD_POW_GOG = 1.35;

	public static double DAMAGE_POW_GOG = 1.518;

	public static double HEALTH_POW_GOG = 1.518;
}
