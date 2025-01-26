using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;

namespace A1_MaCrizzaRegacho
{

    public class Node<T>
    {
        // Properties
        public T Element { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        /// <summary>
        /// Constructor that initializes fields to default values
        /// </summary>
        public Node()
        {
            Element = default(T);
            Next = null;
            Previous = null;
        }

        /// <summary>
        /// Constructor that initializes only the element property
        /// </summary>
        /// <param name="element">Element</param>
        public Node(T element)
        {
            Element = element;
            // Next and Previous are implicitly set to null by default
        }

        /// <summary>
        /// Constructor that initializes all properties
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="previousNode">Previous Node</param>
        /// <param name="nextNode">Next Node</param>
        public Node(T element, Node<T> previousNode, Node<T> nextNode)
        {
            Element = element;
            Previous = previousNode;
            Next = nextNode;
        }

    }
}