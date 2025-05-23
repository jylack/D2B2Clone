public static class ScCh3Define
{
    public const string PROP_KEY_IS_READY           = "IsReady";
    public const string PROP_KEY_CHARACTER_INDEX    = "CharacterIndex";
    public const string PROP_KEY_LEVEL_LOADED       = "LevelLoaded";

    public const int MaxPlayerCount = 6; 
    public static ScCh3PhotonCustomPropertyKey PhotonCustomPropKeys { get; private set; } = new();
}