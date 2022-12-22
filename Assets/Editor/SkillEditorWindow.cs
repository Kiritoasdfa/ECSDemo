using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SkillEditorWindow : EditorWindow
{
    class PlayerEditor
    {
        public int _characterIndex = 0;
        public int _folderIndex = 0;
        public string characterName = string.Empty;
        public string folderName = string.Empty;
        //public string characterFilter = string.Empty;
        public List<string> characteList = new List<string>();
        public Player player = null;
    }
    PlayerEditor m_player = new PlayerEditor();
    List<string> m_folderList = new List<string>();
    List<string> m_characterList = new List<string>();
    Dictionary<string, List<string>> m_folderPrefabs = new Dictionary<string, List<string>>();

    string newSkillName = string.Empty;
    SkillWindow skillWindow;

    [MenuItem("Tools/技能编辑器")]
    public static void Init()
    {
        if(Application.isPlaying)
        {
            SkillEditorWindow window = GetWindow<SkillEditorWindow>("编辑器");
            if(window!=null)
            {
                window.Show();
            }
        }
    }


    string GetCharacterPath()
    {
        return Application.dataPath + "/GameDate/Model";
    }

    private void OnEnable()
    {
        DoSearchFolder();
        DoSearchCharacter();
    }

    private void DoSearchFolder()
    {
        m_folderList.Clear();
        m_folderList.Add("all");
        string[] folders = Directory.GetDirectories(GetCharacterPath());
        foreach(var item in folders)
        {
            m_folderList.Add(Path.GetFileName(item));
        }
    }
    private void DoSearchCharacter()
    {
        string[] files = Directory.GetFiles(GetCharacterPath(), "*.prefab", SearchOption.AllDirectories);

        m_characterList.Clear();
        foreach(var item in files)
        {
            m_characterList.Add(Path.GetFileNameWithoutExtension(item));
            //Debug.Log(Path.GetFileNameWithoutExtension(item));
        } 
        m_characterList.Sort();
        m_characterList.Insert(0, "null");
        m_player.characteList.AddRange(m_characterList);
    }

    private void OnGUI()
    {
        int folderIndex = EditorGUILayout.Popup(m_player._folderIndex, m_folderList.ToArray());
        if(folderIndex!=m_player._folderIndex)
        {
            m_player._folderIndex = folderIndex;
            m_player._characterIndex = -1;
            string folderName = m_folderList[m_player._folderIndex];
            List<string> list;
            if (folderName.Equals("all"))
            {
                list = m_characterList;
            }
            else
            {
                if (!m_folderPrefabs.TryGetValue(folderName, out list))
                {
                    list = new List<string>();
                    string[] files = Directory.GetFiles(GetCharacterPath() + "/" + folderName, "*.prefab", SearchOption.AllDirectories);
                    foreach (var item in files)
                    {
                        list.Add(Path.GetFileNameWithoutExtension(item));
                        //Debug.Log( Path.GetFileNameWithoutExtension(item));
                    }
                    m_folderPrefabs[folderName] = list;
                }
            }
            m_player.characteList.Clear();
            m_player.characteList.AddRange(list);
        }
        int characteIndex = EditorGUILayout.Popup(m_player._characterIndex, m_player.characteList.ToArray());
        if (m_player._characterIndex != characteIndex)
        {
            m_player._characterIndex = characteIndex;
            if(m_player.characterName!=m_player.characteList[m_player._characterIndex])
            {
                m_player.characterName = m_player.characteList[m_player._characterIndex];
                    if(m_player.player!=null)
                    {
                        m_player.player.Destroy();
                    }
                    m_player.player = Player.Init(m_player.characterName);
            }
        }

        Vector2 ScrollViewPos = new Vector2(0, 0);
        GUILayout.BeginHorizontal();
        GUILayout.Label("技能名称：");
        newSkillName = GUILayout.TextField(newSkillName);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("创建新的技能"))
        {
            if (!string.IsNullOrEmpty(newSkillName) && m_player.player != null)
            {
                List<SkillBase> skills = m_player.player.AddNewSkill(newSkillName);
                OpenSkillWindow(newSkillName, skills);
                newSkillName = "";
            }
        }
        
        if (m_player.player != null)
        {

            ScrollViewPos = GUILayout.BeginScrollView(ScrollViewPos, false, true);
            foreach (var item in m_player.player.skillsList) 
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(item.Key)) 
                {
                    List<SkillBase> skills = m_player.player.GetSkill(item.Key);
                    foreach (var ite in skills)
                    {
                        ite.Init();
                    }
                    OpenSkillWindow(item.Key, skills);
                }

                GUILayoutOption[] option = new GUILayoutOption[] {
                GUILayout.Width(60),
                GUILayout.Height(19)
                };

                if (GUILayout.Button("删除技能", option))
                {
                    m_player.player.RevSkill(item.Key);
                    break;
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("保存"))
        {
            m_player.player.Save();
            AssetDatabase.Refresh();
        }
        EditorGUILayout.EndHorizontal();

    }
    void OpenSkillWindow(string newSkillName, List<SkillBase> skills)
    {
        if (skills != null)
        {
            if (skillWindow == null)
            {
                skillWindow = EditorWindow.GetWindow<SkillWindow>(newSkillName);
            }
            skillWindow.titleContent = new GUIContent(newSkillName);

            skillWindow.SetInitSkill(skills, m_player.player); 

            skillWindow.Show(); 
            skillWindow.Repaint(); 
        }

    }
    private void OnDestroy()
    {
        if(m_player.player!=null)
        {
            m_player.player.Destroy();
        }
    }
}
