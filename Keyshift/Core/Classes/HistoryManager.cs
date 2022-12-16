using System;
using System.Collections.Generic;
using Keyshift.Core.Interfaces;

namespace Keyshift.Core.Classes {
    public class HistoryManager {
        private Stack<IReversibleChange> UndoStack = new Stack<IReversibleChange>();
        private Stack<IReversibleChange> RedoStack = new Stack<IReversibleChange>();

        /// <summary>
        /// Triggered when Undo is performed, before actually doing so.
        /// </summary>
        public event EventHandler BeforeUndo;
        /// <summary>
        /// Triggered when Redo is performed, before actually doing so.
        /// </summary>
        public event EventHandler BeforeRedo;

        /// <summary>
        /// Triggered when Undo is performed, after actually doing so.
        /// </summary>
        public event EventHandler AfterUndo;
        /// <summary>
        /// Triggered when Redo is performed, after actually doing so.
        /// </summary>
        public event EventHandler AfterRedo;

        /// <summary>
        /// Triggered when an element is pushed to the Undo queue, before actually doing so.
        /// </summary>
        public event EventHandler<IReversibleChange> BeforePush;
        /// <summary>
        /// Triggered after an element is pushed to the Undo queue, before actually doing so.
        /// </summary>
        public event EventHandler<IReversibleChange> AfterPush;

        /// <summary>
        /// Triggered when the History is cleared up.
        /// </summary>
        public event EventHandler Erasure;

        /// <summary>
        /// Read only array of undoable history items
        /// </summary>
        public IReversibleChange[] UndoArray => UndoStack.ToArray();
        /// <summary>
        /// Read only array of redo-able history items
        /// </summary>
        public IReversibleChange[] RedoArray => RedoStack.ToArray();

        /// <summary>
        /// Whether History can undo.
        /// </summary>
        public bool CanUndo => UndoStack.Count > 0;
        /// <summary>
        /// Whether History can redo.
        /// </summary>
        public bool CanRedo => RedoStack.Count > 0;

        public void AddUndo(IReversibleChange change)
        {
            BeforePush?.Invoke(this, change);
            RedoStack.Clear();
            UndoStack.Push(change);
            AfterPush?.Invoke(this, change);
        }

        public void EraseHistory() {
            Erasure?.Invoke(this, EventArgs.Empty);
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