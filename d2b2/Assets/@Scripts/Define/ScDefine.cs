public static class ScDefine
{
    public static ScLayer Layer { get; private set; } = new();
    public static ScInteractionLayer InteractionLayer { get; private set; } = new();



    public enum ScScene
    {
        InitSettings = 0,
        TutorialInitial,
        TutorialMove,
        TutorialCrosswalk,
        TestTutorialInitial,
        TestTutorialMove,
        TestTutorialCrosswalk,
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
        Sg01_SafetyLine,
        Sg02_LookAround,
        Sg03_HandUp,
        Sg04_TrafficBlink,
        Sg05_SafeWalk, 
        Sg06_BlindSpot,
        Sg07_GsCarPrediction,
        Sg08_Jaywalking,

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
        Character1 = 0,
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

    public enum ScNpcAnimState
    {
        Idle = 0,
        Walking,
        Running,
        LookAround,
        StandingUsingPhone,
        WalkingUsingPhone,
    }

    public enum ScPathPointNextAction
    {
        None,
        Move,
        Crosswalk,
        LookAround,
        DoBadThing,
        Destroy,
    }
}
