namespace Keyshift.Core.Interfaces {
    public interface IReversibleChange {
        void Undo();

        void Redo();
    }
}