using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_MaCrizzaRegacho
{
    public class Stack<T>
    {
        // Smart Properties
        public Node<T>? Head { get; set; } = null;
        public int Size { get; set; } = 0;

        // Constructor
        public Stack() => (Head, Size) = (null, 0);

        public void Push(T element)
        {
            Node<T> newStackNode = new Node<T>(element, Head);
            Head = newStackNode;
            Size++;
        }

        public T Top()
        {
            ThrowIfEmpty();
            return Head.Element;
        }

        public T Pop()
        {
            ThrowIfEmpty();
            T element = Head.Element;
            Head = Head.Next;
            Size--;

            return element;
        }

        public void Clear() => (Head, Size) = (null, 0);

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

