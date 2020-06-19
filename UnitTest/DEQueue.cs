using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class DEQueue<T> : IEnumerable<T>
    {
        Node<T> head;
        Node<T> tail;
        int size = 0;
        private T[] mas;

        public void pushFront(T item)
        {
            Node<T> node = new Node<T>(item);
            Node<T> temp = head;
            node.next = temp;
            head = node;
            head.next = temp;
            if (size == 0)
                tail = head;
            else
                temp.prev = node;
            size++;
        }
        public void popFront()
        {
            if (size == 0)
                throw new InvalidOperationException();
            T element = head.Item;
            if (size == 1)
                head = tail = null;
            else
            {
                head = head.next;
                head.prev = null;
            }
            size--;
        }
        public void pushBack(T item)
        {
            Node<T> element = new Node<T>(item);
            if (tail == null)
                head = element;
            else
            {
                tail.next = element;
                element.prev = tail;
            }
            tail = element;
            size++;
        }
        public void popBack()
        {
            if (size == 0)
                throw new InvalidOperationException();
            T temp = tail.Item;
            if (size == 1)
                head = tail = null;
            else
            {
                tail = tail.prev;
                tail.next = null;
            }
            size--;
        }
        public void Clear()
        {
            head = null;
            tail = null;
            size = 0;
        }
        public int Size { get { return size; } }
        public T front
        {
            get
            {
                if (size == 0)
                    return default(T);
                return head.Item;
            }
        }
        public T back
        {
            get
            {
                if (size == 0)
                    return default(T);
                return tail.Item;
            }
        }
        public void printNode()
        {
            Node<T> temp = head;
            while (temp != null)
            {
                Console.WriteLine(temp.Item);
                temp = temp.next;
            }
            Console.Read();
        }
        public T[] toArray()
        {
            mas = new T[size];
            Node<T> temp = head;
            while (temp != null)
            {
                for (int i = 0; i < size; i++)
                {
                    mas[i] = temp.Item;
                    temp = temp.next;
                }
            }
            return mas;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Item;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}
