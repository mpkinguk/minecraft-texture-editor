using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericUndoRedoManagerAPI
{
    /// <summary>
    /// Undo manager class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UndoManagerAction<T> : IDisposable
    {
        /// <summary>
        /// The current item
        /// </summary>
        public T CurrentItem { get; set; }

        /// <summary>
        /// Private Undo stack
        /// </summary>
        private readonly Stack<T> UndoStack;

        /// <summary>
        /// Private Redo stack
        /// </summary>
        private readonly Stack<T> RedoStack;

        /// <summary>
        /// The log4net logger
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Constructor
        /// </summary>
        public UndoManagerAction(ILog log)
        {
            _log = log;

            UndoStack = new Stack<T>();
            RedoStack = new Stack<T>();
        }

        /// <summary>
        /// Clear all global variables
        /// </summary>
        public void Clear()
        {
            try
            {
                UndoStack.Clear();
                RedoStack.Clear();
                CurrentItem = default;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Add a new item to the Undo stack
        /// </summary>
        /// <param name="item">T</param>
        public void AddItem(T item)
        {
            try
            {
                if (CurrentItem != null)
                {
                    UndoStack.Push(CurrentItem);
                }
                CurrentItem = item;
                RedoStack.Clear();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Undo
        /// </summary>
        /// <returns>T</returns>
        public T Undo()
        {
            try
            {
                RedoStack.Push(CurrentItem);
                CurrentItem = UndoStack.Pop();
                return CurrentItem;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
                return default;
            }
        }

        /// <summary>
        /// Redo
        /// </summary>
        /// <returns>T</returns>
        public T Redo()
        {
            try
            {
                UndoStack.Push(CurrentItem);
                CurrentItem = RedoStack.Pop();
                return CurrentItem;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
                return default;
            }
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
            try
            {
                Clear();

                UndoStack.Clear();
                RedoStack.Clear();

                // The only way to get rid of all the undos/redos
                GC.Collect();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }
    }
}