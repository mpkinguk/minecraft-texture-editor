using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericUndoRedoManagerAPI
{
    /// <summary>
    /// Undo manager class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UndoManagerAction<T>:IDisposable
    {
        /// <summary>
        /// Private Undo stack
        /// </summary>
        private Stack<T> UndoStack;

        /// <summary>
        /// Private Redo stack
        /// </summary>
        private Stack<T> RedoStack;

        /// <summary>
        /// The current item
        /// </summary>
        public T CurrentItem { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public UndoManagerAction()
        {
            UndoStack = new Stack<T>();
            RedoStack = new Stack<T>();
        }

        /// <summary>
        /// Clear all global variables
        /// </summary>
        public void Clear()
        {
            UndoStack.Clear();
            RedoStack.Clear();
            CurrentItem = default;
        }

        /// <summary>
        /// Add a new item to the Undo stack
        /// </summary>
        /// <param name="item">T</param>
        public void AddItem(T item)
        {
            if (CurrentItem != null)
            {
                UndoStack.Push(CurrentItem);
            }
            CurrentItem = item;
            RedoStack.Clear();
        }

        /// <summary>
        /// Undo
        /// </summary>
        /// <returns>T</returns>
        public T Undo()
        {
            RedoStack.Push(CurrentItem);
            CurrentItem = UndoStack.Pop();
            return CurrentItem;
        }

        /// <summary>
        /// Redo
        /// </summary>
        /// <returns>T</returns>
        public T Redo()
        {
            UndoStack.Push(CurrentItem);
            CurrentItem = RedoStack.Pop();
            return CurrentItem;
        }

        /// <summary>
        /// Can perform an Undo
        /// </summary>
        public bool CanUndo => UndoStack.Count > 0;

        /// <summary>
        /// Can perform a Redo
        /// </summary>
        public bool CanRedo => RedoStack.Count > 0;

        /// <summary>
        /// Returns a generic List cast of the Undo stack
        /// </summary>
        /// <returns>List(T)</returns>
        public List<T> UndoItems()
        {
            return UndoStack.ToList();
        }

        /// <summary>
        /// Returns a generic List cast of the Redo stack
        /// </summary>
        /// <returns>List(T)</returns>
        public List<T> RedoItems()
        {
            return RedoStack.ToList();
        }

        public void Dispose()
        {
            Clear();

            UndoStack = null;
            RedoStack = null;
        }
    }
}