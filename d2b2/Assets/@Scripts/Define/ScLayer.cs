public class ScLayer
{
    private const int MirrorIdx = 3;
    private const int NpcIdx = 6;
    private const int PlayerIdx = 7;

    public int MirrorIndex { get; private set; }    = MirrorIdx;
    public int NpcIndex { get; private set; }       = NpcIdx;
    public int PlayerIndex { get; private set; }    = PlayerIdx;

    public int MirrorMask { get; private set; } = Pow(MirrorIdx);
    public int NpcMask { get; private set; }    = Pow(NpcIdx);
    public int PlayerMask { get; private set; } = Pow(PlayerIdx);



    private static int Pow(int power) => ScUtils.Power(2, power);
}