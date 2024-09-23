
using MelonLoader;
using trwm.Source;

namespace trwm
{
    public class TrwmMod : MelonMod
    {
        private readonly ManualDroneController _manualDroneController;
        private readonly GameUtils _gameUtils;
        private ScriptRunner _scriptRunner;

        public TrwmMod()
        {
            _manualDroneController = new ManualDroneController();
            _gameUtils = new GameUtils();
        }
        
        public override void OnInitializeMelon()
        {
            _scriptRunner = new ScriptRunner(LoggerInstance);
            LoggerInstance.Msg("TRWM loaded");
        }

        public override void OnUpdate()
        {
            _manualDroneController.Update();
            _gameUtils.Update();
            _scriptRunner.Update();
        }

        public override void OnGUI()
        {
            _manualDroneController.UpdateGui();
            _scriptRunner.GuiUpdate();
        }
    }
}