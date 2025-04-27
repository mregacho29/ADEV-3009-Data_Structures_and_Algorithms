using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3_MaCrizzaRegacho
{
    /// <summary>
    /// Represents a stack data structure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack<T>
    {
        /// <summary>
        /// Properties
        /// </summary>
        public Node<T>? Head { get; set; } = null;
        public int Size { get; set; } = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public Stack() => (Head, Size) = (null, 0);

        /// <summary>
        /// Adds an element to the stack.
        /// </summary>
        /// <param name="element"></param>
        public void Push(T element)
        {
            Node<T> newStackNode = new Node<T>(element, Head);
            Head = newStackNode;
            Size++;
        }


        /// <summary>
        /// Returns the element at the top of the stack.
        /// </summary>
        /// <returns></returns>
        public T Top()
        {
            ThrowIfEmpty();
            if (Head == null)
            {
                throw new ApplicationException("Stack is empty.");
            }
            return Head.Element;
        }



        /// <summary>
        /// Removes the element at the top of the stack.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            ThrowIfEmpty();
            T element = Head.Element;
            Head = Head.Next;
            Size--;

            return element;
        }

        /// <summary>
        /// Removes all elements from the stack.
        /// </summary>
        public void Clear() => (Head, Size) = (null, 0);

        /// <summary>
        /// Returns whether the stack is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => Size == 0;


        // REFACTORED METHOD
        private void ThrowIfEmpty()
        {
            if (IsEmpty())
            {
                throw new ApplicationException("Cannot pop from an empty stack.");
            }
        }
    }




}

