using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ChallengeSystem
{
    [System.Serializable]
    public class Achievement
    {
        public Achievement(int id, string name, bool isCompleted)
        {
            this.id = id;
            this.name = name;
            this.isCompleted = isCompleted;
        }
        //ID is the placement in the list
        public int id;
        public string name;
        public bool isCompleted = false;
    }

    [System.Serializable]
    public class Challenge
    {
        public Challenge(int id, string name, int completeCount, int counter, bool resetCounterOnLoad, bool achievementOnly, bool isCompleted, Achievement linkAchieve)
        {
            this.id = id;
            this.name = name;
            this.completeCount = completeCount;
            this.resetCounterOnLoad = resetCounterOnLoad;
            this.counter = counter;
            this.achievementOnly = achievementOnly;
            this.isCompleted = isCompleted;
            this.linkAchieve = linkAchieve;
        }
        //ID is the placement in the list
        public int id;
        public string name;
        public int completeCount;
        public bool resetCounterOnLoad;
        public bool achievementOnly;
        public int counter = 0;
        public bool isCompleted = false;
        public Achievement linkAchieve = null;
    }
    static public Achievement GetAchievement(string name, List<Achievement> list)
    {
        if (name.Equals("null"))
        {
            return new Achievement(-1, "null", false);
        }
        foreach(Achievement item in list)
        {
            if (item.name.Equals(name)) 
            {
                Debug.Log("Returning " + item.name);
                return item;
            }
        }

        return null;
    }

    static public Challenge getChallenge(string challengeName, List<Challenge> list)
    {
        foreach (Challenge c in list)
        {
            if (c.name.Equals(challengeName))
            {
                Debug.Log("Challenge Got");
                return c;
            }
        }
        return null;
    }

    static public void setAchievementValues(Achievement a, int id, string name, bool completed)
    {
        a.id = id;
        a.name = name;
        a.isCompleted = completed;
    }

    static public void setChallengeValues(Challenge c, int completeCount, bool completed, Achievement linkA)
    {
        c.completeCount = completeCount;
        c.isCompleted = completed;
        c.linkAchieve = linkA;
    }


    //Add a certain value to a challenge counter
    static public void incrementChallengeCounter(Challenge challenge, int value)
    {
        challenge.counter += value;
        checkChallengeComplete(challenge);
    }

    static public void checkChallengeComplete(Challenge challenge)
    {
        //If the player has cleared enough of a challenge, complete the challenge
        if (challenge.counter >= challenge.completeCount)
        {
            challenge.isCompleted = true;
            //Unlock associated achievement if there is one
            if (challenge.linkAchieve != null)
            {
                unlockAchievement(challenge.linkAchieve);
            }
        }
    }
    static public void unlockAchievement(Achievement a)
    {
        //Check first to see that the achievement is not already complete
        GameManager manager = GameObject.FindObjectOfType<GameManager>();
        if (!GetAchievement(a.name, manager.achievementList).isCompleted)
        {
            a.isCompleted = true;
            AchievementPanel panel = GameObject.FindObjectOfType<AchievementPanel>();
            panel.setInfo(a.name);
            panel.TriggerAnimation();

            SaveDataReader.SaveData();

        }
    }

    static public bool checkChallengeIsCompleted(Challenge challenge)
    {
        if (challenge.isCompleted)
        {
            return true;
        }
        return false;
    }

    //Wrapper function for increasing challenges
    static public void progressChallenge(string name, List<Challenge> fullList, int progressAmount)
    {
        incrementChallengeCounter(getChallenge(name, fullList), progressAmount);
    }

    static public bool searchListForChallenge(string name, List<Challenge> list)
    {
        return list.Contains(getChallenge(name, list));
    }



}
