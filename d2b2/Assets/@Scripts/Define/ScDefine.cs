public static class ScDefine
{
    public static ScLayer Layer { get; private set; } = new ScLayer();
    public static ScInteractionLayer InteractionLayer { get; private set; } = new ScInteractionLayer();



    public enum ScScene
    {
        InitSettings,
        TutorialPlayerSettings,
        TutorialCharacterSelection,
        TutorialPlay,
        Ch1Login,
        Ch1Play,
        Ch2Login,
        Ch2Play,
        Ch3Login,
        Ch3Room,
        Ch3Play,
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
        None,
        Forward,
        Left,
        Right
    }
}