using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace A3_MaCrizzaRegacho
{
    /// <summary>
    /// Represents a queue data structure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Queue<T>
    {
        /// <summary>
        /// Properties
        /// </summary>
        public int Size;
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public Queue() => (Size, Head, Tail) = (0, null, null);

        /// <summary>
        /// Adds an element to the queue.
        /// </summary>
        /// <param name="element"></param>
        public void Enqueue(T element)
        {
            Tail = Tail == null ? Head = new Node<T>(element) : Tail.Next = new Node<T>(element);
            Size++;
        }


        /// <summary>
        /// Returns the element at the front of the queue.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public T Front()
        {
            EnsureNotEmpty();
            return Head.Element;
        }


        /// <summary>
        /// Removes the element at the front of the queue.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public T Dequeue()
        {
            EnsureNotEmpty();

            T frontElement = Head.Element;
            Head = Head.Next;

            if (Head == null)
            {
                Tail = null;
            }

            Size--;
            return frontElement;
        }


        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear() => (Head, Tail, Size) = (null, null, 0);


        /// <summary>
        /// Returns the number of elements in the queue.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => Size == 0;





        /// <summary>
        /// REFACTORED METHOD
        /// Ensures the queue is not empty.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        private void EnsureNotEmpty()
        {
            if (Head == null)
            {
                throw new ApplicationException("Queue is empty.");
            }
        }

    }
}
