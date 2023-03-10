using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlGame;
using System.IO;
using Newtonsoft.Json;

public class GameData :Singleton<GameData>
{
    public Dictionary<string, List<SkillXml>> AllRoleSkillList = new Dictionary<string, List<SkillXml>>();

    public void InitByRoleName(string roleName)
    {
        if(File.Exists("Assets/"+roleName+".txt"))
        {
            string str = File.ReadAllText("Assets/" + roleName + ".txt");
            List<SkillXml> skills=JsonConvert.DeserializeObject<List<SkillXml>>(str);
            AllRoleSkillList.Add(roleName, skills);
        }
    }

    public List<SkillXml> GetSkillsByRoleName(string roleName)
    {
        if(AllRoleSkillList.ContainsKey(roleName))
        {
            return AllRoleSkillList[roleName];
        }
        return null;


    }

}
