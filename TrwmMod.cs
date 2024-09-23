
using MelonLoader;
using trwm.Source;

namespace trwm
{
    public class TrwmMod : MelonMod
    {
        private readonly ManualDroneController _manualDroneController;
        private readonly GameUtils _gameUtils;
        private ScriptRunner _scriptRunner;
        private WindowNavigator _windowNavigator;
        private CameraMover _cameraMover;

        public TrwmMod()
        {
            _manualDroneController = new ManualDroneController();
            _gameUtils = new GameUtils();
            _windowNavigator = new WindowNavigator();
            _cameraMover = new CameraMover();
        }
        
        public override void OnInitializeMelon()
        {
            _scriptRunner = new ScriptRunner(LoggerInstance);
            _windowNavigator.Initialize(LoggerInstance);
            LoggerInstance.Msg("TRWM loaded");
        }

        public override void OnUpdate()
        {
            _manualDroneController.Update();
            if (_manualDroneController.Active)
            {
                return;
            }
            
            _gameUtils.Update();
            _scriptRunner.Update();
            _windowNavigator.Update();
            _cameraMover.Update();
        }

        public override void OnGUI()
        {
            _manualDroneController.UpdateGui();
            _scriptRunner.GuiUpdate();
        }
    }
}