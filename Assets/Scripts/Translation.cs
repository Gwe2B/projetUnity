using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public sealed class Translation : MonoBehaviour {
    public static readonly SystemLanguage[] LANGUAGES = {
        SystemLanguage.English, SystemLanguage.French, SystemLanguage.Japanese
    };

    private static Dictionary<string, string> trad = null;
    private static SystemLanguage lang;

    private static void CheckInstance() {
        if(trad != null) { return; }

        trad = new Dictionary<string, string>();
        var langBuf = Application.systemLanguage;

        // Check if the current language is supported.
        // Otherwise use the first language as default.
        if (Array.IndexOf<SystemLanguage>(LANGUAGES, langBuf) == -1)
        { lang = LANGUAGES[0]; }

        // Load and parse the translation file from the Resources folder.
        var data = Resources.Load<TextAsset>($"Lang/{langBuf}");
        if (data != null) { ParseFile(data.text); }
    }

    public static string Get(string key)
    {
        CheckInstance();
        if (trad.ContainsKey(key)) { return trad[key]; }

#if UNITY_EDITOR
        Debug.Log($"The key {key} is missing");
#endif

        return key;
    }

    private static void ParseFile(string data)
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

    public static SystemLanguage GetLanguage() { return lang; }
}
