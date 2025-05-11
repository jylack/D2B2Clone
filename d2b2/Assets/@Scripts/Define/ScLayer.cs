public class ScLayer
{
    private const int MirrorIdx = 3;
    private const int NpcIdx = 6;

    public int MirrorIndex { get; private set; } = MirrorIdx;
    public int NpcIndex { get; private set; } = NpcIdx;

    public int MirrorMask { get; private set; } = Pow(MirrorIdx);
    public int NpcMask { get; private set; } = Pow(NpcIdx);



    private static int Pow(int power) => ScUtils.Power(2, power);
}