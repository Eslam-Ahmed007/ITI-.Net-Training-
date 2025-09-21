using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day_5_6_task
{
    // ---------- Task 1: Optimized Bubble Sort ----------
    class BubbleSortOptimized
    {
        public static void Sort(int[] arr)
        {
            int n = arr.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;

                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }
        }
    }

    // ---------- Task 2: Generic Range<T> ----------
    class Range<T> where T : IComparable<T>
    {
        public T Min { get; }
        public T Max { get; }

        public Range(T min, T max)
        {
            if (min.CompareTo(max) > 0)
                throw new ArgumentException("Min cannot be greater than Max");
            Min = min;
            Max = max;
        }

        public bool IsInRange(T value)
        {
            return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
        }

        public double Length()
        {
            return Convert.ToDouble(Max) - Convert.ToDouble(Min);
        }
    }

    // ---------- Task 3: Reverse ArrayList ----------
    class ArrayListReverser
    {
        public static void Reverse(ArrayList list)
        {
            int left = 0, right = list.Count - 1;

            while (left < right)
            {
                object temp = list[left];
                list[left] = list[right];
                list[right] = temp;
                left++;
                right--;
            }
        }
    }

    // ---------- Task 4: Get Even Numbers ----------
    class EvenNumbers
    {
        public static List<int> GetEvenNumbers(List<int> numbers)
        {
            return numbers.Where(x => x % 2 == 0).ToList();
        }
    }

    // ---------- Task 5: FixedSizeList<T> ----------
    class FixedSizeList<T>
    {
        private T[] items;
        private int count;

        public FixedSizeList(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero");
            items = new T[capacity];
            count = 0;
        }

        public void Add(T item)
        {
            if (count >= items.Length)
                throw new InvalidOperationException("List is full. Cannot add more items.");
            items[count++] = item;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Invalid index.");
            return items[index];
        }
    }

    // ---------- Task 6: First Non-Repeated Character ----------
    class FirstUniqueChar
    {
        public static int GetFirstUniqueIndex(string s)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();

            foreach (char c in s)
            {
                if (freq.ContainsKey(c))
                    freq[c]++;
                else
                    freq[c] = 1;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (freq[s[i]] == 1)
                    return i;
            }

            return -1;
        }
    }

   
    internal class Program
    {
        static void Main(string[] args)
        {
            // Task 1: Optimized Bubble Sort
            Console.WriteLine("Task 1: Optimized Bubble Sort");
            int[] arr = { 5, 1, 4, 2, 8 };
            BubbleSortOptimized.Sort(arr);
            Console.WriteLine("Sorted Array: " + string.Join(", ", arr));

            // Task 2: Range<T>
            Console.WriteLine("\nTask 2: Generic Range<T>");
            Range<int> range = new Range<int>(10, 20);
            Console.WriteLine($"Is 15 in range? {range.IsInRange(15)}");
            Console.WriteLine($"Length: {range.Length()}");

            // Task 3: Reverse ArrayList
            Console.WriteLine("\nTask 3: Reverse ArrayList");
            ArrayList list = new ArrayList() { 1, 2, 3, 4, 5 };
            ArrayListReverser.Reverse(list);
            Console.WriteLine("Reversed ArrayList: " + string.Join(", ", list.ToArray()));

            // Task 4: Even Numbers
            Console.WriteLine("\nTask 4: Get Even Numbers");
            List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> evens = EvenNumbers.GetEvenNumbers(nums);
            Console.WriteLine("Even Numbers: " + string.Join(", ", evens));

            // Task 5: FixedSizeList<T>
            Console.WriteLine("\nTask 5: FixedSizeList<T>");
            FixedSizeList<string> fixedList = new FixedSizeList<string>(3);
            fixedList.Add("Hello");
            fixedList.Add("World");
            fixedList.Add("!");
            Console.WriteLine("FixedSizeList item at index 1: " + fixedList.Get(1));

            // Task 6: First Non-Repeated Character
            Console.WriteLine("\nTask 6: First Non-Repeated Character");
            string str = "swiss";
            int index = FirstUniqueChar.GetFirstUniqueIndex(str);
            Console.WriteLine($"First unique char index in '{str}': {index}");

          
        }
    }
}
