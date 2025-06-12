public class ScLanguageData
{
    public string Key { get; }
    public bool UseTTS { get; }
    public string Text => Manager.Instance.LanguageMgr.GetText(Key);
    
    
    
    public ScLanguageData(string key, bool useTTS)
    {
        Key = key;
        UseTTS = useTTS;
    }
    
    
    
    public void PlayVoice()
    {
        if (!UseTTS)
            return;
        
        Manager.Instance.SoundMgr.PlayVoice(Key);
    }
}