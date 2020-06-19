using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    class Node<T>
    {
        public T Item { get; set; }
        public Node<T> prev { get; set; }
        public Node<T> next { get; set; }
        public Node(T item)
        {
            Item = item;
        }
        public void set(T item)
        {
            this.Item = item;
        }
        public T get()
        { return Item; }
        public Node<T> getNext() { return next; }
        public Node<T> getPrev() { return prev; }
        public void setNext() { this.next = next; }
        public void setPrev() { this.prev = prev; }
    }
}
