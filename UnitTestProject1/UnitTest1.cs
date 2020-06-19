using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MyIntTest1()
        {
            MyInt x = new MyInt(154);
            MyInt y = new MyInt("-230");
            MyInt c = MyInt.add(x, y);
            int expected = -76;
            Assert.AreEqual(expected, int.Parse(c.ToString()));
        }
        [TestMethod]
        public void MyIntTest2()
        {
            MyInt x = new MyInt(154);
            MyInt y = new MyInt("-230");
            MyInt c = MyInt.max(x, y);
            int expected = 154;
            Assert.AreEqual(expected, int.Parse(c.ToString()));
        }
        [TestMethod]
        public void MyIntTest3()
        {
            MyInt z = new MyInt("15343543481024542121551");
            long num = 1551212454201843451;
            Assert.AreEqual(num, z.longValue());
        }
        [TestMethod]
        public void MyIntTest4()
        {
            MyInt x = new MyInt(154);
            MyInt y = new MyInt("2");
            MyInt c = MyInt.multiply(x, y);
            int expected = 308;
            Assert.AreEqual(expected, int.Parse(c.ToString()));
        }
        [TestMethod]
        public void DeqTest1()
        {
            DEQueue<int> Deq = new DEQueue<int>();
            Deq.pushFront(1);
            Deq.pushFront(2);
            Deq.pushFront(3);
            int[] expected = { 3, 2, 1 };
            int i = 0;
            foreach (int s in Deq)
            {
                Assert.AreEqual(expected[i], s);
                i++;
            }
        }
        [TestMethod]
        public void DeqTest2()
        {
            DEQueue<int> Deq = new DEQueue<int>();
            Deq.pushFront(1);
            Deq.pushFront(2);
            Deq.pushFront(3);
            Deq.popBack();
            int[] expected = { 3, 2};
            int i = 0;
            foreach (int s in Deq)
            {
                Assert.AreEqual(expected[i], s);
                i++;
            }
        }
        [TestMethod]
        public void DeqTest3()
        {
            DEQueue<int> Deq = new DEQueue<int>();
            Deq.pushFront(1);
            Deq.pushFront(2);
            Deq.pushFront(3);
            int res = Deq.front;
            int expected = 3;
            Assert.AreEqual(expected, res);
        }
        [TestMethod]
        public void DeqTest4()
        {
            DEQueue<int> Deq = new DEQueue<int>();
            Deq.pushBack(1);
            Deq.pushBack(2);
            Deq.pushBack(3);
            int[] expected = { 1, 2, 3 };
            int i = 0;
            foreach (int s in Deq)
            {
                Assert.AreEqual(expected[i], s);
                i++;
            }
        }
        [TestMethod]
        public void DeqTest5()
        {
            DEQueue<int> Deq = new DEQueue<int>();
            Deq.pushFront(1);
            Deq.pushFront(2);
            Deq.pushFront(3);
            Deq.popFront();
            int[] expected = { 2, 1 };
            int i = 0;
            foreach (int s in Deq)
            {
                Assert.AreEqual(expected[i], s);
                i++;
            }
        }
        [TestMethod]
        public void DeqTest6()
        {
            DEQueue<int> Deq = new DEQueue<int>();
            Deq.pushFront(1);
            Deq.pushFront(2);
            Deq.pushFront(3);
            int res = Deq.back;
            int expected = 1;
            Assert.AreEqual(expected, res);
        }
    }
}
