using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonData
{
    public List<MonsterData> datas = new List<MonsterData>();
}
[Serializable]
public class MonsterData
{
    public string name;
    public float x;
    public float y;
    public float z;
    public MonsterType type;

    public MonsterData(string name, float x, float y, float z, MonsterType type)
    {
        this.name = name;
        this.x = x;
        this.y = y;
        this.z = z;
        this.type = type;
    }
}

public class MonsterCfg
{
    static MonsterCfg _instacn;
    public static MonsterCfg Instance
    {
        get
        {
            if (_instacn == null)
            {
                _instacn = new MonsterCfg();
                _instacn.Init();
            }
            return _instacn;
        }
    }
    public JsonData data;
    string path;

    void Init()
    {
        path = Application.dataPath + "/monster.json";
        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<JsonData>(json);
        //Debug.Log(json);
    }
    public JsonData GetJsonDate()
    {
        return data;
    }
}

