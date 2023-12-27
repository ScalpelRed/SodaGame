namespace Game.Main
{
    public class ModuleRequiredException<TThrowed, TRequired> : Exception where TThrowed : ObjectModule where TRequired : ObjectModule
    { 

        public ModuleRequiredException() : base("Module " + typeof(TRequired) + " requires a " + typeof(TRequired) + " on the object.")
        {

        }

    }
}
