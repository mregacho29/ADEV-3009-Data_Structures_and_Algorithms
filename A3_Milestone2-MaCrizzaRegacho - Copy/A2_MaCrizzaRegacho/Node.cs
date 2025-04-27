using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;

namespace A2_MaCrizzaRegacho
{

    public class Node<T>(T element = default(T), Node<T> nextNode = null)
    {
        // Properties
        public T Element { get; set; } = element;
        public Node<T> Next { get; set; } = nextNode;
    }
}