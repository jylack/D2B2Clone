public static class ScDefine
{
    public static ScLayer Layer { get; private set; } = new();
    public static ScInteractionLayer InteractionLayer { get; private set; } = new();



    public enum ScScene
    {
        InitSettings = 0,
        TutorialPlayerSettings,
        TutorialCharacterSelection,
        TutorialPlay,
        Ch1Login,
        Ch1Play,        
        Ch2Login,
        Ch2Play,
        Ch2BlindSpot,
        Ch3Login,
        Ch3Room,
        Ch3Play,
        LookAroundGuide,
        HandUpGuide,
        SG01_SafetyLine,
        SG04_TrafficBlink,
        SG05_RedLightViolation,
        SG06_Jaywalking,
        Sg06_BlindSpot,
        Sg02_LookAround,
        Sg03_HandUp,
        Sg07_GsCarPrediction
    }

    public enum ScNickNameValidation
    {
        None = 0,
        Empty,
        LessThan2Char,
        IncompleteHangul,
    }

    public enum ScHeadTurn
    {
        None = 0,
        Forward,
        Left,
        Right
    }

    public enum ScGuideCharacter
    {
        None = 0,
        Character1,
        Character2,
        Character3,
        Character4,
    }

    public enum ScTrafficLightType
    {
        None = 0,
        Red,
        Green,
    }
}
