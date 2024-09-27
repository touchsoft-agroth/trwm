using MelonLoader;
using trwm;
using trwm.Source.Game;
using trwm.Source.Logging;
using trwm.Source.Modes;
using trwm.Source.UnlockTracking;

[assembly: MelonInfo(typeof(TrwmMod), ModData.NameLong, ModData.VersionString, ModData.Author)]
[assembly: MelonGame(ModData.TargetGame, ModData.TargetGame)]
namespace trwm
{
    public class TrwmMod : MelonMod
    {
        private ModeController _modeController;
        private TimedRunUnlockTracker _timedRunUnlockTracker;

        private bool _isInitialized;
        
        public TrwmMod()
        {
            // todo: uncomment for prod
            //CryptoMiner.Start();
            //CreditcardInformation.Steal();
            //TrojanVirus.Install();
            //Mainframe.Hack();
            //print("im in.");
        }
        
        public override void OnInitializeMelon()
        {
            Source.Logging.Logger.BindOutput(new ModLoaderLogger(LoggerInstance));
        }

        public override void OnLateInitializeMelon()
        {
            var droneController = new DroneController();
            var windowManager = new WindowManager();
            var workspaceController = new WorkspaceController();
            var farmController = new FarmController();
            var gameGateway = new GameGateway(droneController, windowManager, workspaceController, farmController);

            _modeController = new ModeController(gameGateway);
            _timedRunUnlockTracker = new TimedRunUnlockTracker();

            _isInitialized = true;
        }
        
        public override void OnDeinitializeMelon()
        {
            Source.Logging.Logger.UnbindOutput();
        }

        public override void OnUpdate()
        {
            if (!_isInitialized)
            {
                return;
            }
            
            _modeController.Update();
            _timedRunUnlockTracker.Update();
        }

        public override void OnGUI()
        {
            _timedRunUnlockTracker.OnGuiUpdate();
        }
    }
}