namespace Triode.Game.General
{
    public class ModuleRequiredException<TThrowed, TRequired> : Exception where TThrowed : ObjectModule where TRequired : ObjectModule
    { 

        public ModuleRequiredException() : base("Module " + typeof(TThrowed) + " requires a " + typeof(TRequired) + " on the object.")
        {

        }

    }
}
