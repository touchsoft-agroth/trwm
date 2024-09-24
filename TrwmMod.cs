using MelonLoader;
using trwm;
using trwm.Source.Game;
using trwm.Source.Logging;
using trwm.Source.Modes;

[assembly: MelonInfo(typeof(TrwmMod), ModData.NameLong, ModData.VersionString, ModData.Author)]
[assembly: MelonGame(ModData.TargetGame, ModData.TargetGame)]
namespace trwm
{
    public class TrwmMod : MelonMod
    {
        public static bool CanBeInitialized { private get; set; }
        
        private ModeController _modeController;

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
            var gameGateway = new GameGateway(droneController, windowManager);

            _modeController = new ModeController(gameGateway);
            // this should be done in mode controller
            _modeController.Register(ModeType.Drone, new DroneMode());
            _modeController.Register(ModeType.Window, new WindowMode());
            _modeController.Register(ModeType.DroneEntityPlacement, new DroneEntityPlacementMode());

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
        }

        public override void OnGUI()
        {
        }

        private void CustomInit()
        {
            var droneController = new DroneController();
            var windowManager = new WindowManager();
            var gameGateway = new GameGateway(droneController, windowManager);

            _modeController = new ModeController(gameGateway);
            _modeController.Register(ModeType.Drone, new DroneMode());
            _modeController.Register(ModeType.Window, new WindowMode());

            _isInitialized = true;
        }
    }
}