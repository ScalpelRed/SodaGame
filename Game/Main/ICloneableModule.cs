namespace Game.Main
{
    public interface ICloneableModule<T> where T : ObjectModule
    {
        public T Clone(WorldObject newLinkedObject);
    }
}
