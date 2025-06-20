public class ScLobbyPlayerEntity
{
    public string nickname;
    public int actorNumber;
    public ScDefine.ScGuideCharacter guideCharacterType;
    public int positionIndex;



    public ScLobbyPlayerEntity() { }

    public ScLobbyPlayerEntity(string name, int actorNum, ScDefine.ScGuideCharacter characterType, int posIdx)
    {
        nickname = name;
        actorNumber = actorNum;
        guideCharacterType = characterType;
        positionIndex = posIdx;
    }
}