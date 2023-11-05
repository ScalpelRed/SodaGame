

namespace Game.Main
{


    public class AssetNotFoundException : Exception
    {
        public AssetNotFoundException(string type, string name)
            : base(type + " \"" + name + "\" was not found.")
        {

        }
    }
}
