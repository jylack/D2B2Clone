public class ScLeftPlayerEntity
{
    public int actorNumber;
    public int[] actorNumbersForPosition;



    public ScLeftPlayerEntity() { }

    public ScLeftPlayerEntity(int actorNum, int[] actorNumsForPos)
    {
        actorNumber = actorNum;
        actorNumbersForPosition = actorNumsForPos;
    }
}