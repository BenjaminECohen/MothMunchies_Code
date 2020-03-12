using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class SaveDataReader
{
    //YOU MUST LOAD ALL ACHIEVEMENTS FIRST

    public static void LoadData(GameManager manager)
    {
        string path = Path.Combine(Application.dataPath, @"saveData.txt");
        path = Path.GetFullPath(path);
        StreamReader sr = new StreamReader(path, true);
        string file = sr.ReadToEnd();
        string[] lines = file.Split(System.Environment.NewLine.ToCharArray());

        foreach (string line in lines)
        {
            if(!string.IsNullOrEmpty(line))
            {
                int flag = line.IndexOf(':');
                if(line.Substring(0, flag).Equals("c"))
                {
                    Debug.Log("Loading Challenge");
                    string[] fields = line.Split(':');
                    manager.challengeList.Add(new ChallengeSystem.Challenge(manager.challengeList.Count, fields[1], int.Parse(fields[2]),
                        int.Parse(fields[3]), bool.Parse(fields[4]), bool.Parse(fields[5]), bool.Parse(fields[6]), 
                        ChallengeSystem.GetAchievement(fields[7], manager.achievementList)));
                }
                else if (line.Substring(0, flag).Equals("a"))
                {
                    Debug.Log("Loading Achievement");
                    //3 Sections
                    string[] fields = line.Split(':');
                    manager.achievementList.Add(new ChallengeSystem.Achievement(manager.achievementList.Count, fields[1], bool.Parse(fields[2])));
                }
                else
                {
                    //x is a viable flag and is used for testing purposes. It never generates anything
                    if (!line.Substring(0, flag).Equals("x"))
                    {
                        Debug.Log("Bad Save Data: Bad Flag " + line.Substring(0, flag) + " is not an allowed flag");
                    }
                }
            }
        }
        sr.Close();
    }

    public static void SaveData()
    {   
        string path = Path.Combine(Application.dataPath, @"saveData.txt");
        path = Path.GetFullPath(path);
        
        Debug.Log(path);

        File.WriteAllText(path, string.Empty);

        GameManager manager = GameObject.FindObjectOfType<GameManager>();

        List<ChallengeSystem.Achievement> aList = manager.achievementList;
        List<ChallengeSystem.Challenge> cList = manager.challengeList;

        if (manager.devSaveTest)
        {
            File.AppendAllText(path, "x: Achievement Name : Is Completed\n" +
                "x: Challenge Name : Max Count : Counter : Reset Counter on Load : Achievement Only : Is Completed : Name of Linked Achievement\n\n");
        }
        
        foreach (ChallengeSystem.Achievement a in aList)
        {
            File.AppendAllText(path, "a:" + a.name + ":" + a.isCompleted + "\n");
        }
        foreach (ChallengeSystem.Challenge c in cList)
        {
            int count = 0;
            bool isCompleted = false;
            if (!c.resetCounterOnLoad)
            { 
                count = c.counter;
            }
            if (c.achievementOnly)
            {
                isCompleted = c.isCompleted;
            }
            File.AppendAllText(path, "c:" + c.name + ":" + c.completeCount + ":" + count + ":" + c.resetCounterOnLoad + ":" + c.achievementOnly +":" + isCompleted + ":" + c.linkAchieve.name + "\n");
        }

    }
}
