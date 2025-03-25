using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;

namespace A3_MaCrizzaRegacho
{
    /// <summary>
    /// Represents a node in a doubly linked list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="element"></param>
    /// <param name="previousNode"></param>
    /// <param name="nextNode"></param>
    public class Node<T>(T element = default(T), Node<T> previousNode = null, Node<T> nextNode = null)
    {
        // Properties
        public T Element { get; set; } = element;
        public Node<T> Previous { get; set; } = previousNode;
        public Node<T> Next { get; set; } = nextNode;
    }
}