using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;

namespace A1_MaCrizzaRegacho
{
    public class LinkedList<T> where T : IComparable<T>
    {
        /// <summary>
        /// Properties for the Head, Tail, and Size of the LinkedList
        /// </summary>
        public Node<T> Head { get; set; }
        // Points to the first node in the list(or null if there are no nodes)

        public Node<T> Tail { get; set; }
        // Points to the last node in the list (or null if there are no nodes)

        public int Size { get; set; }
        //Count of the number of nodes in the list, zero when the list is empty.

        /// <summary>
        /// Default constructor that initializes the Head, Tail, and Size to default values
        /// </summary>
        public LinkedList()
        {
            Clear();
        }

        /// <summary>
        /// Clears the Linked list
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Size = 0;
        }

        /// <summary>
        /// Checks if the linked list is empty, we will use the size property to determine this
        /// </summary>
        /// <returns>True if the list is empty, otherwise false</returns>
        public bool IsEmpty() => Size == 0;


        /// <summary>
        /// Returns the first element in the list
        /// </summary>
        /// <returns></returns>
        public T GetFirst()
        {
            // Check if the list is empty
            ValidateListNotEmpty();
            return Head.Element;
        }

        /// <summary>
        /// Returns the last element in the list
        /// </summary>
        /// <returns></returns>
        public T GetLast()
        {
            // Check if the list is empty
            ValidateListNotEmpty();
            return Tail.Element;
        }


        /// <summary>
        /// Takes in an element and assigns it to the Head's element, 
        /// returning the old element. 
        /// </summary>
        /// <param name="element">The new element to set</param>
        /// <returns>The old element stored in the head, before it was replaced</returns>
        public T SetFirst(T element)
        {

            // Validate the element is not null
            ValidateElementNotNull(element);

            // Store the old element from the Head node
            T oldElement = GetFirst();

            // Replace the Head node's element with the new element
            Head.Element = element;

            // Return the old element that was stored in the Head node
            return oldElement;
        }

        /// <summary>
        /// Takes in an element and assigns it to the Tail's element,
        /// returning the old element. 
        /// </summary>
        /// <param name="element">The new element to set</param>
        /// <returns>The old element stored in the tail, before it was replaced</returns>
        public T SetLast(T element)
        {
            // Validate the element is not null
            ValidateElementNotNull(element);

            // Check if the list is empty
            ValidateListNotEmpty();

            // Store the old element from the Tail node
            T oldElement = Tail.Element;

            // Replace the Tail node's element with the new element
            Tail.Element = element;

            // Return the old element that was stored in the Tail node
            return oldElement;
        }

        /// <summary>
        /// Adds a Node to the front of the LinkedList by creating a new Node 
        /// and assigning it the value of the incoming element parameter.
        /// </summary>
        /// <param name="element"></param>
        public void AddFirst(T element) //=> AddNode(null, element, Head);
        {

            // Validate the element is not null
            ValidateElementNotNull(element);

            // Create a new node with the given element
            Node<T> newHeadNode = new Node<T>(element, previousNode: null, nextNode: Head);

            if (IsEmpty())
            {
                // If the list is empty, set the Head and Tail to the new node
                Tail = newHeadNode;
            }
            else
            {
                // Set the current Head's previous to the new node
                Head.Previous = newHeadNode;
            }

            // Update the Head to the new node
            Head = newHeadNode;
            // Increment the size of the list by 1
            Size++;
        }

        /// <summary>
        /// Adds a Node to the end of the LinkedList by creating a new Node and 
        /// assigning it the value of the incoming element parameter.
        /// </summary>
        /// <param name="element"></param>
        public void AddLast(T element)
        {
            // Validate the element is not null
            ValidateElementNotNull(element);

            // Create a new node with the given element
            Node<T> newTailNode = new Node<T>(element);

            if (IsEmpty())
            {
                // If the list is empty, set the Head and Tail to the new node
                Head = newTailNode;
                Tail = newTailNode;
            }
            else
            {
                // Set the new node's Previous pointer to the current Tail
                newTailNode.Previous = Tail;

                // Set the current Tail's Next pointer to the new node
                Tail.Next = newTailNode;

                // Update the Tail to be the new node
                Tail = newTailNode;
            }

            // Increment the size of the list by 1
            Size++;
        }

        /// <summary>
        /// Removes the first Node in the list. Returns the removed node’s element.
        /// </summary>
        /// <returns>The old element stored in the head, before it was removed</returns>
        public T RemoveFirst()
        {
            // Check if the list is empty
            ValidateListNotEmpty();


            // Store the old element from the Head node
            T oldElement = Head.Element;

            // Update the Head to the next node in the list
            UpdateHead(Head.Next);

            // Decrement the size of the list by 1
            Size--;

            // Return the old element that was stored in the Head node
            return oldElement;
        }

        /// <summary>
        /// Removes the last Node in the list. Returns the removed node’s element.
        /// </summary>
        /// <returns></returns>
        public T RemoveLast()
        {
            // Check if the list is empty
            ValidateListNotEmpty();


            // Store the old element from the Tail node
            T oldElement = Tail.Element;

            // Update the Tail to the previous node in the list
            UpdateTail(Tail.Previous);


            // Decrease the size of the list by 1
            Size--;

            // Return the old element that was stored in the Tail node
            return oldElement;
        }








        /// <summary>
        /// Returns the value of the element contained in the Node at the position specified in the parameter.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public T Get(int position)
        {
            // Use the helper method to get the node at the specified position
            Node<T> node = GetNodeByPosition(position);

            // Return the element contained in the node
            return node.Element;
        }

        /// <summary>
        /// Removes the Node at the position specified. Returns the old element’s value of the removed Node.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public T Remove(int position) => RemoveNode(GetNodeByPosition(position));
        //{

        //    // Use the helper method to get the node at the specified position
        //    Node<T> nodeToRemove = GetNodeByPosition(position);

        //    // Store the old element from the node
        //    T oldElement = nodeToRemove.Element;

        //    // Update the pointers of the adjacent nodes to bypass the node being removed
        //    UpdatePointersForNodeRemoval(nodeToRemove);

        //    // Decrease the size of the list by 1
        //    Size--;

        //    // Return the old element that was stored in the node
        //    return oldElement;
        //}

        /// <summary>
        /// Finds the Node at the position specified and replaces the element on that node with a new element. Returns the old element’s value.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public T Set(T element, int position) => SetNode(GetNodeByPosition(position), element);
        //{

        //// Validate the element is not null
        //    ValidateElementNotNull(element);

        //// Use the helper method to get the node at the specified position
        //Node<T> nodeToSet = GetNodeByPosition(position);

        //// Store the old element from the node
        //T oldElement = nodeToSet.Element;

        //// Replace the element in the node with the new element
        //nodeToSet.Element = element;

        //// Return the old element that was stored in the node
        //return oldElement;
        //}

        /// <summary>
        /// Finds the Node at the position specified and adds a new node after it.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="position"></param>
        public void AddAfter(T element, int position)
        {
            // Validate the element is not null
            ValidateElementNotNull(element);

            // Use the helper method to get the node at the specified position
            Node<T> nodeToAddAfter = GetNodeByPosition(position);

            // Create a new node with the given element
            Node<T> newNode = new Node<T>(element);

            // Add the new node after the specified node
            AddNodeAfter(nodeToAddAfter, newNode); // if you pass the element, youcan validate inside there.
        }

        /// <summary>
        /// Finds the Node at the position specified and adds a new node before it.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="position"></param>
        public void AddBefore(T element, int position)
        {
            // Validate the element is not null
            ValidateElementNotNull(element);

            // Use the helper method to get the node at the specified position
            Node<T> nodeToAddBefore = GetNodeByPosition(position);

            // Create a new node with the given element
            Node<T> newNode = new Node<T>(element);

            // Add the new node before the specified node
            AddNodeBefore(nodeToAddBefore, newNode);
        }











        /// <summary>
        /// Finds the first node with the specified element and returns that node’s value.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public T Get(T element) => GetNodeByElement(element).Element;
        //{

        //    // Use the helper method to get the node with the specified element
        //    Node<T> node =

        //    // Return the element contained in the node
        //    return node.Element;
        //}

        /// <summary>
        /// Adds a new node after the first node found with the specified oldElement value.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="oldElement"></param>
        public void AddAfter(T element, T oldElement)
        {
            // Validate the element is not null
            ValidateElementNotNull(element);

            // Use the helper method to get the node with the specified old element
            Node<T> nodeToAddAfter = GetNodeByElement(oldElement);

            //// Validate the node is not null
            //ValidateNodeNotNull(nodeToAddAfter);

            // Create a new node with the given element
            Node<T> toAdd = new Node<T>(element);

            // If the node to add after is the tail, update the tail
            AddNodeAfter(nodeToAddAfter, toAdd);

        }
        /// <summary>
        /// Adds a new node before the first node found with the specified oldElement value.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="oldElement"></param>
        public void AddBefore(T element, T oldElement)
        {
            // Validate the element is not null
            ValidateElementNotNull(element);

            //// Validate the oldElement is not null
            //ValidateOldElementNotNull(oldElement);

            // Use the helper method to get the node with the specified old element
            Node<T> nodeToAddBefore = GetNodeByElement(oldElement);

            //// Validate the node is not null
            //ValidateNodeNotNull(nodeToAddBefore);

            // Create a new node with the given element
            Node<T> newNode = new Node<T>(element);

            // Update the pointers of the adjacent nodes to insert the new node before the specified node
            AddNodeBefore(nodeToAddBefore, newNode);
        }

        /// <summary>
        /// Removes the first node found with the specified element and returns that node’s value.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public T Remove(T element)
        {
            //// Validate the element is not null
            //ValidateElementNotNull(element);

            // Use the helper method to get the node with the specified element
            Node<T> nodeToRemove = GetNodeByElement(element);

            //// Validate the node is not null
            //ValidateNodeNotNull(nodeToRemove);

            // Remove the node and return the old element
            return RemoveNode(nodeToRemove);
        }

        /// <summary>
        /// Finds the first node found with the specified oldElement value and replaces the element
        /// on that node with a new element. Returns the original value of the found Node’s element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="oldElement"></param>
        /// <returns></returns>
        public T Set(T element, T oldElement)
        {
            //// Validate the element is not null
            //ValidateElementNotNull(element);

            // Validate the oldElement is not null
            //ValidateOldElementNotNull(oldElement);

            // Use the helper method to get the node with the specified old element
            Node<T> nodeToSet = GetNodeByElement(oldElement);

            // Validate the node is not null
            //ValidateNodeNotNull(nodeToSet);

            // Use the SetNode helper method to set the new element and return the old element
            return SetNode(nodeToSet, element);
        }

        /// <summary>
        /// Adds the element into the linked list in natural ascending order.
        /// </summary>
        /// <param name="element"></param>
        public void Insert(T element)
        {

            // Validate the element is not null
            ValidateElementNotNull(element);

            // Create a new node with the given element
            Node<T> newNode = new Node<T>(element);

            // If the list is empty, set the Head and Tail to the new node
            //if (IsEmpty())
            //{
            //    Head = newNode;
            //    Tail = newNode;
            //}
            //else
            //{
                // Start from the head
                Node<T> currentNode = Head;

                // Traverse the list to find the correct position to insert the new node
                while (currentNode != null && currentNode.Element.CompareTo(element) < 0)
                {
                    currentNode = currentNode.Next;
                }

                // Insert the new node at the correct position
                //if (currentNode == Head)
                //{
                //    AddFirst(element);
                //    //// Insert at the beginning
                //    //newNode.Next = Head;
                //    //Head.Previous = newNode;
                //    //Head = newNode;
                //}
               // else 
                if (currentNode == null)
                {
                    AddLast(element); // empty or LARGEST! :)
                    //// Insert at the end
                    //Tail.Next = newNode;
                    //newNode.Previous = Tail;
                    //Tail = newNode;
                }
                else
                {
                    AddNodeBefore(currentNode, new Node<T>(element));
                    //// Insert in the middle (Add Before?)
                    //newNode.Next = currentNode;
                    //newNode.Previous = currentNode.Previous;
                    //currentNode.Previous.Next = newNode;
                    //currentNode.Previous = newNode;
                }
           // }
            // AddBefore or AddLast()/AddFirst() ... 
            // Increase the size of the list by 1
            //Size++;
        }

        /// <summary>
        /// Sorts the elements into ascending order using bubble sort.
        /// </summary>
        public void SortAscending()
        {
            // Check if the list is empty or has only one element. If so, return immediately as the list is already sorted.
            if (IsEmpty() || Head.Next == null) return;

            // to track if any elements were swapped during a pass.
            bool swapped;
            
            //A counter to track the number of passes through the list.
            int counter = 0; 

            do
            {
                // Reset the swapped flag at the beginning of each pass.
                swapped = false;

                // Start from the head
                Node<T> current = Head;

                // Loop through the list, comparing adjacent elements and swapping them if they are out of order.
                // The range of the loop decreases with each pass, as the largest elements are bubbled to the end of the list.
                for (int i = 0; i < Size - 1 - counter; i++)
                {
                    // Compare the current element with the next element.
                    if (current.Element.CompareTo(current.Next.Element) > 0)
                    {
                        // Swap the elements if they are out of order.
                        T temp = current.Element;
                        current.Element = current.Next.Element;
                        current.Next.Element = temp;
                        swapped = true; // Set the swapped flag to true, indicating that a swap was made.
                    }
                    
                    // Move to the next node in the list.
                    current = current.Next; 
                }
                // Increment the counter after each pass.
                counter++;
            } while (swapped); // Continue looping until no elements are swapped in a pass.
        }


        #region REFACTORED METHODS

        #region VALIDATION OR CHECK PRIVATE METHODS
        /// <summary>
        /// REFACTORED METHOD
        /// Check if the element is null and thrown an ArgumentNullException
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void ValidateElementNotNull(T element)
        {
            // Check if the element is null
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "Element cannot be null");
            }
        }

        /// <summary>
        /// REFACTORED METHOD
        /// Check if the old element is null and throw an ArgumentNullException.
        /// </summary>
        /// <param name="oldElement"></param>
        private void ValidateOldElementNotNull(T oldElement)
        {
            if (oldElement == null)
            {
                throw new ArgumentNullException(nameof(oldElement), "Old element cannot be null");
            }
        }




        /// <summary>
        /// REFACTORED METHOD
        /// Check if the list is empty and throw an ApplicationException
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        private void ValidateListNotEmpty()
        {
            if (IsEmpty())
            {
                throw new ApplicationException("The list is empty");
            }
        }

        /// <summary>
        /// REFACTORED METHOD
        /// Validate the position and throw an exception if invalid.
        /// </summary>
        /// <param name="position"></param>
        private void ValidatePosition(int position)
        {
            if (position <= 0 || position > Size)
            {
                throw new ApplicationException("Position must be between 1 and the size of the list.");
            }
        }


        /// <summary>
        /// REFACTORED METHOD
        /// Check if the node is null and throw an ApplicationException if it is.
        /// </summary>
        /// <param name="node"></param>
        private void ValidateNodeNotNull(Node<T> node)
        {
            if (node == null)
            {
                throw new ApplicationException("Element not found in the list");
            }
        }

        #endregion



        /// <summary>
        /// REFACTORED METHOD
        /// Finds the first node with the specified element.
        /// </summary>
        /// <param name="element">The element to find</param>
        /// <returns>The node with the specified element, or null if not found</returns>
        private Node<T> GetNodeByElement(T element)
        {
            // Validate the element is not null
            ValidateElementNotNull(element);

            // Start from the head
            Node<T> currentNode = Head;

            // Traverse the list to find the node with the specified element
            while (currentNode != null)
            {
                if (currentNode.Element.CompareTo(element) == 0)
                {
                    return currentNode;
                }
                currentNode = currentNode.Next;
            }

            // Return null if the element is not found
            ValidateNodeNotNull(currentNode);

            // Return null if the element is not found
            return null; // should never happen
        }

        /// <summary>
        /// REFACTORED METHOD
        /// Returns the Node at the specified position.
        /// </summary>
        /// <param name="position">The position of the node to retrieve</param>
        /// <returns>The Node at the specified position</returns>
        private Node<T> GetNodeByPosition(int position)
        {

            // Validate the position
            ValidatePosition(position);

            // Start from the head
            Node<T> currentNode = Head;

            // Traverse the list to the specified position
            for (int i = 1; i < position; i++)
            {
                currentNode = currentNode.Next;
            }

            return currentNode;
        }



        /// <summary>
        /// REFACTORED METHOD
        /// Update the Head to the new node and set the new node's previous to null.
        /// </summary>
        /// <param name="newHead"></param>
        private void UpdateHead(Node<T> newHead)
        {
            Head = newHead;
            if (Head != null)
            {
                Head.Previous = null;
                return;
            }
            Tail = null;
            
        }

        /// <summary>
        /// REFACTORED METHOD
        /// Update the Tail to the new node and set the new node's next to null.
        /// </summary>
        /// <param name="newTail"></param>
        private void UpdateTail(Node<T> newTail)
        {
            Tail = newTail;
            if (Tail != null)
            {
                Tail.Next = null;
                return;
            }
            Head = null;

        }




        /// <summary>
        /// REFACTORED METHOD
        /// updating the pointers of adjacent nodes when removing a node
        /// </summary>
        /// <param name="nodeToRemove"></param>
        private void UpdatePointersForNodeRemoval(Node<T> nodeToRemove)
        {
            // Update adjacent nodes' pointers
            if (nodeToRemove.Previous != null)
            {
                nodeToRemove.Previous.Next = nodeToRemove.Next;
            }
            if (nodeToRemove.Next != null)
            {
                nodeToRemove.Next.Previous = nodeToRemove.Previous;
            }

            // Update boundary nodes using pattern matching
            if (nodeToRemove == Head) UpdateHead(nodeToRemove.Next);
            if (nodeToRemove == Tail) UpdateTail(nodeToRemove.Previous);
        }








        /// <summary>
        /// REFACTORED METHOD
        /// adding nodes after a specified node
        /// </summary>
        /// <param name="nodeToAddAfter"></param>
        /// <param name="newNode"></param>
        private void AddNodeAfter(Node<T> nodeToAddAfter, Node<T> newNode)
        {
            // Set the new node's next pointer to the next node of the node to add after
            newNode.Next = nodeToAddAfter.Next;

            // Set the new node's previous pointer to the node to add after
            newNode.Previous = nodeToAddAfter;

            // If the node to add after has a next node, update its previous pointer to the new node
            if (nodeToAddAfter.Next != null)
            {
                nodeToAddAfter.Next.Previous = newNode;
            }

            // Update the next pointer of the node to add after to the new node
            nodeToAddAfter.Next = newNode;

            // If the node to add after is the tail, update the tail to the new node
            if (nodeToAddAfter == Tail)
            {
                Tail = newNode;
            }

            // Increment the size of the list
            Size++;
        }

        /// <summary>
        /// REFACTORED METHOD
        /// Adding nodes before a specified node
        /// </summary>
        /// <param name="nodeToAddBefore"></param>
        /// <param name="newNode"></param>
        private void AddNodeBefore(Node<T> nodeToAddBefore, Node<T> newNode)
        {
            // Set the new node's next pointer to the node to add before
            newNode.Next = nodeToAddBefore;

            // Set the new node's previous pointer to the previous node of the node to add before
            newNode.Previous = nodeToAddBefore.Previous;

            // If the node to add before has a previous node, update its next pointer to the new node
            if (nodeToAddBefore.Previous != null)
            {
                nodeToAddBefore.Previous.Next = newNode;
            }

            // Update the previous pointer of the node to add before to the new node
            nodeToAddBefore.Previous = newNode;

            // If the node to add before is the head, update the head to the new node
            if (nodeToAddBefore == Head)
            {
                Head = newNode;
            }

            // Increment the size of the list
            Size++;
        }




        /// <summary>
        /// Helper method to remove a node and return its element.
        /// </summary>
        /// <param name="nodeToRemove"></param>
        /// <returns></returns>
        private T RemoveNode(Node<T> nodeToRemove)
        {
            // Store the old element from the node
            T oldElement = nodeToRemove.Element;

            // Update the pointers of the adjacent nodes to bypass the node being removed
            UpdatePointersForNodeRemoval(nodeToRemove);

            // Decrease the size of the list by 1
            Size--;

            // Return the old element that was stored in the node
            return oldElement;
        }


        /// <summary>
        /// Helper method to set a node's element and return the old element.
        /// </summary>
        /// <param name="nodeToSet"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private T SetNode(Node<T> nodeToSet, T element)
        {
            // Validate the element is not null
            ValidateElementNotNull(element);

            // Store the old element from the node
            T oldElement = nodeToSet.Element;

            // Replace the element in the node with the new element
            nodeToSet.Element = element;

            // Return the old element that was stored in the node
            return oldElement;
        }


        #endregion

    }
}
