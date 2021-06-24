namespace Weekly.Behaviour
{
    public interface IChange
    {
        void Undo();
        void Redo();
    }
}
