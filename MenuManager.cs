using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public List<ChallengeSystem.Achievement> achievementList;
    public GameObject AchieveScrollViewContent;
    public List<GameObject> achievementPanels;

    private void Awake()
    {
        SaveDataReader.LoadData(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform panel in AchieveScrollViewContent.transform)
        {
            achievementPanels.Add(panel.gameObject);
        }

        for (int i = 0; i < achievementList.Count; i++)
        {
            //Get the text child of each panel gameObject
            foreach (Transform child in achievementPanels[i].transform)
            {
                child.GetComponent<Text>().text = achievementList[i].name;
                if (!achievementList[i].isCompleted)
                {
                    child.GetComponent<Text>().color = new Color(0.5283019f, 0.5283019f, 0.5283019f);
                }
                else
                {
                    child.GetComponent<Text>().color = new Color(0.0f, 1.0f, 0.0f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
