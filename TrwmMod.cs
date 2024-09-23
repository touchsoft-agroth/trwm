
using MelonLoader;
using trwm.Source;
using trwm.Source.Logging;

namespace trwm
{
    public class TrwmMod : MelonMod
    {
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

        public override void OnDeinitializeMelon()
        {
            Source.Logging.Logger.UnbindOutput();
        }

        public override void OnUpdate()
        {
        }

        public override void OnGUI()
        {
        }
    }
}