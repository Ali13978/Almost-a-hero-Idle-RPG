using System;
using System.Text;
using UnityEngine;


public static class InstantUtility
{

    public static CookiePayload GetCookiePlayload()
    {

        return InstantUtility.DebugLoad();
    }


 

   
 
    public static CookiePayload DebugLoad()
    {
        return new CookiePayload
        {
            coinAmount = 250.0,
            stageIndex = 5,
            waveIndex = 0,
            tutorialStep = 21,
            heroXp = 2,
            totalTaps = 100,
            ringLevel = 0,
            ringXp = 5,
            initialzedCookie = true,
            upgradedSkillIndex = 0,
            tutorialQuestIndex = 0,
            tutorialQuestProgress = 0.0
        };
    }
}
