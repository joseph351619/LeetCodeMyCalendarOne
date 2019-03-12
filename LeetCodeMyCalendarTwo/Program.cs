using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeMyCalendarTwo
{
    public class Program
    {
        static void Main(string[] args)
        {
            MyCalendar myCalendar = new MyCalendar();
            //Console.WriteLine( myCalendar.Book(10, 20)); // returns true
            //Console.WriteLine( myCalendar.Book(50, 60)); // returns true
            //Console.WriteLine( myCalendar.Book(10, 40)); // returns false
            //Console.WriteLine( myCalendar.Book(5, 15)); // returns false
            //Console.WriteLine( myCalendar.Book(5, 10)); // returns true
            //Console.WriteLine(myCalendar.Book(25, 55)); // returns false
            Console.WriteLine(myCalendar.Book(6, 23)); // returns false
            Console.WriteLine(myCalendar.Book(6, 20)); // returns false
            Console.WriteLine(myCalendar.Book(4, 17)); // returns false
                //Console.WriteLine(myCalendar.Book(26, 35));
                //Console.WriteLine(myCalendar.Book(26, 32));
                //Console.WriteLine(myCalendar.Book(25, 32));
                //Console.WriteLine(myCalendar.Book(18, 26));
                //Console.WriteLine(myCalendar.Book(40, 45));
                //Console.WriteLine(myCalendar.Book(19, 26));
                //Console.WriteLine(myCalendar.Book(48, 50));
                //Console.WriteLine(myCalendar.Book(1, 6));
                //Console.WriteLine(myCalendar.Book(46, 50));
                //Console.WriteLine(myCalendar.Book(11, 18));
                        //Console.WriteLine(myCalendar.Book(47, 50));
                        //Console.WriteLine(myCalendar.Book(1, 10));
                        //Console.WriteLine(myCalendar.Book(27, 36));
                        //Console.WriteLine(myCalendar.Book(40, 47));
                        //Console.WriteLine(myCalendar.Book(20, 27));
                        //Console.WriteLine(myCalendar.Book(15, 23));
                        //Console.WriteLine(myCalendar.Book(10, 18));
                        //Console.WriteLine(myCalendar.Book(27, 36));
                        //Console.WriteLine(myCalendar.Book(17, 25));
                    //Console.WriteLine(myCalendar.Book(17, 25));
                    //Console.WriteLine(myCalendar.Book(17, 25));
            Console.ReadLine();
        }
    }
    public class MyCalendar
    {
        public MyCalendar() { }
        public Interval Intervals;
        public Interval StartInterval;
        public TreeNode OverLapTree;
        public bool Book(int start, int end)
        {
            if(OverLapTree != null)
            {
                TreeNode current = new TreeNode(start, end);
                if (!IsAccept(current, OverLapTree))
                    return false;
            }
            Intervals = StartInterval;
            while(Intervals!=null && Intervals.Start<end)
            {
                if(Intervals.Start <= start && Intervals.End > start)
                {
                    if (Intervals.End > end)
                        MakeTree(start, end);
                    else
                        MakeTree(start, Intervals.End);
                }else if(Intervals.Start > start && Intervals.Start < end)
                {
                    if (Intervals.End < end)
                        MakeTree(Intervals.Start, Intervals.End);
                    else
                        MakeTree(Intervals.Start, end);
                }
                Intervals = Intervals.Next;
            }
            SetInterval(new Interval(start, end), StartInterval);
            return true;

        }
        public void MakeTree(int start, int end)
        {

            TreeNode node = new TreeNode(start, end);
            if(OverLapTree == null)
                OverLapTree = node;
            else
                AddTreeNode(node, OverLapTree);
        }
        public void AddTreeNode(TreeNode current, TreeNode root)
        {
            if (current.Start > root.End)
            {
                if (root.Right != null)
                    AddTreeNode(current, root.Right);
                else
                    root.Right = current;
            }
            if (current.End < root.Start)
            {
                if (root.Left != null)
                    AddTreeNode(current, root.Left);
                else
                    root.Left = current;
            }
        }
        public bool IsAccept(TreeNode current, TreeNode root)
        {
            if(current.Start >= root.End)
            {
                if (root.Right != null)
                    return IsAccept(current, root.Right);
                else
                    return true;
            }
            if(current.End <= root.Start)
            {
                if(root.Left!=null)
                    return IsAccept(current, root.Left);
                else
                    return true;
            }
            return false;
        }
        public void SetInterval(Interval current, Interval root)
        {
            if (root == null)
                StartInterval = current;
            else if (root.Start > current.Start)
            {
                if (root.Last != null)
                    SetInterval(current, root.Last);
                else
                {
                    current.Next = root;
                    root = current;
                    root.Next.Last = root;
                    StartInterval = root;
                }
            }
            else if (root.Next == null)
            {
                root.Next = current;
                root.Next.Last = root;
            }
            else if (root.Next.Start > current.Start)
            {
                current.Next = root.Next;
                root.Next.Last = current;
                root.Next = current;
                root.Next.Last = root;
            }
            else
                SetInterval(current, root.Next);

        }
        public class Interval
        {
            public Interval(int start, int end)
            {
                Start = start;
                End = end;
            }
            public int Start;
            public int End;
            public Interval Next;
            public Interval Last;
        }
        public class TreeNode
        {
            public TreeNode(int start, int end)
            {
                Start = start;
                End = end;
            }
            public int Start;
            public int End;
            public TreeNode Left;
            public TreeNode Right;
        }
    }
    public class MyCalendar1
    {
        public MyCalendar1() { }
        public Interval Intervals;
        public Interval StartInterval;
        public bool Book(int start, int end)
        {
            if (Intervals == null)
            {
                Intervals = new Interval(start, end);
                StartInterval = Intervals;
                return true;
            }
            while (Intervals.Start < start && Intervals.End <= start)
            {
                if (Intervals.Next == null)
                {
                    Intervals.Next = new Interval(start, end);
                    Intervals.Next.Last = Intervals;
                    Intervals = StartInterval;
                    return true;
                }
                Intervals.Next.Last = Intervals;
                Intervals = Intervals.Next;
            }
            if (Intervals.Start >= end)
            {
                if (Intervals.Last == null)
                {
                    StartInterval = new Interval(start, end);
                    StartInterval.Next = Intervals;
                    Intervals = StartInterval;
                    return true;
                }
                Intervals.Last.Next = new Interval(start, end);
                Intervals.Last.Next.Next = Intervals;
                Intervals = StartInterval;
                return true;
            }
            var testIntervals = Intervals;
            bool isOverlap = false;
            if (testIntervals.Next != null)
            {
                while (testIntervals.Next.Start < end)
                {
                    if (testIntervals.End > testIntervals.Next.Start)
                    {
                        if ( (testIntervals.Next.Start >= start && testIntervals.Next.Start < end)
                             ||(testIntervals.End >start && testIntervals.Start<end)
                            )
                        isOverlap = true;
                        break;
                    }
                    testIntervals = testIntervals.Next;
                    if (testIntervals.Next == null)
                        break;
                }
            }
            if (!isOverlap)
            {
                if(Intervals.Last==null)
                {
                    StartInterval = new Interval(start, end);
                    StartInterval.Next = Intervals;
                    Intervals = StartInterval;
                    return true;
                }
                while(Intervals.Last.Start> start)
                {
                    Intervals = Intervals.Last;
                    if (Intervals.Last == null)
                    {
                        StartInterval = new Interval(start, end);
                        StartInterval.Next = Intervals;
                        Intervals = StartInterval;
                        return true;
                    }
                }
                Intervals.Last.Next = new Interval(start, end);
                Intervals.Last.Next.Next = Intervals;
                Intervals = StartInterval;
                return true;
            }
            Intervals = StartInterval;
            return false;
        }
    }
        public class Interval
        {
            public Interval(int start, int end)
            {
                Start = start;
                End = end;
            }
            public int Start;
            public int End;
            public Interval Next;
            public Interval Last;

        }
}
