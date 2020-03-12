using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool devSaveTest = false;
    //CHALLENGES
    //[HideInInspector]
    public List<ChallengeSystem.Challenge> challengeList;
    //ACHIEVEMENTS
    //[HideInInspector]
    public List<ChallengeSystem.Achievement> achievementList;
    //Challenge set the player has to have. 
    public List<ChallengeSystem.Challenge> activeChallenges;

    [System.Serializable]
    public class QuestPanel
    {
        public GameObject panel;
        public List<Text> challenges;
        public List<Image> chalCheckBox;
    }
    public QuestPanel questPanel;

    private void Awake()
    {
        SaveDataReader.LoadData(this);
        generateChallenges(challengeList, activeChallenges, 3);
    }

    /*////////////////////////////
     * Use This when linking an achievement of an action
     * 
     * ChallengeSystem.progressChallenge(<Name Of Challenge>, challengeList, 1);
            if (ChallengeSystem.searchListForChallenge(<Name Of Challenge>, challengeList))
            {
                checkChallengeComplete(<Name Of Challenge>, challengeList);
            }
     * //////////////////////
     */

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChallengeSystem.progressChallenge("Eat Ten Clothes", challengeList, 1);

            if (ChallengeSystem.searchListForChallenge("Eat Ten Clothes", challengeList))
            {
                checkChallengeComplete("Eat Ten Clothes", challengeList);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("Saving Game");
            SaveDataReader.SaveData();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void toggleQuestPanel()
    {
        if (questPanel.panel.activeInHierarchy)
        {
            questPanel.panel.SetActive(false);
        }
        else
        {
            questPanel.panel.SetActive(true);
        }
    }

    public void generateChallenges(List<ChallengeSystem.Challenge> fullList, List<ChallengeSystem.Challenge> listToFill, int maxChallenges)
    {
        List<ChallengeSystem.Challenge> temp = new List<ChallengeSystem.Challenge>();
        for (int i = 0; i < fullList.Count; i++)
        {
            if (!fullList[i].achievementOnly)
            {
                temp.Add(fullList[i]);
            }
        }
        
        for (int i = 0; i < maxChallenges; i++)
        {
            int index = Random.Range(0, temp.Count);
            ChallengeSystem.Challenge chalToAdd = temp[index];
            listToFill.Add(chalToAdd);
            questPanel.challenges[i].text = chalToAdd.name;
            temp.RemoveAt(index);           
        }
        
    }

    public void checkChallengeComplete(string name, List<ChallengeSystem.Challenge> list)
    {
        //Check if a challenge is completed
        ChallengeSystem.Challenge challenge = ChallengeSystem.getChallenge(name, list);
        if (ChallengeSystem.checkChallengeIsCompleted(challenge))
        {
            //Check to see if the challenge is in the players active challenge list
            if (activeChallenges.Contains(challenge))
            {
                questPanel.chalCheckBox[activeChallenges.IndexOf(challenge)].gameObject.SetActive(true);
            }
        }
    }

}
