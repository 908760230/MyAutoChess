using UnityEngine;
using NFSDK;
using NFrame;

public enum GAME_MODE
{
    GAME_MODE_NONE,
    SINGLE_PLAYER_MODE,
    TWO_PLAYER_MODE,
    MULTIPLE_PLAYER_MODE
}


public class GameMain : MonoBehaviour
{
    private NFUIModule mUIModule;
    private NFIClassModule mClassModule;
    private NFIKernelModule mKernelModule;
    private NFNetModule mNetModule;
    private NFLogModule mLogModule;
    private NFPluginManager mPluginManager;

    private static GameMain _instance = null;
    private GAME_MODE mGameMode = GAME_MODE.GAME_MODE_NONE;
    public GameStage currentGameStage = GameStage.Preparation;
    //private ConfigXML mConfig = new ConfigXML();
    private void Awake()
    {
        mPluginManager = new NFPluginManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        Debug.Log("Root Start " + Application.platform);
        RenderSettings.fog = false;
        //mConfig.load();

        mPluginManager.Registered(new NFSDKPlugin(mPluginManager));
        mPluginManager.Registered(new NFUIPlugin(mPluginManager));
        mPluginManager.Registered(new NFScenePlugin(mPluginManager));

        mKernelModule = mPluginManager.FindModule<NFIKernelModule>();
        mClassModule = mPluginManager.FindModule<NFIClassModule>();
        mNetModule = mPluginManager.FindModule<NFNetModule>();
        mUIModule = mPluginManager.FindModule<NFUIModule>();
        mLogModule = mPluginManager.FindModule<NFLogModule>();

        //mClassModule.SetDataPath(mConfig.GetDataPath());

        mPluginManager.Awake();
        mPluginManager.Init();
        mPluginManager.AfterInit();

        mNetModule.StartConnect("127.0.0.1", 14001);

        mUIModule.ShowUI<NFUILogin>();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        mPluginManager.Execute();
    }

    private void OnDestroy()
    {
        mPluginManager.BeforeShut();
        mPluginManager.Shut();
        mPluginManager = null;
    }

    public static GameMain Instance()
    {
        return _instance;
    }
    public GAME_MODE GetGameMode()
    {
        return this.mGameMode;
    }
    public void SetGameMode(GAME_MODE mode)
    {
        this.mGameMode = mode;
    }
    public NFIPluginManager GetPluginManager()
    {
        return mPluginManager;
    }
}
