using NUnit.Framework;
using System.Collections.Generic;

public class AutoConstructTest
{
    private class AutoContainer<Type>
    {
        private AutoConstruct<List<Type>> autoList = new AutoConstruct<List<Type>>();
        public List<Type> list => autoList.Data;
    }

    private struct TestStruct
    {
        public int X, Y;
        public TestStruct(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    private class TestClass
    {
        public int X, Y;
        public TestClass(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    #region Primative List
    [Test]
    public void PrimativeListEmpty()
    {
        var list = new List<int>();
        var testClass = new AutoContainer<int>();

        List<int> auto = testClass.list;

        Assert.AreEqual(list, auto);
    }

    [Test]
    public void PrimativeListWithElements()
    {
        var list = new List<int>();
        var testClass = new AutoContainer<int>();

        for (var i = 0; i < 100; i++)
        {
            list.Add(i);
            testClass.list.Add(i);
        }

        List<int> auto = testClass.list;

        Assert.AreEqual(list, auto);
    }
    #endregion

    #region Struct List
    [Test]
    public void StructListEmpty()
    {
        var list = new List<TestStruct>();
        var testClass = new AutoContainer<TestStruct>();

        List<TestStruct> auto = testClass.list;

        Assert.AreEqual(list, auto);
    }

    [Test]
    public void StructListWithElements()
    {
        var list = new List<TestStruct>();
        var testClass = new AutoContainer<TestStruct>();

        for (var i = 0; i < 100; i++)
        {
            var s = new TestStruct(i, 2 * i);
            list.Add(s);
            testClass.list.Add(s);
        }

        List<TestStruct> auto = testClass.list;

        Assert.AreEqual(list, auto);
    }
    #endregion

    #region Class List
    [Test]
    public void ClassListEmpty()
    {
        var list = new List<TestClass>();
        var testClass = new AutoContainer<TestClass>();

        List<TestClass> auto = testClass.list;

        Assert.AreEqual(list, auto);
    }

    [Test]
    public void ClassListWithElements()
    {
        var list = new List<TestClass>();
        var testClass = new AutoContainer<TestClass>();

        for (var i = 0; i < 100; i++)
        {
            var s = new TestClass(i, 2 * i);
            list.Add(s);
            testClass.list.Add(s);
        }

        List<TestClass> auto = testClass.list;

        Assert.AreEqual(list, auto);
    }
    #endregion
}
