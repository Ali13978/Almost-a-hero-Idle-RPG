using System;
using System.Collections.Generic;
using DynamicLoading;
using Simulation;
using UnityEngine;

public class SoundArchieve : MonoBehaviour
{
	public void UnloadCommonHeroSounds()
	{
		this.heroRevive.UnloadAsset();
		this.heroDeathCommon1.UnloadAsset();
		this.heroDeathCommon2.UnloadAsset();
		this.heroDeathCommon3.UnloadAsset();
	}

	public void OnHoratioMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voHoratioSelected.Length; i++)
		{
			this.voHoratioSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voHoratioUpgradeItem.Length; j++)
		{
			this.voHoratioUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadHoratioSounds()
	{
		AudioClipPromise.UnloadAssets(this.horatioAttacks);
		AudioClipPromise.UnloadAssets(this.horatioDamages);
		AudioClipPromise.UnloadAssets(this.horatioAutoSkills);
		this.horatioHasThePower.UnloadAsset();
		this.horatioReverseExcaliburTakeGround.UnloadAsset();
		this.horatioReverseExcaliburInsertSword.UnloadAsset();
		this.horatioReverseExcaliburThrowRock.UnloadAsset();
		this.horatioUltiStart.UnloadAsset();
		this.horatioUltiLoop.UnloadAsset();
		this.horatioUltiEnd.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voHoratioSpawn);
		AudioClipPromise.UnloadAssets(this.voHoratioDeath);
		AudioClipPromise.UnloadAssets(this.voHoratioWelcome);
		AudioClipPromise.UnloadAssets(this.voHoratioEnvChange);
		AudioClipPromise.UnloadAssets(this.voHoratioLevelUp);
		AudioClipPromise.UnloadAssets(this.voHoratioUlti);
		AudioClipPromise.UnloadAssets(this.voHoratioSkillAuto1);
		AudioClipPromise.UnloadAssets(this.voHoratioSkillAuto2);
		AudioClipPromise.UnloadAssets(this.voHoratioCheer);
		AudioClipPromise.UnloadAssets(this.voHoratioUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voHoratioSelected);
	}

	public void OnKindLennyMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voLennySelected.Length; i++)
		{
			this.voLennySelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voLennyUpgradeItem.Length; j++)
		{
			this.voLennyUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadKindLennySounds()
	{
		AudioClipPromise.UnloadAssets(this.lennyAttacks);
		AudioClipPromise.UnloadAssets(this.lennyDamages);
		AudioClipPromise.UnloadAssets(this.lennyAutoSkills);
		AudioClipPromise.UnloadAssets(this.lennyBombardAttacks);
		this.lennyDeath.UnloadAsset();
		this.lennyRevive.UnloadAsset();
		this.lennyUltiStart.UnloadAsset();
		this.lennyUltiLoop.UnloadAsset();
		this.lennyUltiEnd.UnloadAsset();
		this.lennyReload.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voLennySpawn);
		AudioClipPromise.UnloadAssets(this.voLennyDeath);
		AudioClipPromise.UnloadAssets(this.voLennyRevive);
		AudioClipPromise.UnloadAssets(this.voLennyWelcome);
		AudioClipPromise.UnloadAssets(this.voLennyEnvChange);
		AudioClipPromise.UnloadAssets(this.voLennyLevelUp);
		AudioClipPromise.UnloadAssets(this.voLennyUlti);
		AudioClipPromise.UnloadAssets(this.voLennySkillAuto1);
		AudioClipPromise.UnloadAssets(this.voLennySkillAuto2);
		AudioClipPromise.UnloadAssets(this.voLennyCheer);
		AudioClipPromise.UnloadAssets(this.voLennyUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voLennySelected);
	}

	public void OnThourMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voThourSelected.Length; i++)
		{
			this.voThourSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voThourUpgradeItem.Length; j++)
		{
			this.voThourUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadThourSounds()
	{
		AudioClipPromise.UnloadAssets(this.thourAttacks);
		AudioClipPromise.UnloadAssets(this.thourAutoSkills);
		this.thourUlti.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.thourUltiAttacks);
		AudioClipPromise.UnloadAssets(this.voThourSpawn);
		AudioClipPromise.UnloadAssets(this.voThourDeath);
		AudioClipPromise.UnloadAssets(this.voThourRevive);
		AudioClipPromise.UnloadAssets(this.voThourWelcome);
		AudioClipPromise.UnloadAssets(this.voThourEnvChange);
		AudioClipPromise.UnloadAssets(this.voThourLevelUp);
		AudioClipPromise.UnloadAssets(this.voThourUlti);
		AudioClipPromise.UnloadAssets(this.voThourSkillAuto1);
		AudioClipPromise.UnloadAssets(this.voThourSkillAuto2);
		AudioClipPromise.UnloadAssets(this.voThourCheer);
		AudioClipPromise.UnloadAssets(this.voThourUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voThourSelected);
	}

	public void OnIdaMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voIdaSelected.Length; i++)
		{
			this.voIdaSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voIdaUpgradeItem.Length; j++)
		{
			this.voIdaUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadIdaSounds()
	{
		AudioClipPromise.UnloadAssets(this.idaAttacks);
		AudioClipPromise.UnloadAssets(this.idaDamages);
		AudioClipPromise.UnloadAssets(this.idaAutoSkills);
		this.idaHitGround.UnloadAsset();
		this.idaUltiStart.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.idaUltiLoop);
		AudioClipPromise.UnloadAssets(this.voIdaSpawn);
		AudioClipPromise.UnloadAssets(this.voIdaDeath);
		AudioClipPromise.UnloadAssets(this.voIdaRevive);
		AudioClipPromise.UnloadAssets(this.voIdaWelcome);
		AudioClipPromise.UnloadAssets(this.voIdaEnvChange);
		AudioClipPromise.UnloadAssets(this.voIdaLevelUp);
		AudioClipPromise.UnloadAssets(this.voIdaUlti);
		AudioClipPromise.UnloadAssets(this.voIdaSkillAuto1);
		AudioClipPromise.UnloadAssets(this.voIdaSkillAuto2);
		AudioClipPromise.UnloadAssets(this.voIdaCheer);
		AudioClipPromise.UnloadAssets(this.voIdaUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voIdaSelected);
	}

	public void OnDerekMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voDerekSelected.Length; i++)
		{
			this.voDerekSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voDerekUpgradeItem.Length; j++)
		{
			this.voDerekUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadDerekSounds()
	{
		AudioClipPromise.UnloadAssets(this.derekAttacks);
		AudioClipPromise.UnloadAssets(this.derekAttackImpacts);
		AudioClipPromise.UnloadAssets(this.derekDamages);
		AudioClipPromise.UnloadAssets(this.derekAutoSkills);
		this.derekUlti.UnloadAsset();
		this.derekThrowBook.UnloadAsset();
		this.derekBookExplosion.UnloadAsset();
		this.derekFireball.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voDerekSpawn);
		AudioClipPromise.UnloadAssets(this.voDerekDeath);
		AudioClipPromise.UnloadAssets(this.voDerekRevive);
		AudioClipPromise.UnloadAssets(this.voDerekWelcome);
		AudioClipPromise.UnloadAssets(this.voDerekEnvChange);
		AudioClipPromise.UnloadAssets(this.voDerekLevelUp);
		AudioClipPromise.UnloadAssets(this.voDerekUlti);
		AudioClipPromise.UnloadAssets(this.voDerekSkillAuto1);
		AudioClipPromise.UnloadAssets(this.voDerekSkillAuto2);
		AudioClipPromise.UnloadAssets(this.voDerekCheer);
		AudioClipPromise.UnloadAssets(this.voDerekSelected);
		AudioClipPromise.UnloadAssets(this.voDerekUpgradeItem);
	}

	public void OnSheelaMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voSheelaSelected.Length; i++)
		{
			this.voSheelaSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voSheelaUpgradeItem.Length; j++)
		{
			this.voSheelaUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadSheelaSounds()
	{
		AudioClipPromise.UnloadAssets(this.sheelaAttacks);
		AudioClipPromise.UnloadAssets(this.sheelaAutoSkills);
		AudioClipPromise.UnloadAssets(this.sheelaAutoSkillBacks);
		AudioClipPromise.UnloadAssets(this.sheelaUltiAttacks);
		this.sheelaUlti.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.sheelaReloads);
		AudioClipPromise.UnloadAssets(this.voSheelaSpawn);
		AudioClipPromise.UnloadAssets(this.voSheelaDeath);
		AudioClipPromise.UnloadAssets(this.voSheelaRevive);
		AudioClipPromise.UnloadAssets(this.voSheelaWelcome);
		AudioClipPromise.UnloadAssets(this.voSheelaEnvChange);
		AudioClipPromise.UnloadAssets(this.voSheelaLevelUp);
		AudioClipPromise.UnloadAssets(this.voSheelaUlti);
		AudioClipPromise.UnloadAssets(this.voSheelaSkillAuto1);
		AudioClipPromise.UnloadAssets(this.voSheelaSkillAuto2);
		AudioClipPromise.UnloadAssets(this.voSheelaCheer);
		AudioClipPromise.UnloadAssets(this.voSheelaUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voSheelaSelected);
	}

	public void OnBombermanMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voBombermanSelected.Length; i++)
		{
			this.voBombermanSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voBombermanUpgradeItem.Length; j++)
		{
			this.voBombermanUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadBombermanSounds()
	{
		AudioClipPromise.UnloadAssets(this.bombermanAttacks);
		AudioClipPromise.UnloadAssets(this.bombermanAttackImpacts);
		this.bombermanFriendlyThrow.UnloadAsset();
		this.bombermanFriendlyCatch.UnloadAsset();
		this.bombermanFireworkLaunch.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.bombermanFireworks);
		this.bombermanUlti.UnloadAsset();
		this.bombermanUltiExplosion.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voBombermanSpawn);
		AudioClipPromise.UnloadAssets(this.voBombermanDeath);
		AudioClipPromise.UnloadAssets(this.voBombermanRevive);
		AudioClipPromise.UnloadAssets(this.voBombermanWelcome);
		AudioClipPromise.UnloadAssets(this.voBombermanEnvChange);
		AudioClipPromise.UnloadAssets(this.voBombermanLevelUp);
		AudioClipPromise.UnloadAssets(this.voBombermanUlti);
		AudioClipPromise.UnloadAssets(this.voBombermanSkillAuto1);
		AudioClipPromise.UnloadAssets(this.voBombermanSkillAuto2);
		AudioClipPromise.UnloadAssets(this.voBombermanCheer);
		AudioClipPromise.UnloadAssets(this.voBombermanUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voBombermanSelected);
	}

	public void OnSamMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voSamSelected.Length; i++)
		{
			this.voSamSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voSamUpgradeItem.Length; j++)
		{
			this.voSamUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadSamSounds()
	{
		AudioClipPromise.UnloadAssets(this.samAttacks);
		AudioClipPromise.UnloadAssets(this.samAttackImpacts);
		AudioClipPromise.UnloadAssets(this.samAutoSkills);
		this.samAutoSkillBack.UnloadAsset();
		this.samDeath.UnloadAsset();
		this.samUltiStart.UnloadAsset();
		this.samUltiEnd.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voSamSpawn);
		AudioClipPromise.UnloadAssets(this.voSamDeath);
		AudioClipPromise.UnloadAssets(this.voSamRevive);
		AudioClipPromise.UnloadAssets(this.voSamWelcome);
		AudioClipPromise.UnloadAssets(this.voSamEnvChange);
		AudioClipPromise.UnloadAssets(this.voSamLevelUp);
		AudioClipPromise.UnloadAssets(this.voSamUlti);
		AudioClipPromise.UnloadAssets(this.voSamSkillAuto1);
		AudioClipPromise.UnloadAssets(this.voSamSkillAuto2);
		AudioClipPromise.UnloadAssets(this.voSamCheer);
		AudioClipPromise.UnloadAssets(this.voSamUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voSamSelected);
	}

	public void OnLiaMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voLiaSelected.Length; i++)
		{
			this.voLiaSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voLiaUpgradeItem.Length; j++)
		{
			this.voLiaUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadLiaSounds()
	{
		AudioClipPromise.UnloadAssets(this.liaAttacks);
		AudioClipPromise.UnloadAssets(this.liaAttackImpacts);
		AudioClipPromise.UnloadAssets(this.liaAutoSkills);
		this.liaUltiStart.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.liaUltiAttacks);
		AudioClipPromise.UnloadAssets(this.voLiaSpawn);
		AudioClipPromise.UnloadAssets(this.voLiaDeath);
		AudioClipPromise.UnloadAssets(this.voLiaRevive);
		AudioClipPromise.UnloadAssets(this.voLiaWelcome);
		AudioClipPromise.UnloadAssets(this.voLiaEnvChange);
		AudioClipPromise.UnloadAssets(this.voLiaLevelUp);
		AudioClipPromise.UnloadAssets(this.voLiaUlti);
		AudioClipPromise.UnloadAssets(this.voLiaSkillAuto1);
		AudioClipPromise.UnloadAssets(this.voLiaSkillAuto2);
		AudioClipPromise.UnloadAssets(this.voLiaCheer);
		AudioClipPromise.UnloadAssets(this.voLiaUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voLiaSelected);
	}

	public void OnJimMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voJimSelected.Length; i++)
		{
			this.voJimSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voJimUpgradeItem.Length; j++)
		{
			this.voJimUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadJimSounds()
	{
		AudioClipPromise.UnloadAssets(this.jimAttacks);
		AudioClipPromise.UnloadAssets(this.jimBattleCry);
		AudioClipPromise.UnloadAssets(this.jimWeepingSong);
		this.jimUltiStart.UnloadAsset();
		this.jimUltiLoop.UnloadAsset();
		this.jimUltiEnd.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voJimSpawn);
		AudioClipPromise.UnloadAssets(this.voJimDeath);
		AudioClipPromise.UnloadAssets(this.voJimRevive);
		AudioClipPromise.UnloadAssets(this.voJimWelcome);
		AudioClipPromise.UnloadAssets(this.voJimEnvChange);
		AudioClipPromise.UnloadAssets(this.voJimLevelUp);
		AudioClipPromise.UnloadAssets(this.voJimUlti);
		AudioClipPromise.UnloadAssets(this.voJimSkillBattlecry);
		AudioClipPromise.UnloadAssets(this.voJimSkillWeepingsong);
		AudioClipPromise.UnloadAssets(this.voJimCheer);
		AudioClipPromise.UnloadAssets(this.voJimUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voJimSelected);
	}

	public void OnTamMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voTamSelected.Length; i++)
		{
			this.voTamSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voTamUpgradeItem.Length; j++)
		{
			this.voTamUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadTamSounds()
	{
		AudioClipPromise.UnloadAssets(this.tamAttacks);
		this.tamReload.UnloadAsset();
		this.tamCrowAttack.UnloadAsset();
		this.tamFlare.UnloadAsset();
		this.tamUlti.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voTamSpawn);
		AudioClipPromise.UnloadAssets(this.voTamDeath);
		AudioClipPromise.UnloadAssets(this.voTamRevive);
		AudioClipPromise.UnloadAssets(this.voTamWelcome);
		AudioClipPromise.UnloadAssets(this.voTamEnvChange);
		AudioClipPromise.UnloadAssets(this.voTamLevelUp);
		AudioClipPromise.UnloadAssets(this.voTamUlti);
		AudioClipPromise.UnloadAssets(this.voTamCrowAttack);
		AudioClipPromise.UnloadAssets(this.voTamFlare);
		AudioClipPromise.UnloadAssets(this.voTamCheer);
		AudioClipPromise.UnloadAssets(this.voTamUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voTamSelected);
	}

	public void OnGoblinMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voGoblinSelected.Length; i++)
		{
			this.voGoblinSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voGoblinUpgradeItem.Length; j++)
		{
			this.voGoblinUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadGoblinSounds()
	{
		AudioClipPromise.UnloadAssets(this.goblinAttacks);
		AudioClipPromise.UnloadAssets(this.goblinAttackImpacts);
		AudioClipPromise.UnloadAssets(this.goblinAutoSkills);
		this.goblinUlti.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voGoblinSpawn);
		AudioClipPromise.UnloadAssets(this.voGoblinDeath);
		AudioClipPromise.UnloadAssets(this.voGoblinRevive);
		AudioClipPromise.UnloadAssets(this.voGoblinWelcome);
		AudioClipPromise.UnloadAssets(this.voGoblinEnvChange);
		AudioClipPromise.UnloadAssets(this.voGoblinLevelUp);
		AudioClipPromise.UnloadAssets(this.voGoblinUlti);
		AudioClipPromise.UnloadAssets(this.voGoblinNegotiate);
		AudioClipPromise.UnloadAssets(this.voGoblinAffinities);
		AudioClipPromise.UnloadAssets(this.voGoblinCheer);
		AudioClipPromise.UnloadAssets(this.voGoblinUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voGoblinSelected);
	}

	public void OnWarlockMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voWarlockSelected.Length; i++)
		{
			this.voWarlockSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voWarlockUpgradeItem.Length; j++)
		{
			this.voWarlockUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadWarlockSounds()
	{
		AudioClipPromise.UnloadAssets(this.warlockAttacks);
		AudioClipPromise.UnloadAssets(this.warlockAttackImpacts);
		AudioClipPromise.UnloadAssets(this.warlockAutoSkills);
		this.warlockDeath.UnloadAsset();
		this.warlockUltiStart.UnloadAsset();
		this.warlockUltiLoop.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.warlockUltiAttacks);
		this.warlockUltiEnd.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voWarlockSpawn);
		AudioClipPromise.UnloadAssets(this.voWarlockDeath);
		AudioClipPromise.UnloadAssets(this.voWarlockRevive);
		AudioClipPromise.UnloadAssets(this.voWarlockWelcome);
		AudioClipPromise.UnloadAssets(this.voWarlockEnvChange);
		AudioClipPromise.UnloadAssets(this.voWarlockLevelUp);
		AudioClipPromise.UnloadAssets(this.voWarlockUlti);
		AudioClipPromise.UnloadAssets(this.voWarlockRegret);
		AudioClipPromise.UnloadAssets(this.voWarlockSwarm);
		AudioClipPromise.UnloadAssets(this.voWarlockCheer);
		AudioClipPromise.UnloadAssets(this.voWarlockUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voWarlockSelected);
	}

	public void OnBabuMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voBabuSelected.Length; i++)
		{
			this.voBabuSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voBabuUpgradeItem.Length; j++)
		{
			this.voBabuUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadBabuSounds()
	{
		AudioClipPromise.UnloadAssets(this.babuAttacks);
		AudioClipPromise.UnloadAssets(this.babuAttackImpacts);
		AudioClipPromise.UnloadAssets(this.babuAutoSkills);
		this.babuDeath.UnloadAsset();
		this.babuDeathInUlti.UnloadAsset();
		this.babuUltiStart.UnloadAsset();
		this.babuUltiLoop.UnloadAsset();
		this.babuUltiEnd.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voBabuSpawn);
		AudioClipPromise.UnloadAssets(this.voBabuDeath);
		AudioClipPromise.UnloadAssets(this.voBabuRevive);
		AudioClipPromise.UnloadAssets(this.voBabuWelcome);
		AudioClipPromise.UnloadAssets(this.voBabuEnvChange);
		AudioClipPromise.UnloadAssets(this.voBabuLevelUp);
		AudioClipPromise.UnloadAssets(this.voBabuUlti);
		AudioClipPromise.UnloadAssets(this.voBabuGoodCupOfTea);
		AudioClipPromise.UnloadAssets(this.voBabuGotYourBack);
		AudioClipPromise.UnloadAssets(this.voBabuCheer);
		AudioClipPromise.UnloadAssets(this.voBabuCheer);
		AudioClipPromise.UnloadAssets(this.voBabuUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voBabuSelected);
	}

	public void OnDruidMainSoundsLoaded(HeroBundle heroBundle)
	{
		for (int i = 0; i < this.voDruidSelected.Length; i++)
		{
			this.voDruidSelected[i].Clip = heroBundle.SelectedAudioClips[i];
		}
		for (int j = 0; j < this.voDruidUpgradeItem.Length; j++)
		{
			this.voDruidUpgradeItem[j].Clip = heroBundle.UpgradeItemAudioClips[j];
		}
	}

	public void UnloadDruidSounds()
	{
		AudioClipPromise.UnloadAssets(this.druidPawsAttacks);
		AudioClipPromise.UnloadAssets(this.druidGrowAttacks);
		AudioClipPromise.UnloadAssets(this.druidTailAttacks);
		AudioClipPromise.UnloadAssets(this.druidAutoSkills);
		AudioClipPromise.UnloadAssets(this.druidLarrySkillAnimalSpawn);
		AudioClipPromise.UnloadAssets(this.druidLarrySkillAnimalDisappear);
		this.druidUltiDeath.UnloadAsset();
		this.druidUltiStart.UnloadAsset();
		this.druidUltiStartShort.UnloadAsset();
		this.druidUltiEnd.UnloadAsset();
		AudioClipPromise.UnloadAssets(this.voDruidSpawn);
		AudioClipPromise.UnloadAssets(this.voDruidDeath);
		AudioClipPromise.UnloadAssets(this.voDruidRevive);
		AudioClipPromise.UnloadAssets(this.voDruidWelcome);
		AudioClipPromise.UnloadAssets(this.voDruidEnvChange);
		AudioClipPromise.UnloadAssets(this.voDruidLevelUp);
		AudioClipPromise.UnloadAssets(this.voDruidStampede);
		AudioClipPromise.UnloadAssets(this.voDruidStampedeBeast);
		AudioClipPromise.UnloadAssets(this.voDruidLarry);
		AudioClipPromise.UnloadAssets(this.voDruidLarryBeast);
		AudioClipPromise.UnloadAssets(this.voDruidCheer);
		AudioClipPromise.UnloadAssets(this.voDruidUpgradeItem);
		AudioClipPromise.UnloadAssets(this.voDruidSelected);
	}

	private void UnloadTimeChallengesVoSounds()
	{
		AudioClipPromise.UnloadAssets(this.voGreenManTimeChallengeComplete);
		AudioClipPromise.UnloadAssets(this.voGreenManTimeChallengeFail);
		this.voGreenManTimeChallengeUnlock.UnloadAsset();
	}

	private void UnloadTutorialVoSounds()
	{
		this.voGreenManFirstWelcome.UnloadAsset();
		this.voGreenManFirstRingOffer.UnloadAsset();
		this.voGreenManFirstRingClaimed.UnloadAsset();
		this.voGreenManFirstHeroesTabUnlock.UnloadAsset();
		this.voGreenManFirstRingUpgrade.UnloadAsset();
		this.voGreenManFirstRingUpgradeDone.UnloadAsset();
	}

	private void UnloadRiftVoSounds()
	{
		AudioClipPromise.UnloadAssets(this.voRiftComplete);
		AudioClipPromise.UnloadAssets(this.voRiftFail);
		this.voWiseSnakeUnlockRift.UnloadAsset();
	}

	public void LoadGameSounds(World world)
	{
		this.LoadBundle("heroes/sounds/common", this.inGameSoundBundlesLoaded);
		foreach (Hero hero in world.heroes)
		{
			string bundleName = HeroIds.HeroSoundsAssetBundleByName[hero.GetId()];
			this.LoadBundle(bundleName, this.inGameSoundBundlesLoaded);
		}
	}

	public void LoadUiBundle(string bundleName)
	{
		this.LoadBundle(bundleName, this.uiSoundBundlesLoaded);
	}

	public void UnloadUiBundle(string bundleName)
	{
		if (!this.uiSoundBundlesLoaded.ContainsKey(bundleName))
		{
			return;
		}
		if (bundleName != null)
		{
			if (!(bundleName == "sounds/greenman-tutorial"))
			{
				if (!(bundleName == "sounds/timechallenge-mode"))
				{
					if (bundleName == "sounds/rift-mode")
					{
						this.UnloadRiftVoSounds();
					}
				}
				else
				{
					this.UnloadTimeChallengesVoSounds();
				}
			}
			else
			{
				this.UnloadTutorialVoSounds();
			}
		}
		this.uiSoundBundlesLoaded.Remove(bundleName);
	}

	private void LoadBundle(string bundleName, Dictionary<string, bool> bundlesContainer)
	{
		if (!bundlesContainer.ContainsKey(bundleName))
		{
			bundlesContainer.Add(bundleName, false);
			DynamicLoadManager.GetPermanentReferenceToBundle(bundleName, delegate
			{
				bundlesContainer[bundleName] = true;
			}, false);
		}
	}

	public void UnloadGameSounds()
	{
		this.soundLoaders.Clear();
		foreach (string text in this.inGameSoundBundlesLoaded.Keys)
		{
			DynamicLoadManager.RemovePermanentReferenceToBundle(text);
			switch (text)
			{
			case "heroes/sounds/horatio":
				this.UnloadHoratioSounds();
				break;
			case "heroes/sounds/kindlenny":
				this.UnloadKindLennySounds();
				break;
			case "heroes/sounds/thour":
				this.UnloadThourSounds();
				break;
			case "heroes/sounds/ida":
				this.UnloadIdaSounds();
				break;
			case "heroes/sounds/wendle":
				this.UnloadDerekSounds();
				break;
			case "heroes/sounds/sheela":
				this.UnloadSheelaSounds();
				break;
			case "heroes/sounds/bomberman":
				this.UnloadBombermanSounds();
				break;
			case "heroes/sounds/sam":
				this.UnloadSamSounds();
				break;
			case "heroes/sounds/lia":
				this.UnloadLiaSounds();
				break;
			case "heroes/sounds/jim":
				this.UnloadJimSounds();
				break;
			case "heroes/sounds/tam":
				this.UnloadTamSounds();
				break;
			case "heroes/sounds/goblin":
				this.UnloadGoblinSounds();
				break;
			case "heroes/sounds/warlock":
				this.UnloadWarlockSounds();
				break;
			case "heroes/sounds/babu":
				this.UnloadBabuSounds();
				break;
			case "heroes/sounds/druid":
				this.UnloadDruidSounds();
				break;
			}
		}
		this.UnloadCommonHeroSounds();
		DynamicLoadManager.RemovePermanentReferenceToBundle("heroes/sounds/common");
		this.inGameSoundBundlesLoaded.Clear();
	}

	public void UnloadGameModesVoices()
	{
		if (this.uiSoundBundlesLoaded.ContainsKey("sounds/rift-mode"))
		{
			this.UnloadRiftVoSounds();
			DynamicLoadManager.UnloadAssetsFromBundle("sounds/rift-mode");
		}
		if (this.uiSoundBundlesLoaded.ContainsKey("sounds/timechallenge-mode"))
		{
			this.UnloadTimeChallengesVoSounds();
			DynamicLoadManager.UnloadAssetsFromBundle("sounds/timechallenge-mode");
		}
	}

	public bool AreAllSoundBundlesLoaded(int heroesCount)
	{
        Debug.Log("AreAllSoundBundlesLoaded " + this.inGameSoundBundlesLoaded.Count);
		if (this.inGameSoundBundlesLoaded.Count - 1 != heroesCount)
		{
			return false;
		}
		using (Dictionary<string, bool>.ValueCollection.Enumerator enumerator = this.inGameSoundBundlesLoaded.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current)
				{
					return false;
				}
			}
		}
		return true;
	}

	public SoundArchieve.SoundLoader LoadSound(string soundPath, string bundleId, Action<AudioClip> onLoaded)
	{
		SoundArchieve.SoundLoader soundLoader = new SoundArchieve.SoundLoader
		{
			SoundPath = soundPath,
			BundleId = bundleId,
			OnLoaded = onLoaded
		};
		this.soundLoaders.Add(soundLoader);
		return soundLoader;
	}

	public void CancelSoundLoading(SoundArchieve.SoundLoader soundLoader)
	{
		this.soundLoaders.Remove(soundLoader);
	}

	private void Awake()
	{
		SoundArchieve.inst = this;
	}

	private void Update()
	{
		for (int i = this.soundLoaders.Count - 1; i >= 0; i--)
		{
			SoundArchieve.SoundLoader soundLoader = this.soundLoaders[i];
			if ((this.inGameSoundBundlesLoaded.ContainsKey(soundLoader.BundleId) && this.inGameSoundBundlesLoaded[soundLoader.BundleId]) || (this.uiSoundBundlesLoaded.ContainsKey(soundLoader.BundleId) && this.uiSoundBundlesLoaded[soundLoader.BundleId]))
			{
				DynamicLoadManager.LoadAsset<AudioClip>(soundLoader.BundleId, soundLoader.SoundPath, soundLoader.OnLoaded, true);
				this.soundLoaders.RemoveFastAt(i);
			}
		}
	}

	public static SoundArchieve inst;

	public AudioClip[] totemThrows;

	public AudioClip totemFireOverheated;

	public AudioClip[] lightnings;

	public AudioClip[] thunderbolts;

	public AudioClip totemIceChargeStart;

	public AudioClip totemIceChargeLoop;

	public AudioClip totemIceChargeEnd;

	public AudioClip totemIceRainLoop;

	public AudioClip[] totemIceStrikes;

	public AudioClipPromise heroRevive = new AudioClipPromise("heroes/sounds/common", "Assets/sound/hero/fx/common/revive.mp3");

	public AudioClipPromise heroDeathCommon1 = new AudioClipPromise("heroes/sounds/common", "Assets/sound/hero/fx/common/death1.mp3");

	public AudioClipPromise heroDeathCommon2 = new AudioClipPromise("heroes/sounds/common", "Assets/sound/hero/fx/common/death2.mp3");

	public AudioClipPromise heroDeathCommon3 = new AudioClipPromise("heroes/sounds/common", "Assets/sound/hero/fx/common/death3.mp3");

	public AudioClipPromise[] horatioAttacks = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.horatioAttacksPaths);

	public AudioClipPromise[] horatioDamages = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.horatioDamagesPaths);

	public AudioClipPromise[] horatioAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.horatioAutoSkillsPaths);

	public AudioClipPromise horatioHasThePower = new AudioClipPromise("heroes/sounds/horatio", "Assets/sound/hero/fx/horatio/Hilt_Ability_2_init_effect.wav");

	public AudioClipPromise horatioReverseExcaliburTakeGround = new AudioClipPromise("heroes/sounds/horatio", SoundIds.horatioReverseExcaliburTakeGroundPath);

	public AudioClipPromise horatioReverseExcaliburInsertSword = new AudioClipPromise("heroes/sounds/horatio", SoundIds.horatioReverseExcaliburInsertSwordPath);

	public AudioClipPromise horatioReverseExcaliburThrowRock = new AudioClipPromise("heroes/sounds/horatio", SoundIds.horatioReverseExcaliburThrowRockPath);

	public AudioClipPromise horatioUltiStart = new AudioClipPromise("heroes/sounds/horatio", "Assets/sound/hero/fx/horatio/Hilt_Ulti_init_effect.wav");

	public AudioClipPromise horatioUltiLoop = new AudioClipPromise("heroes/sounds/horatio", "Assets/sound/hero/fx/horatio/Hilt_Ulti_sword_whoosh_loop.wav");

	public AudioClipPromise horatioUltiEnd = new AudioClipPromise("heroes/sounds/horatio", "Assets/sound/hero/fx/horatio/ultimate_loop_end.mp3");

	public AudioClipPromise[] voHoratioSpawn = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.voHoratioSpawnPaths);

	public AudioClipPromise[] voHoratioDeath = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.voHoratioDeathPaths);

	public AudioClipPromise[] voHoratioRevive = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.voHoratioRevivePaths);

	public AudioClipPromise[] voHoratioWelcome = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.voHoratioWelcomePaths);

	public AudioClipPromise[] voHoratioEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.voHoratioEnvChangePaths);

	public AudioClipPromise[] voHoratioLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.voHoratioLevelUpPaths);

	public AudioClipPromise[] voHoratioUlti = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.voHoratioUltiPaths);

	public AudioClipPromise[] voHoratioSkillAuto1 = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.voHoratioSkillAuto1Paths);

	public AudioClipPromise[] voHoratioSkillAuto2 = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.voHoratioSkillAuto2Paths);

	public AudioClipPromise[] voHoratioCheer = AudioClipPromise.BuildPromises("heroes/sounds/horatio", SoundIds.voHoratioCheerPaths);

	public AudioClipPromise[] voHoratioUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voHoratioSelected = AudioClipPromise.BuildPromises(4);

	public AudioClipPromise[] lennyAttacks = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.lennyAttacksPaths);

	public AudioClipPromise[] lennyDamages = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.lennyDamagesPaths);

	public AudioClipPromise[] lennyAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.lennyAutoSkillsPaths);

	public AudioClipPromise[] lennyBombardAttacks = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.lennyBombardAttackPaths);

	public AudioClipPromise lennyDeath = new AudioClipPromise("heroes/sounds/kindlenny", "Assets/sound/hero/fx/lenny/death.mp3");

	public AudioClipPromise lennyRevive = new AudioClipPromise("heroes/sounds/kindlenny", "Assets/sound/hero/fx/lenny/revive.mp3");

	public AudioClipPromise lennyUltiStart = new AudioClipPromise("heroes/sounds/kindlenny", "Assets/sound/hero/fx/lenny/Lenny_Ulti_init_effect.wav");

	public AudioClipPromise lennyUltiLoop = new AudioClipPromise("heroes/sounds/kindlenny", "Assets/sound/hero/fx/lenny/Lenny_Ulti_shoot_loop.wav");

	public AudioClipPromise lennyUltiEnd = new AudioClipPromise("heroes/sounds/kindlenny", "Assets/sound/hero/fx/lenny/Lenny_Ulti_end.wav");

	public AudioClipPromise lennyReload = new AudioClipPromise("heroes/sounds/kindlenny", "Assets/sound/hero/fx/lenny/Lenny_Reload.wav");

	public AudioClipPromise[] voLennySpawn = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.voLennySpawnPaths);

	public AudioClipPromise[] voLennyDeath = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.voLennyDeathPaths);

	public AudioClipPromise[] voLennyRevive = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.voLennyRevivePaths);

	public AudioClipPromise[] voLennyWelcome = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.voLennyWelcomePaths);

	public AudioClipPromise[] voLennyEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.voLennyEnvChangePaths);

	public AudioClipPromise[] voLennyLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.voLennyLevelUpPaths);

	public AudioClipPromise[] voLennyUlti = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.voLennyLevelUpPaths);

	public AudioClipPromise[] voLennySkillAuto1 = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.voLennySkillAuto1Paths);

	public AudioClipPromise[] voLennySkillAuto2 = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.voLennySkillAuto2Paths);

	public AudioClipPromise[] voLennyCheer = AudioClipPromise.BuildPromises("heroes/sounds/kindlenny", SoundIds.voHoratioCheerPaths);

	public AudioClipPromise[] voLennyUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voLennySelected = AudioClipPromise.BuildPromises(3);

	public AudioClipPromise[] thourAttacks = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.thourAttackPaths);

	public AudioClipPromise[] thourAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.thourAutoSkillsPaths);

	public AudioClipPromise thourUlti = new AudioClipPromise("heroes/sounds/thour", "Assets/sound/hero/fx/thour/Bellylarf_Ulti.wav");

	public AudioClipPromise[] thourUltiAttacks = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.thourUltiAttackPaths);

	public AudioClipPromise[] voThourSpawn = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.voThourSpawnPaths);

	public AudioClipPromise[] voThourDeath = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.voThourDeathPaths);

	public AudioClipPromise[] voThourRevive = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.voThourRevivePaths);

	public AudioClipPromise[] voThourWelcome = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.voThourWelcomePaths);

	public AudioClipPromise[] voThourEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.voThourEnvChangePaths);

	public AudioClipPromise[] voThourLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.voThourLevelUpPaths);

	public AudioClipPromise[] voThourUlti = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.voThourUltiPaths);

	public AudioClipPromise[] voThourSkillAuto1 = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.voThourSkillAuto1Paths);

	public AudioClipPromise[] voThourSkillAuto2 = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.voThourSkillAuto2Paths);

	public AudioClipPromise[] voThourCheer = AudioClipPromise.BuildPromises("heroes/sounds/thour", SoundIds.voThourCheerPath);

	public AudioClipPromise[] voThourUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voThourSelected = AudioClipPromise.BuildPromises(3);

	public AudioClipPromise[] idaAttacks = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.idaAttackPaths);

	public AudioClipPromise[] idaDamages = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.idaDamagesPahts);

	public AudioClipPromise[] idaAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.idaAutoSkillsPath);

	public AudioClipPromise idaHitGround = new AudioClipPromise("heroes/sounds/ida", "Assets/sound/hero/fx/ida/Vexx_Ability_2_hit_ground.wav");

	public AudioClipPromise idaUltiStart = new AudioClipPromise("heroes/sounds/ida", "Assets/sound/hero/fx/ida/Vexx_Ulti_init_effect.wav");

	public AudioClipPromise[] idaUltiLoop = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.idaUltiLoopPaths);

	public AudioClipPromise[] voIdaSpawn = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.voIdaSpawnPaths);

	public AudioClipPromise[] voIdaDeath = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.voIdaDeathPaths);

	public AudioClipPromise[] voIdaRevive = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.voIdaRevivePaths);

	public AudioClipPromise[] voIdaWelcome = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.voIdaWelcomePaths);

	public AudioClipPromise[] voIdaEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.voIdaEnvChangePaths);

	public AudioClipPromise[] voIdaLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.voIdaLevelUpPaths);

	public AudioClipPromise[] voIdaUlti = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.voIdaUltiPaths);

	public AudioClipPromise[] voIdaSkillAuto1 = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.voIdaSkillAuto1Paths);

	public AudioClipPromise[] voIdaSkillAuto2 = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.voIdaSkillAuto2Paths);

	public AudioClipPromise[] voIdaCheer = AudioClipPromise.BuildPromises("heroes/sounds/ida", SoundIds.voIdaCheerPaths);

	public AudioClipPromise[] voIdaUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voIdaSelected = AudioClipPromise.BuildPromises(4);

	public AudioClipPromise[] derekAttacks = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.derekAttackPaths);

	public AudioClipPromise[] derekAttackImpacts = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.derekAttackImpactsPaths);

	public AudioClipPromise[] derekDamages = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.derekDamagesPahts);

	public AudioClipPromise[] derekAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.derekAutoSkillsPath);

	public AudioClipPromise derekUlti = new AudioClipPromise("heroes/sounds/wendle", "Assets/sound/hero/fx/derek/wendel_Ulti_init.wav");

	public AudioClipPromise derekThrowBook = new AudioClipPromise("heroes/sounds/wendle", "Assets/sound/hero/fx/derek/wendel_Ulti_throw_book.wav");

	public AudioClipPromise derekBookExplosion = new AudioClipPromise("heroes/sounds/wendle", "Assets/sound/hero/fx/derek/wendel_Ulti_book_explosion.wav");

	public AudioClipPromise derekFireball = new AudioClipPromise("heroes/sounds/wendle", "Assets/sound/hero/fx/derek/wendel_Ability_2_fireball_impact.wav");

	public AudioClipPromise[] voDerekSpawn = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.voDerekSpawnPaths);

	public AudioClipPromise[] voDerekDeath = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.voDerekDeathPaths);

	public AudioClipPromise[] voDerekRevive = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.voDerekRevivePaths);

	public AudioClipPromise[] voDerekWelcome = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.voDerekWelcomePaths);

	public AudioClipPromise[] voDerekEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.voDerekEnvChangePaths);

	public AudioClipPromise[] voDerekLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.voDerekLevelUpPaths);

	public AudioClipPromise[] voDerekUlti = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.voDerekUltiPaths);

	public AudioClipPromise[] voDerekSkillAuto1 = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.voDerekSkillAuto1Paths);

	public AudioClipPromise[] voDerekSkillAuto2 = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.voDerekSkillAuto2Paths);

	public AudioClipPromise[] voDerekCheer = AudioClipPromise.BuildPromises("heroes/sounds/wendle", SoundIds.voDerekCheerPaths);

	public AudioClipPromise[] voDerekSelected = AudioClipPromise.BuildPromises(4);

	public AudioClipPromise[] voDerekUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] sheelaAttacks = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.sheelaAttackPaths);

	public AudioClipPromise[] sheelaAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.sheelaAutoSkillsPath);

	public AudioClipPromise[] sheelaAutoSkillBacks = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.sheelaAutoSkillBacksPath);

	public AudioClipPromise[] sheelaUltiAttacks = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.sheelaUltiAttacksPaths);

	public AudioClipPromise sheelaUlti = new AudioClipPromise("heroes/sounds/sheela", "Assets/sound/hero/fx/sheela/V_Ulti_init_effect.wav");

	public AudioClipPromise[] sheelaReloads = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.sheelaReloadsPaths);

	public AudioClipPromise[] voSheelaSpawn = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.voSheelaSpawnPaths);

	public AudioClipPromise[] voSheelaDeath = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.voSheelaDeathPaths);

	public AudioClipPromise[] voSheelaRevive = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.voSheelaRevivePaths);

	public AudioClipPromise[] voSheelaWelcome = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.voSheelaWelcomePaths);

	public AudioClipPromise[] voSheelaEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.voSheelaEnvChangePaths);

	public AudioClipPromise[] voSheelaLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.voSheelaLevelUpPaths);

	public AudioClipPromise[] voSheelaUlti = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.voSheelaUltiPaths);

	public AudioClipPromise[] voSheelaSkillAuto1 = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.voSheelaSkillAuto1Paths);

	public AudioClipPromise[] voSheelaSkillAuto2 = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.voSheelaSkillAuto2Paths);

	public AudioClipPromise[] voSheelaCheer = AudioClipPromise.BuildPromises("heroes/sounds/sheela", SoundIds.voSheelaCheerPaths);

	public AudioClipPromise[] voSheelaUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voSheelaSelected = AudioClipPromise.BuildPromises(4);

	public AudioClipPromise[] bombermanAttacks = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.bombermanAttackPaths);

	public AudioClipPromise[] bombermanAttackImpacts = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.bombermanAttackImpactsPaths);

	public AudioClipPromise bombermanFriendlyThrow = new AudioClipPromise("heroes/sounds/bomberman", "Assets/sound/hero/fx/bomberman/Boomer_Ability_2_throw_dinamite.wav");

	public AudioClipPromise bombermanFriendlyCatch = new AudioClipPromise("heroes/sounds/bomberman", "Assets/sound/hero/fx/bomberman/Boomer_Ability_2_catch_dinamite.wav");

	public AudioClipPromise bombermanFireworkLaunch = new AudioClipPromise("heroes/sounds/bomberman", "Assets/sound/hero/fx/bomberman/Boomer_Ability_1_full_anim.wav");

	public AudioClipPromise[] bombermanFireworks = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.bombermanFireworksPaths);

	public AudioClipPromise bombermanUlti = new AudioClipPromise("heroes/sounds/bomberman", "Assets/sound/hero/fx/bomberman/Boomer_Ulti_init_effect.wav");

	public AudioClipPromise bombermanUltiExplosion = new AudioClipPromise("heroes/sounds/bomberman", "Assets/sound/hero/fx/bomberman/Boomer_Ulti_explosion.wav");

	public AudioClipPromise[] voBombermanSpawn = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.voBombermanSpawnPaths);

	public AudioClipPromise[] voBombermanDeath = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.voBombermanDeathPaths);

	public AudioClipPromise[] voBombermanRevive = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.voBombermanRevivePaths);

	public AudioClipPromise[] voBombermanWelcome = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.voBombermanWelcomePaths);

	public AudioClipPromise[] voBombermanEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.voBombermanEnvChangePaths);

	public AudioClipPromise[] voBombermanLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.voBombermanLevelUpPaths);

	public AudioClipPromise[] voBombermanUlti = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.voBombermanUltiPaths);

	public AudioClipPromise[] voBombermanSkillAuto1 = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.voBombermanSkillAuto1Paths);

	public AudioClipPromise[] voBombermanSkillAuto2 = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.voBombermanSkillAuto2Paths);

	public AudioClipPromise[] voBombermanCheer = AudioClipPromise.BuildPromises("heroes/sounds/bomberman", SoundIds.voBombermanCheerPaths);

	public AudioClipPromise[] voBombermanUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voBombermanSelected = AudioClipPromise.BuildPromises(3);

	public AudioClipPromise[] samAttacks = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.samAttackPaths);

	public AudioClipPromise[] samAttackImpacts = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.samAttackImpactsPaths);

	public AudioClipPromise[] samAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.samAutoSkillsPaths);

	public AudioClipPromise samAutoSkillBack = new AudioClipPromise("heroes/sounds/sam", "Assets/sound/hero/fx/sam/Sam_Ability_2_back.wav");

	public AudioClipPromise samDeath = new AudioClipPromise("heroes/sounds/sam", "Assets/sound/hero/fx/sam/death.mp3");

	public AudioClipPromise samUltiStart = new AudioClipPromise("heroes/sounds/sam", "Assets/sound/hero/fx/sam/Sam_Ulti.wav");

	public AudioClipPromise samUltiEnd = new AudioClipPromise("heroes/sounds/sam", "Assets/sound/hero/fx/sam/Sam_Ulti_end.wav");

	public AudioClipPromise[] voSamSpawn = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.voSamSpawnPaths);

	public AudioClipPromise[] voSamDeath = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.voSamDeathPaths);

	public AudioClipPromise[] voSamRevive = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.voSamRevivePaths);

	public AudioClipPromise[] voSamWelcome = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.voSamWelcomePaths);

	public AudioClipPromise[] voSamEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.voSamEnvChangePaths);

	public AudioClipPromise[] voSamLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.voSamLevelUpPaths);

	public AudioClipPromise[] voSamUlti = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.voSamUltiPaths);

	public AudioClipPromise[] voSamSkillAuto1 = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.voSamSkillAuto1Paths);

	public AudioClipPromise[] voSamSkillAuto2 = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.voSamSkillAuto2Paths);

	public AudioClipPromise[] voSamCheer = AudioClipPromise.BuildPromises("heroes/sounds/sam", SoundIds.voSamCheerPaths);

	public AudioClipPromise[] voSamUpgradeItem = AudioClipPromise.BuildPromises(4);

	public AudioClipPromise[] voSamSelected = AudioClipPromise.BuildPromises(4);

	public AudioClipPromise[] liaAttacks = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.liaAttackPaths);

	public AudioClipPromise[] liaAttackImpacts = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.liaAttackImpactsPaths);

	public AudioClipPromise[] liaAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.liaAutoSkillsPaths);

	public AudioClipPromise liaUltiStart = new AudioClipPromise("heroes/sounds/lia", "Assets/sound/hero/fx/blindarcher/Lia_Ulti_init_effect.wav");

	public AudioClipPromise[] liaUltiAttacks = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.liaUltiAttacksPaths);

	public AudioClipPromise[] voLiaSpawn = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.voLiaSpawnPaths);

	public AudioClipPromise[] voLiaDeath = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.voLiaDeathPaths);

	public AudioClipPromise[] voLiaRevive = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.voLiaRevivePaths);

	public AudioClipPromise[] voLiaWelcome = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.voLiaWelcomePaths);

	public AudioClipPromise[] voLiaEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.voLiaEnvChangePaths);

	public AudioClipPromise[] voLiaLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.voLiaLevelUpPaths);

	public AudioClipPromise[] voLiaUlti = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.voLiaUltiPaths);

	public AudioClipPromise[] voLiaSkillAuto1 = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.voLiaSkillAuto1Paths);

	public AudioClipPromise[] voLiaSkillAuto2 = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.voLiaSkillAuto2Paths);

	public AudioClipPromise[] voLiaCheer = AudioClipPromise.BuildPromises("heroes/sounds/lia", SoundIds.voLiaCheerPaths);

	public AudioClipPromise[] voLiaUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voLiaSelected = AudioClipPromise.BuildPromises(3);

	public AudioClipPromise[] jimAttacks = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.jimAttackPaths);

	public AudioClipPromise[] jimBattleCry = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.jimBattleCryPaths);

	public AudioClipPromise[] jimWeepingSong = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.jimWeepingSongPaths);

	public AudioClipPromise jimUltiStart = new AudioClipPromise("heroes/sounds/jim", "Assets/sound/hero/fx/jim/Hansum Jim_Bittersweet_init_V2.wav");

	public AudioClipPromise jimUltiLoop = new AudioClipPromise("heroes/sounds/jim", "Assets/sound/hero/fx/jim/Hansum Jim_Bittersweet_loop_V2.wav");

	public AudioClipPromise jimUltiEnd = new AudioClipPromise("heroes/sounds/jim", "Assets/sound/hero/fx/jim/Hansum Jim_Bittersweet_end_V2.wav");

	public AudioClipPromise[] voJimSpawn = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.voJimSpawnPaths);

	public AudioClipPromise[] voJimDeath = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.voJimDeathPaths);

	public AudioClipPromise[] voJimRevive = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.voJimRevivePaths);

	public AudioClipPromise[] voJimWelcome = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.voJimWelcomePaths);

	public AudioClipPromise[] voJimEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.voJimEnvChangePaths);

	public AudioClipPromise[] voJimLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.voJimLevelUpPaths);

	public AudioClipPromise[] voJimUlti = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.voJimUltiPaths);

	public AudioClipPromise[] voJimSkillBattlecry = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.voJimSkillBattleCryPaths);

	public AudioClipPromise[] voJimSkillWeepingsong = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.voJimSkillWeepingSongPaths);

	public AudioClipPromise[] voJimCheer = AudioClipPromise.BuildPromises("heroes/sounds/jim", SoundIds.voJimCheerPaths);

	public AudioClipPromise[] voJimUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voJimSelected = AudioClipPromise.BuildPromises(6);

	public AudioClipPromise[] tamAttacks = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.tamAttackPaths);

	public AudioClipPromise tamReload = new AudioClipPromise("heroes/sounds/tam", "Assets/sound/hero/fx/tam/Tam_Reload.wav");

	public AudioClipPromise tamCrowAttack = new AudioClipPromise("heroes/sounds/tam", "Assets/sound/hero/fx/tam/Tam_Bird_Attack.wav");

	public AudioClipPromise tamFlare = new AudioClipPromise("heroes/sounds/tam", "Assets/sound/hero/fx/tam/Tam_Throw_flare.wav");

	public AudioClipPromise tamUlti = new AudioClipPromise("heroes/sounds/tam", "Assets/sound/hero/fx/tam/Tam_Attack_Bear_talhaedit.wav");

	public AudioClipPromise[] voTamSpawn = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.voTamSpawnPaths);

	public AudioClipPromise[] voTamDeath = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.voTamDeathPaths);

	public AudioClipPromise[] voTamRevive = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.voTamRevivePaths);

	public AudioClipPromise[] voTamWelcome = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.voTamWelcomePaths);

	public AudioClipPromise[] voTamEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.voTamEnvChangePaths);

	public AudioClipPromise[] voTamLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.voTamLevelUpPaths);

	public AudioClipPromise[] voTamUlti = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.voTamUltiPaths);

	public AudioClipPromise[] voTamCrowAttack = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.voTamCrowAttackPaths);

	public AudioClipPromise[] voTamFlare = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.voTamFlarePaths);

	public AudioClipPromise[] voTamCheer = AudioClipPromise.BuildPromises("heroes/sounds/tam", SoundIds.voTamCheerPaths);

	public AudioClipPromise[] voTamUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voTamSelected = AudioClipPromise.BuildPromises(7);

	public AudioClipPromise[] goblinAttacks = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.goblinAttackPaths);

	public AudioClipPromise[] goblinAttackImpacts = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.goblinAttackImpactsPaths);

	public AudioClipPromise[] goblinAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.goblinAutoSkillsPaths);

	public AudioClipPromise goblinUlti = new AudioClipPromise("heroes/sounds/goblin", "Assets/sound/hero/fx/goblin(redoh)/Redroh_Ultimate.wav");

	public AudioClipPromise[] voGoblinSpawn = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.voGoblinSpawnPaths);

	public AudioClipPromise[] voGoblinDeath = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.voGoblinDeathPaths);

	public AudioClipPromise[] voGoblinRevive = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.voGoblinRevivePaths);

	public AudioClipPromise[] voGoblinWelcome = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.voGoblinWelcomePaths);

	public AudioClipPromise[] voGoblinEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.voGoblinEnvChangePaths);

	public AudioClipPromise[] voGoblinLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.voGoblinLevelUpPaths);

	public AudioClipPromise[] voGoblinUlti = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.voGoblinUltiPaths);

	public AudioClipPromise[] voGoblinNegotiate = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.voGoblinNegotiatePaths);

	public AudioClipPromise[] voGoblinAffinities = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.voGoblinAffinitiesPaths);

	public AudioClipPromise[] voGoblinCheer = AudioClipPromise.BuildPromises("heroes/sounds/goblin", SoundIds.voGoblinCheerPaths);

	public AudioClipPromise[] voGoblinUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voGoblinSelected = AudioClipPromise.BuildPromises(3);

	public AudioClipPromise[] warlockAttacks = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.warlockAttackPaths);

	public AudioClipPromise[] warlockAttackImpacts = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.warlockAttackImpactsPaths);

	public AudioClipPromise[] warlockAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.warlockAutoSkillsPaths);

	public AudioClipPromise warlockDeath = new AudioClipPromise("heroes/sounds/warlock", "Assets/sound/hero/fx/warlock(uno)/Uno_Death.wav");

	public AudioClipPromise warlockUltiStart = new AudioClipPromise("heroes/sounds/warlock", "Assets/sound/hero/fx/warlock(uno)/Uno_Ultimate_start.wav");

	public AudioClipPromise warlockUltiLoop = new AudioClipPromise("heroes/sounds/warlock", "Assets/sound/hero/fx/warlock(uno)/Uno_Ultimate_loop.wav");

	public AudioClipPromise[] warlockUltiAttacks = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.warlockUltiAttacksPaths);

	public AudioClipPromise warlockUltiEnd = new AudioClipPromise("heroes/sounds/warlock", "Assets/sound/hero/fx/warlock(uno)/Uno_Ultimate_End.wav");

	public AudioClipPromise[] voWarlockSpawn = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.voWarlockSpawnPaths);

	public AudioClipPromise[] voWarlockDeath = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.voWarlockDeathPaths);

	public AudioClipPromise[] voWarlockRevive = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.voWarlockRevivePaths);

	public AudioClipPromise[] voWarlockWelcome = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.voWarlockWelcomePaths);

	public AudioClipPromise[] voWarlockEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.voWarlockEnvChangePaths);

	public AudioClipPromise[] voWarlockLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.voWarlockLevelUpPaths);

	public AudioClipPromise[] voWarlockUlti = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.voWarlockUltiPaths);

	public AudioClipPromise[] voWarlockRegret = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.voWarlockRegretPaths);

	public AudioClipPromise[] voWarlockSwarm = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.voWarlockSwarmPaths);

	public AudioClipPromise[] voWarlockCheer = AudioClipPromise.BuildPromises("heroes/sounds/warlock", SoundIds.voWarlockCheerPaths);

	public AudioClipPromise[] voWarlockUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voWarlockSelected = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] babuAttacks = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.babuAttackPaths);

	public AudioClipPromise[] babuAttackImpacts = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.babuAttackImpactsPaths);

	public AudioClipPromise[] babuAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.babuAutoSkillsPaths);

	public AudioClipPromise babuDeath = new AudioClipPromise("heroes/sounds/babu", "Assets/sound/hero/fx/babu/Nanna_death.wav");

	public AudioClipPromise babuDeathInUlti = new AudioClipPromise("heroes/sounds/babu", "Assets/sound/hero/fx/babu/Nanna_death_ultimate.wav");

	public AudioClipPromise babuUltiStart = new AudioClipPromise("heroes/sounds/babu", "Assets/sound/hero/fx/babu/Nanna_ultimate_start.wav");

	public AudioClipPromise babuUltiLoop = new AudioClipPromise("heroes/sounds/babu", "Assets/sound/hero/fx/babu/Nanna_ultimate_idle.wav");

	public AudioClipPromise babuUltiEnd = new AudioClipPromise("heroes/sounds/babu", "Assets/sound/hero/fx/babu/Nanna_ultimate_end.wav");

	public AudioClipPromise[] voBabuSpawn = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.voBabuSpawnPaths);

	public AudioClipPromise[] voBabuDeath = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.voBabuDeathPaths);

	public AudioClipPromise[] voBabuRevive = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.voBabuRevivePaths);

	public AudioClipPromise[] voBabuWelcome = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.voBabuWelcomePaths);

	public AudioClipPromise[] voBabuEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.voBabuEnvChangePaths);

	public AudioClipPromise[] voBabuLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.voBabuLevelUpPaths);

	public AudioClipPromise[] voBabuUlti = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.voBabuUltiPaths);

	public AudioClipPromise[] voBabuGoodCupOfTea = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.voBabuGoodCupOfTea);

	public AudioClipPromise[] voBabuGotYourBack = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.voBabuGotYourBack);

	public AudioClipPromise[] voBabuCheer = AudioClipPromise.BuildPromises("heroes/sounds/babu", SoundIds.voBabuCheerPaths);

	public AudioClipPromise[] voBabuUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voBabuSelected = AudioClipPromise.BuildPromises(6);

	public AudioClipPromise[] druidPawsAttacks = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.druidAttackPawsPaths);

	public AudioClipPromise[] druidGrowAttacks = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.druidAttackGrowlPaths);

	public AudioClipPromise[] druidTailAttacks = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.druidAttackTailPaths);

	public AudioClipPromise[] druidAutoSkills = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.druidAutoSkillsPaths);

	public AudioClipPromise[] druidLarrySkillAnimalSpawn = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.druidLarrySkillAnimalsSpawnPaths);

	public AudioClipPromise[] druidLarrySkillAnimalDisappear = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.druidLarrySkillAnimalsDisappearPaths);

	public AudioClipPromise druidUltiDeath = new AudioClipPromise("heroes/sounds/druid", "Assets/sound/hero/fx/druid/Ron_Werewolf_die.wav");

	public AudioClipPromise druidUltiStart = new AudioClipPromise("heroes/sounds/druid", "Assets/sound/hero/fx/druid/Ron_transforms_werewolf_V2.wav");

	public AudioClipPromise druidUltiStartShort = new AudioClipPromise("heroes/sounds/druid", "Assets/sound/hero/fx/druid/Ron_transforms_werewolf_short_(1).wav");

	public AudioClipPromise druidUltiEnd = new AudioClipPromise("heroes/sounds/druid", "Assets/sound/hero/fx/druid/Ron_transforms_human.wav");

	public AudioClipPromise[] voDruidSpawn = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidSpawnPaths);

	public AudioClipPromise[] voDruidDeath = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidDeathPaths);

	public AudioClipPromise[] voDruidRevive = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidRevivePaths);

	public AudioClipPromise[] voDruidWelcome = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidWelcomePaths);

	public AudioClipPromise[] voDruidEnvChange = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidEnvChangePaths);

	public AudioClipPromise[] voDruidLevelUp = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidLevelUpPaths);

	public AudioClipPromise[] voDruidStampede = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidStampede);

	public AudioClipPromise[] voDruidStampedeBeast = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidStampedeBeast);

	public AudioClipPromise[] voDruidLarry = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidLarry);

	public AudioClipPromise[] voDruidLarryBeast = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidLarryBeast);

	public AudioClipPromise[] voDruidCheer = AudioClipPromise.BuildPromises("heroes/sounds/druid", SoundIds.voDruidCheerPaths);

	public AudioClipPromise[] voDruidUpgradeItem = AudioClipPromise.BuildPromises(5);

	public AudioClipPromise[] voDruidSelected = AudioClipPromise.BuildPromises(3);

	public AudioClip[] voGreenManEvolveCommon;

	public AudioClip[] voGreenManEvolveUncommon;

	public AudioClip[] voGreenManEvolveRare;

	public AudioClip[] voGreenManEvolveEpic;

	public AudioClip[] voGreenManEvolveLegendary;

	public AudioClip[] voGreenManEvolveMythical;

	public AudioClip[] voGreenManPrestige;

	public AudioClipPromise[] voGreenManTimeChallengeComplete = AudioClipPromise.BuildPromises("sounds/timechallenge-mode", SoundIds.VoGreenManTimeChallengeCompletePaths);

	public AudioClipPromise[] voGreenManTimeChallengeFail = AudioClipPromise.BuildPromises("sounds/timechallenge-mode", SoundIds.VoGreenManTimeChallengeFailPaths);

	public AudioClipPromise voGreenManTimeChallengeUnlock = new AudioClipPromise("sounds/timechallenge-mode", "Assets/sound/vo/greenMan/vo_greenman_unlock_tc.wav");

	public AudioClipPromise voGreenManFirstWelcome = new AudioClipPromise("sounds/greenman-tutorial", "Assets/sound/vo/greenMan/Greenman_phrase_1.wav");

	public AudioClipPromise voGreenManFirstRingOffer = new AudioClipPromise("sounds/greenman-tutorial", "Assets/sound/vo/greenMan/Greenman_phrase_2.wav");

	public AudioClipPromise voGreenManFirstRingClaimed = new AudioClipPromise("sounds/greenman-tutorial", "Assets/sound/vo/greenMan/Greenman_phrase_3.wav");

	public AudioClipPromise voGreenManFirstHeroesTabUnlock = new AudioClipPromise("sounds/greenman-tutorial", "Assets/sound/vo/greenMan/Greenman_phrase_4.wav");

	public AudioClipPromise voGreenManFirstRingUpgrade = new AudioClipPromise("sounds/greenman-tutorial", "Assets/sound/vo/greenMan/Greenman_phrase_5.wav");

	public AudioClipPromise voGreenManFirstRingUpgradeDone = new AudioClipPromise("sounds/greenman-tutorial", "Assets/sound/vo/greenMan/Greenman_phrase_6.wav");

	public AudioClipPromise[] voRiftComplete = AudioClipPromise.BuildPromises("sounds/rift-mode", SoundIds.VoRiftCompletePaths);

	public AudioClipPromise[] voRiftFail = AudioClipPromise.BuildPromises("sounds/rift-mode", SoundIds.VoRiftFailPaths);

	public AudioClipPromise voWiseSnakeUnlockRift = new AudioClipPromise("sounds/rift-mode", "Assets/sound/vo/wisesnake/WISESNAKE_GOG_UNLOCK.wav");

	public AudioClip[] voWiseSnakeSpawn;

	public AudioClip[] voWiseSnakeSummonMinions;

	public AudioClip[] voWiseSnakeScape;

	public AudioClip[] voWiseSnakeDeath;

	public AudioClip[] voMerchantUseItem;

	public AudioClip[] voMerchantUseItemLast;

	public AudioClip[] voAlchemistSlotUnlock;

	public AudioClip[] voAlchemistCraft;

	public SoundArchieve.ArtifactRaritySounds[] voAlchemistEvolve;

	public AudioClip voAlchemistOverhaul;

	public AudioClip[] banditSpawns;

	public AudioClip[] banditDeaths;

	public AudioClip[] banditDamages;

	public AudioClip[] humanSemiCorruptedSpawns;

	public AudioClip[] humanSemiCorruptedDeaths;

	public AudioClip[] humanSemiCorruptedDamages;

	public AudioClip[] humanCorruptedSpawns;

	public AudioClip[] humanCorruptedDeaths;

	public AudioClip[] humanCorruptedDamages;

	public AudioClip[] batSpawns;

	public AudioClip[] batDeaths;

	public AudioClip[] batDamages;

	public AudioClip[] spiderSpawns;

	public AudioClip[] spiderDeaths;

	public AudioClip[] spiderDamages;

	public AudioClip[] magoliesSpawns;

	public AudioClip[] magoliesDeaths;

	public AudioClip[] magoliesDamages;

	public AudioClip[] dwarfSpawns;

	public AudioClip[] dwarfDeaths;

	public AudioClip[] dwarfDamages;

	public AudioClip[] dwarfBossSpawns;

	public AudioClip[] dwarfBossDeaths;

	public AudioClip[] dwarfBossDamages;

	public AudioClip[] wolfSpawns;

	public AudioClip[] wolfDeaths;

	public AudioClip[] wolfAttacks;

	public AudioClip[] wolfDamages;

	public AudioClip bossSpawn;

	public AudioClip bossDeath;

	public AudioClip[] bossAttacks;

	public AudioClip[] bossDamages;

	public AudioClip[] orcBossSpawns;

	public AudioClip[] orcBossDeaths;

	public AudioClip[] orcBossDamages;

	public AudioClip[] banditBossSpawns;

	public AudioClip[] banditBossDeaths;

	public AudioClip[] banditBossDamages;

	public AudioClip[] elfBossSpawns;

	public AudioClip[] elfBossDeaths;

	public AudioClip[] elfBossDamages;

	public AudioClip[] elfHalfCorruptedSpawns;

	public AudioClip[] elfHalfCorruptedDeaths;

	public AudioClip[] elfHalfCorruptedAttack;

	public AudioClip[] elfHalfCorruptedDamages;

	public AudioClip[] elfCorruptedSpawns;

	public AudioClip[] elfCorruptedDeaths;

	public AudioClip[] elfCorruptedAttack;

	public AudioClip[] elfCorruptedDamages;

	public AudioClip[] magoliesBossSpawns;

	public AudioClip[] magoliesBossDeaths;

	public AudioClip[] magoliesBossDamages;

	public AudioClip[] snakeSpawns;

	public AudioClip[] snakeAttacks;

	public AudioClip[] snakeDeaths;

	public AudioClip wiseSnakeSpawn;

	public AudioClip[] wiseSnakeAttacks;

	public AudioClip wiseSnakeSummonMinions;

	public AudioClip wiseSnakeCirclePieceBroken;

	public AudioClip wiseSnakeLastCirclePieceBroken;

	public AudioClip wiseSnakeCircleDestroyed;

	public AudioClip wiseSnakeHurt;

	public AudioClip wiseSnakeDeath;

	public AudioClip[] enemyGenericAttacks;

	public AudioClip[] enemyGenericDeaths;

	public AudioClip[] chestSpawns;

	public AudioClip[] chestDeaths;

	public AudioClip[] goldTaps;

	public AudioClip[] goldCollects;

	public AudioClip[] creditsTaps;

	public AudioClip[] creditsCollects;

	public AudioClip[] scrapsTaps;

	public AudioClip[] scrapsCollects;

	public AudioClip[] mythstonesTaps;

	public AudioClip[] mythstonesCollects;

	public AudioClip[] tokensTaps;

	public AudioClip[] tokensCollects;

	public AudioClip ambientForest;

	public AudioClip adDragonLoop;

	public AudioClip adDragonTap;

	public AudioClip adDragonLeave;

	public AudioClip heroEvolveFirstClick;

	public AudioClip heroEvolveMain;

	public AudioClip heroEvolveFinish;

	public AudioClip modeTransition;

	public AudioClip uiMenuUp;

	public AudioClip uiMenuDown;

	public AudioClip uiTabSwitch;

	public AudioClip uiDefaultFailClick;

	public AudioClip uiTransitionAppear;

	public AudioClip uiTransitionDisappear;

	public AudioClip uiOpenMenu;

	public AudioClip uiOpenTabClick;

	public AudioClip uiPopupAppear;

	public AudioClip uiPopupDisappear;

	public AudioClip uiRingSelect;

	public AudioClip uiMenuBack;

	public AudioClip uiToggleOn;

	public AudioClip uiToggleOff;

	public AudioClip uiUnlockPopup;

	public AudioClip uiPurchaseGemPack;

	public AudioClip uiPrestigeSelected;

	public AudioClip uiPrestigeActivated;

	public AudioClip uiSkillSelect;

	public AudioClip uiInteractModeButton;

	[Header("Merchant Items")]
	public AudioClip merchantItemAutoTap;

	public AudioClip merchantItemGoldBag;

	public AudioClip merchantItemPowerUp;

	public AudioClip merchantItemTimeWarp;

	public AudioClip merchantItemRefresherOrb;

	[Header("Artifacts UI")]
	public AudioClip uiArtifactCraft;

	public AudioClip uiArtifactCraftTableFall;

	public AudioClip uiArtifactEvolve;

	public AudioClip uiArtifactReroll;

	public AudioClip uiArtifactSelect;

	public AudioClip uiArtifactSlotSelect;

	public AudioClip uiArtifactSlotUnlock;

	public AudioClip uiArtifactConversion;

	public AudioClip uiArtifactConversionLoop;

	public AudioClip uiArtifactAppearConversionLoop;

	public AudioClip uiArtifactAppearConversionSingle;

	public AudioClip uiNewTALMilestoneReached;

	public AudioClip uiLootpackTap;

	public AudioClip[] uiLootpackSelect;

	public AudioClip[] uiLootpackOpen;

	public AudioClip uiLootpackCurrency;

	public AudioClip uiLootpackRune;

	public AudioClip[] uiLootpackItem;

	public AudioClip uiHeroUpgrade;

	public AudioClip uiHeroLevelUp;

	public AudioClip uiWorldUpgrade;

	public AudioClip uiNewHeroBuy;

	public AudioClip uiTimeChallengeCountdown;

	public AudioClip uiDuck;

	public AudioClip uiActivateSkill;

	public AudioClip uiLeaveBoss;

	public AudioClip uiFillAeonDustBarComplete;

	public AudioClip uiFillAeonDustBarProgress;

	public AudioClip uiButtonDiscoverNewGates;

	public AudioClip uiFlashOfferPurchase;

	public AudioClip uiCharmPackOpening;

	public AudioClip uiCharmPackShake;

	public AudioClip uiCharmPackDrop;

	public AudioClip[] uiCharmChoiceEnter;

	public AudioClip[] uiCharmChoiceTurn;

	public AudioClip uiCharmSelected;

	public AudioClip charmTriggered;

	public AudioClip charmSelectionAvailable;

	public AudioClip curseAppear;

	public AudioClip curseDispel;

	public AudioClip musicMenu;

	public AudioClip musicStandard;

	public AudioClip musicStandardBoss;

	public AudioClip musicTimeChallenge;

	public AudioClip[] earthRingMeteor;

	public AudioClip[] earthRingMeteorImpact;

	public AudioClip[] earthRingTap;

	public AudioClip[] earthRingTapDisabled;

	public AudioClip uiTrinketCrafting;

	public AudioClip uiTrinketDisenchant;

	public AudioClip uiTrinketEffectButtonClick;

	public AudioClip[] ornamentDropImpacts;

	public AudioClip blizzardStartSound;

	public AudioClip treeFullyPurchasedAnim;

	public AudioClip[] soundChristmasTreeStrings;

	public AudioClip[] soundChristmasTreeBalls;

	public AudioClip screenshot;

	private Dictionary<string, bool> inGameSoundBundlesLoaded = new Dictionary<string, bool>();

	private Dictionary<string, bool> uiSoundBundlesLoaded = new Dictionary<string, bool>();

	private List<SoundArchieve.SoundLoader> soundLoaders = new List<SoundArchieve.SoundLoader>();

	[Serializable]
	public class ArtifactRaritySounds
	{
		public AudioClip[] Clips;
	}

	public class SoundLoader
	{
		public string SoundPath;

		public string BundleId;

		public Action<AudioClip> OnLoaded;
	}
}
