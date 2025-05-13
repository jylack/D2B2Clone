public class ScLayer
{
    private const int MirrorIdx = 3;
    private const int NpcIdx = 6;
    private const int PlayerIdx = 7;
    private const int WallIdx = 8;
    private const int HandUpResionIdx = 9;
    private const int LookAroundResionIdx = 10;

    public int MirrorIndex { get; private set; }    = MirrorIdx;
    public int NpcIndex { get; private set; }       = NpcIdx;
    public int PlayerIndex { get; private set; }    = PlayerIdx;
    public int WallIndex { get; private set; } = WallIdx;
    public int HandUpResionIndex { get; private set; } = HandUpResionIdx;
    public int LookAroundResionIndex { get; private set; } = LookAroundResionIdx;


    public int MirrorMask { get; private set; } = Pow(MirrorIdx);
    public int NpcMask { get; private set; }    = Pow(NpcIdx);
    public int PlayerMask { get; private set; } = Pow(PlayerIdx);
    public int WallMask { get; private set; } = Pow(WallIdx);



    private static int Pow(int power) => ScUtils.Power(2, power);
}