using System;
using System.Collections.Generic;
using Keyshift.Core.Interfaces;

namespace Keyshift.Core.Classes {
    public class HistoryManager {
        private Stack<IReversibleChange> UndoStack = new Stack<IReversibleChange>();
        private Stack<IReversibleChange> RedoStack = new Stack<IReversibleChange>();

        public event EventHandler BeforeUndo;
        public event EventHandler BeforeRedo;

        public event EventHandler AfterUndo;
        public event EventHandler AfterRedo;

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

        public void DoUndo() {
            if (!CanUndo) return;
            BeforeUndo?.Invoke(this, EventArgs.Empty);
            IReversibleChange current = UndoStack.Pop();
            current.Undo();
            RedoStack.Push(current);
            AfterUndo?.Invoke(this, EventArgs.Empty);
        }

        public void DoRedo()
        {
            if (!CanRedo) return;
            BeforeRedo?.Invoke(this, EventArgs.Empty);
            IReversibleChange current = RedoStack.Pop();
            current.Redo();
            UndoStack.Push(current);
            AfterRedo?.Invoke(this, EventArgs.Empty);
        }

    }
}