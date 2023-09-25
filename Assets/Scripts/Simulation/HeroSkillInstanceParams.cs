using System;
using System.Collections.Generic;

namespace Simulation
{
	public class HeroSkillInstanceParams
	{
		public HeroSkillInstanceParams()
		{
			this.isSkillActives = new List<bool>();
			this.isSkillTogglable = new List<bool>();
			this.isSkillToggling = new List<bool>();
			this.canActivateSkills = new List<bool>();
			this.cooldownRatios = new List<float>();
			this.toggleDeltas = new List<float>();
			this.cooldownRatios1 = new List<float>();
			this.cooldownRatios2 = new List<float>();
			this.cooldownMaxes = new List<float>();
			this.heroReviveTimes = new List<float>();
			this.heroReviveTimeMaxes = new List<float>();
			this.heroRechargeBuffs = new List<bool>();
			this.heroStunnedBuffs = new List<bool>();
			this.heroSilencedBuffs = new List<bool>();
			this.skillTypes = new List<Type>();
		}

		public void Clear()
		{
			this.isSkillActives.Clear();
			this.isSkillTogglable.Clear();
			this.isSkillToggling.Clear();
			this.canActivateSkills.Clear();
			this.cooldownRatios.Clear();
			this.toggleDeltas.Clear();
			this.cooldownRatios1.Clear();
			this.cooldownRatios2.Clear();
			this.cooldownMaxes.Clear();
			this.heroReviveTimes.Clear();
			this.heroReviveTimeMaxes.Clear();
			this.heroRechargeBuffs.Clear();
			this.heroStunnedBuffs.Clear();
			this.heroSilencedBuffs.Clear();
			this.skillTypes.Clear();
		}

		public List<bool> isSkillActives;

		public List<bool> isSkillTogglable;

		public List<bool> isSkillToggling;

		public List<bool> canActivateSkills;

		public List<float> cooldownRatios;

		public List<float> toggleDeltas;

		public List<float> cooldownRatios1;

		public List<float> cooldownRatios2;

		public List<float> cooldownMaxes;

		public List<float> heroReviveTimes;

		public List<float> heroReviveTimeMaxes;

		public List<bool> heroRechargeBuffs;

		public List<bool> heroStunnedBuffs;

		public List<bool> heroSilencedBuffs;

		public List<Type> skillTypes;
	}
}
