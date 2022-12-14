using System;
using System.Collections.Generic;
using Keyshift.Core.Interfaces;

namespace Keyshift.Core.Classes {
    public class HistoryManager {
        private Stack<IReversibleChange> UndoStack = new Stack<IReversibleChange>();
        private Stack<IReversibleChange> RedoStack = new Stack<IReversibleChange>();

        /// <summary>
        /// Read only array of undoable history items
        /// </summary>
        public IReversibleChange[] UndoArray => UndoStack.ToArray();
        /// <summary>
        /// Read only array of redo-able history items
        /// </summary>
        public IReversibleChange[] RedoArray => RedoStack.ToArray();

        public bool CanUndo => UndoStack.Count > 0;
        public bool CanRedo => RedoStack.Count > 0;

        public void AddUndo(IReversibleChange change)
        {
            RedoStack.Clear();
            UndoStack.Push(change);
        }

        public void EraseHistory() {
            UndoStack.Clear();
            RedoStack.Clear();
        }

        public void Undo() {
            if (!CanUndo) return;
            IReversibleChange current = UndoStack.Pop();
            current.Undo();
            RedoStack.Push(current);
        }

        public void Redo()
        {
            if (!CanRedo) return;
            IReversibleChange current = RedoStack.Pop();
            current.Redo();
            UndoStack.Push(current);
        }

    }
}