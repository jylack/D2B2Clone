public class ScInteractionLayer
{
    private const int mirrorIdx = 1;
    private const int npcIdx = 2;

    public int MirrorIndex { get; private set; } = mirrorIdx;
    public int NpcIndex { get; private set; } = npcIdx;

    public int MirrorMask { get; private set; } = Pow(mirrorIdx);
    public int NpcMask { get; private set; } = Pow(npcIdx);



    private static int Pow(int power) => ScUtils.Power(2, power);
}
