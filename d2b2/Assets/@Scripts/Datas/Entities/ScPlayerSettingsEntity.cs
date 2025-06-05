public class ScPlayerSettingsEntity
{
    public float renderScale; //0f ~ 1f
    public float brightness; //0f ~ 1f

    public int colorWeakMode; //0 ~ 3 to VolumeColors(Enum) int ScColorWeakSetting
    public float colorWeakCompensate; //-100 ~ 100

    public int rotateMode; //0 ~ 2 
    public float rotationAngle; //1 ~ 6
    public float rotationSpeed; //1 ~ 10

    public float masterVolume; //0f ~ 1f
    public float bgmVolume; //0f ~ 1f
    public float voiceVolume; //0f ~ 1f
    public float soundEffectVolume; //0f ~ 1f

    //public bool isOnColorBlindness;
}