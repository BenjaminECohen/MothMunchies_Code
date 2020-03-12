using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class MothSkins
{

    public class Skins
    {
        public Skins(string name, GameObject skin, bool isObtained)
        {
            this.name = name;
            this.skin = skin;
            this.isObtained = isObtained;
        }
        public string name;
        public GameObject skin;
        public bool isObtained;

    }

    static public GameObject getSkinByName(string name, List<GameObject> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].name.Equals(name))
            {
                return list[i];
            }
        }
        Debug.Log("No Such Skin Exists");
        return null;
    }

}
