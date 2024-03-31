using Triode.Game.Util;

namespace Triode.Game.ExactGame.SodaScenes
{
    public class SodaInfo
    {
        public readonly TranslatableString Name;
        public readonly TranslatableString Commentary;

        public SodaInfo(string rawName, string rawComm = "") 
        { 
            Name = new TranslatableString(rawName);
            Commentary = new TranslatableString(rawComm);
        }
    }
}
