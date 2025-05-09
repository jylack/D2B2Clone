public class ScLayer
{
    private const int mirrorIdx = 3;
    private const int npcIdx = 6;

    public int MirrorIndex { get; private set; } = mirrorIdx;
    public int NpcIndex { get; private set; } = npcIdx;

    public int MirrorMask { get; private set; } = Pow(mirrorIdx);
    public int NpcMask { get; private set; } = Pow(npcIdx);



    private static int Pow(int power) => ScUtils.Power(2, power);
}