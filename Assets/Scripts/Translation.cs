using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Translation : MonoBehaviour {
    public static readonly SystemLanguage[] LANGUAGES = {
        SystemLanguage.English, SystemLanguage.French, SystemLanguage.Japanese
    };

    private static Dictionary<string, string> trad = null;
    private static SystemLanguage lang;

#if UNITY_EDITOR
    private static bool d_OverrideLanguage = false;
    private static SystemLanguage d_Language = SystemLanguage.Japanese;
#endif

    private static void CheckInstance() {
        // Already Initialized ?
        if(trad != null) { return; }

        trad = new Dictionary<string, string>();
        var langBuf = Application.systemLanguage;

#if UNITY_EDITOR
        // Override the current language for testing purpose.
        if (d_OverrideLanguage) { langBuf = d_Language; }
#endif
        // Check if the current language is supported.
        // Otherwise use the first language as default.
        if (Array.IndexOf<SystemLanguage>(LANGUAGES, langBuf) == -1)
        { lang = LANGUAGES[0]; }

        // Load and parse the translation file from the Resources folder.
        var data = Resources.Load<TextAsset>($"Lang/{langBuf}");
        if (data != null) { ParseFile(data.text); }
    }

    // Returns the translation for this key.
    public static string Get(string key)
    {
        CheckInstance();
        if (trad.ContainsKey(key)) { return trad[key]; }

#if UNITY_EDITOR
        Debug.Log($"The key {key} is missing");
#endif

        return key;
    }

    public static void ParseFile(string data)
    {
        using (var stream = new StringReader(data)) {
            var line = stream.ReadLine();
            var temp = new string[2];
            var key = string.Empty;
            var value = string.Empty;

            while (line != null) {
                if (line.StartsWith(";") || line.StartsWith("[")) {
                    line = stream.ReadLine();
                    continue;
                }

                temp = line.Split('=');
                if (temp.Length == 2) {
                    key = temp[0].Trim();
                    value = temp[1].Trim();

                    if (value == string.Empty) { continue; }
                    if (trad.ContainsKey(key)) { trad[key] = value; }
                    else { trad.Add(key, value); }
                }
                
                line = stream.ReadLine();
            }
        }
    }

    //public static void setLanguage() {}

    public static SystemLanguage GetLanguage() { return lang; }
}
