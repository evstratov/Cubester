using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static int SelectedLanguage { get; private set; }

    public const string SCORE_KEY = "Score_Key";
    public const string TAP_TO_CONTINUE_KEY = "TapToContinue_Key";
    public const string SIDE_SWIPE_KEY = "SideSwipe_Key";
    public const string VERTICAL_SWIPE_KEY = "VerticalSwipe_Key";
    public const string HURRY_UP_KEY = "HurryUp_Key";

    public static event LanguageChangeHandler OnLanguageChange;
    public delegate void LanguageChangeHandler();

    private static Dictionary<string, List<string>> localization;

    [SerializeField]
    private TextAsset textFile;

    private void Awake()
    {
        // TODO: load language id
        SelectedLanguage = 0;
        if (localization == null)
            LoadLocalization();
    }

    public void SetLanguage(int id)
    {
        SelectedLanguage = id;
        OnLanguageChange?.Invoke();
    }

    private void LoadLocalization()
    {
        localization = new Dictionary<string, List<string>>();

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textFile.text);

        foreach (XmlNode key in xmlDocument["Keys"].ChildNodes)
        {
            string keyStr = key.Attributes["Name"].Value;

            var values = new List<string>();
            foreach (XmlNode translate in key["Translates"].ChildNodes)
                values.Add(translate.InnerText);

            localization[keyStr] = values;
        }
    }

    public static string GetTranslate(string key, int languageId = -1)
    {
        if (languageId == -1)
            languageId = SelectedLanguage;

        if (localization.ContainsKey(key))
            return localization[key][languageId];

        return key;
    }
}
