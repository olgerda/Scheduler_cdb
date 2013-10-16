﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalendarControl3
{

    static public class TESTCASE
    {
        static public ITable2ControlInterface GetTestTable()
        {
            /*
             * TEST
             */
            var testtable = new TestTable(200);
            //testtable.descriptions.Add(100, "100");
            testtable.descriptions.Add(1000, "1000");
            testtable.descriptions.Add(1600, "1600");
            testtable.descriptions.Add(200,"200");
            testtable.descriptions.Add(400,"400");
            testtable.descriptions.Add(600,"600");
            testtable.descriptions.Add(1500,"1500");
            testtable.descriptions.Add(900,"900");
//             testtable.descriptions.Add("first");
//             testtable.descriptions.Add("second");
//             testtable.descriptions.Add("thrird");
            var testcol1 = new TestColumn("column1");
            var testcol2 = new TestColumn("column2");
            testcol1.entities.Add(new TestEntity("200\r\n400",200, 400));
            testcol1.entities.Add(new TestEntity("600\r\n1500", 600, 1500));
            testcol2.entities.Add(new TestEntity("200\r\n900", 200, 900));
            testtable.columns.Add(testcol1);
            
            testtable.columns.Add(new TestColumn("3"));
            testtable.columns.Add(new TestColumn("4"));
            
//             testtable.columns.Add(new TestColumn("5"));
//             testtable.columns.Add(new TestColumn("6"));
//             testtable.columns.Add(new TestColumn("7"));
//             testtable.columns.Add(new TestColumn("8"));
//             testtable.columns.Add(new TestColumn("9"));
//             testtable.columns.Add(new TestColumn("10"));
            testtable.columns.Add(new TestColumn("11"));
            testtable.columns.Add(testcol2);


            /*
             * /TEST
             */
            return testtable;
        }
    }

    public class TestTable : ITable2ControlInterface
    {
        public List<IColumn2ControlInterface> columns;
        //public List<IDescription2ControlInterface> descriptions;
        public Dictionary<int, string> descriptions;
        public int minValue;
        public int maxValue;

        public TestTable(int minV = 100, int maxV = 2000)
        {
            columns = new List<IColumn2ControlInterface>();
            //descriptions = new List<IDescription2ControlInterface>();
            descriptions = new Dictionary<int, string>();
            minValue = minV;
            maxValue = maxV;
        }

        public int GetColumnCount()
        {
            return columns.Count;
        }

        public int GetMinValue()
        {
            return minValue;
        }

        public int GetMaxValue()
        {
            return maxValue;
        }

        //public List<IDescription2ControlInterface> GetDescripptionsToValueLevels()
        public Dictionary<int, string> GetDescripptionsToValueLevels()
        {
            return descriptions;
        }

        public List<IColumn2ControlInterface> GetColumns()
        {
            return columns;
        }
    }

    public class TestColumn : IColumn2ControlInterface
    {
        string name;
        public List<IEntity2ControlInterface> entities;

        public TestColumn(string n = "Default name")
        {
            name = n;
            entities = new List<IEntity2ControlInterface>();
        }

        public string GetName()
        {
            return name;
        }

        public List<IEntity2ControlInterface> GetEntities()
        {
            //entities.Add(new TestEntity());
            return entities;
        }
    }

    public class TestEntity : IEntity2ControlInterface
    {
        public string Message;
        public int tLevel;
        public int bLevel;
        public object obj;

        public TestEntity(string msg = "Simple String To Show", int tl = 500, int bl = 1000, object o = null)
        {
            Message = msg;
            tLevel = tl;
            bLevel = bl;
            obj = o;
        }

        public string StringToShow()
        {
            return Message;
        }

        public int TopLevel()
        {
            return tLevel;
        }

        public int BottomLevel()
        {
            return bLevel;
        }

        public object GetObject()
        {
            return obj;
        }


        public ulong GetID()
        {
            throw new NotImplementedException();
        }
    }

//     public class TestDescription : IDescription2ControlInterface
//     {
//         public string description;
//         public int level;
// 
// 
// 
//         public TestDescription(string msg = "default", int lvl = 200)
//         {
//             description = msg;
//             level = lvl;
//         }
// 
//         public int ToInt()
//         {
//             return level;
//         }
// 
//         public string ToString()
//         {
//             return description;
//         }
//     }


}