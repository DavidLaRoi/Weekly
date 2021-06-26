using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Weekly.Behaviour
{
    public class ChangeTracker : IEnumerable<IChange>
    {
        private readonly List<IChange> changes = new List<IChange>();

        private int headIndex = -1;

        public void Reset()
        {
            changes.Clear();
            headIndex = -1;
        }

        public bool CanRedo => headIndex < changes.Count - 1;

        public bool CanUndo => headIndex >= 0;

        public void Track(Action undo, Action redo)
        {
            Track(new Change(undo, redo));
        }

        public void Track(IChange change)
        {
            if (headIndex < changes.Count - 1)
            {
                changes.RemoveRange(headIndex + 1, changes.Count - headIndex - 1);
            }
            changes.Add(change);
            headIndex += 1;
        }

        public void Undo()
        {
            if (CanUndo)
            {
                IChange head = changes[headIndex];
                head.Undo();
                headIndex--;
            }
        }

        public void Redo()
        {
            if (CanRedo)
            {
                IChange nextHead = changes[headIndex + 1];
                nextHead.Redo();
                headIndex++;
            }
        }

        public IEnumerator<IChange> GetEnumerator()
        {
            return changes.OfType<IChange>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Change : IChange
        {
            public readonly Action Undo;
            public readonly Action Redo;

            public Change(Action undo, Action redo)
            {
                Undo = undo;
                Redo = redo;
            }

            void IChange.Redo()
            {
                Redo();
            }

            void IChange.Undo()
            {
                Undo();
            }
        }

    }
}
