public class ScInteractionLayer
{
    private const int MirrorIdx = 1;
    private const int NpcIdx = 2;

    public int MirrorIndex { get; private set; } = MirrorIdx;
    public int NpcIndex { get; private set; } = NpcIdx;

    public int MirrorMask { get; private set; } = Pow(MirrorIdx);
    public int NpcMask { get; private set; } = Pow(NpcIdx);



    private static int Pow(int power) => ScUtils.Power(2, power);
}
