using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SysRandom = System.Random;

namespace WillsTools.Pages.Random
{
    public class RandomViewModel
    {
        private static readonly SysRandom Random = new SysRandom();
        public InputType InputType { get; set; } = InputType.OneAtATime;

        public List<RandomItem> Items { get; set; } = new List<RandomItem>();
        public string Input { get; set; }
        public string InputRangeStart { get; set; }
        public string InputRangeEnd { get; set; }
        public string RandomItem { get; set; }

        public void AddInputToList()
        {
            switch (InputType)
            {
                case InputType.OneAtATime:
                    if (!string.IsNullOrEmpty(Input))
                    {
                        Items.Add(new RandomItem(Input));
                        Input = null;
                    }
                    break;
                case InputType.Range:
                    if (!string.IsNullOrEmpty(InputRangeStart) && !string.IsNullOrEmpty(InputRangeEnd))
                    {
                        Items.AddRange(GetRandomRange(InputRangeStart, InputRangeEnd).Select(s => new RandomItem(s)));
                        InputRangeStart = null;
                        InputRangeEnd = null;
                    }
                    break;
                case InputType.Scramble:
                    if (!string.IsNullOrEmpty(Input))
                    {
                        var input = new List<char>(Input);
                        var n = input.Count;
                        while (n > 1)
                        {
                            n--;
                            var k = Random.Next(n + 1);
                            var value = input[k];
                            input[k] = input[n];
                            input[n] = value;
                        }
                        RandomItem = string.Join("", input);
                    }
                    else
                    {
                        RandomItem = "Enter something to scramble!";
                    }
                    break;
            }
        }

        public void Randomize()
        {
            if (Items.Count == 0)
            {
                RandomItem = "This button will randomly re-order your list.";
            }
            else
            {
                var n = Items.Count;
                while (n > 1)
                {
                    n--;
                    var k = Random.Next(n + 1);
                    var value = Items[k];
                    Items[k] = Items[n];
                    Items[n] = value;
                }

                RandomItem = "Randomly re-ordered your list!";
            }
        }

        public void Get()
        {
            var currentItems = Items.Where(i => !i.Popped);
            if (Items.Count == 0)
            {
                RandomItem = "This button will return a random item from your list.";
            }
            else if (currentItems.Any())
            {
                RandomItem = currentItems.ElementAt(Random.Next(currentItems.Count())).Name;
            }
            else
            {
                RandomItem = "You have no active items to get :(";
            }
        }

        public void Pop()
        {
            var currentItems = Items.Where(i => !i.Popped);
            if (Items.Count == 0)
            {
                RandomItem = "Pop is a coding term which means to remove the element on top and return it. This button means to remove a random item from your list and return it.";
            }
            else if (currentItems.Any())
            {
                var item = currentItems.ElementAt(Random.Next(currentItems.Count()));
                RandomItem = item.Name;
                item.Popped = true;
            }
            else
            {
                RandomItem = "You have no active items to pop :(";
            }
        }

        public void Reset(bool clearItems = true)
        {
            if (clearItems)
            {
                if (Items.Count == 0)
                {
                    RandomItem = "This button will remove all items from your list.";
                }
                else
                {
                    Items.Clear();
                    RandomItem = "Cleared your list!";
                }
            }
            else
            {
                if (Items.Count == 0)
                {
                    RandomItem = "This button will put your items back to original order and activate any that got popped.";
                }
                else
                {
                    Items = Items.OrderBy(i =>
                    {
                        i.Popped = false;
                        return i.Id;
                    }).ToList();
                    RandomItem = "Reset your list!";
                }
            }
        }

        // stolen from Will's Tools Android
        public List<string> GetRandomRange(string strStart, string strEnd)
        {
            // A1:Z13
            // A:F
            // 0:39
            // A1:24 X not sure how to interpret this

            List<List<string>> rangechunks = new List<List<string>>();

            string word1, word2;
            bool isNumeric1, isNumeric2;

            while (true)
            {
                strStart = GetAlphaOrNumericWord(strStart, out word1, out isNumeric1);
                strEnd = GetAlphaOrNumericWord(strEnd, out word2, out isNumeric2);

                if (isNumeric1 && isNumeric2) // both results are numeric
                {
                    rangechunks.Add(GetRange(int.Parse(word1), int.Parse(word2)));
                }
                else
                {
                    rangechunks.Add(GetRange(word1, word2));
                }

                // exit loop in this case           
                if (string.IsNullOrEmpty(strStart) || string.IsNullOrEmpty(strEnd))
                {
                    if (string.IsNullOrEmpty(strStart) && string.IsNullOrEmpty(strEnd) == false)
                    {
                        rangechunks.Add(new List<string> { strEnd });
                    }
                    else if (string.IsNullOrEmpty(strEnd) && string.IsNullOrEmpty(strStart) == false)
                    {
                        rangechunks.Add(new List<string> { strStart });
                    }

                    List<string> range = new List<string>();

                    foreach (List<string> rangeChunk in rangechunks)
                    {
                        if (range.Count == 0)
                        {
                            foreach (string str in rangeChunk)
                            {
                                range.Add(str);
                            }
                        }
                        else
                        {
                            List<string> nextRange = new List<string>();
                            foreach (string beginningWord in range)
                            {
                                nextRange = nextRange.Concat(GetPermutations(beginningWord, rangeChunk)).ToList();
                            }
                            range = nextRange;
                        }
                    }

                    return range;
                }
            }
        }

        public string GetAlphaOrNumericWord(string word, out string result, out bool isNumeric)
        {
            result = string.Empty;
            isNumeric = false;

            string newWord = string.Empty;
            bool foundLetter = false;
            foreach (char c in word)
            {
                if (char.IsDigit(c) && foundLetter == false)
                {
                    isNumeric = true;
                    result += c;
                }
                else if (char.IsDigit(c) == false && isNumeric == false)
                {
                    foundLetter = true;
                    result += c;
                }
                else
                {
                    newWord += c;
                }
            }

            return newWord;
        }

        private List<string> GetRange(string longerWord, string shorterWord)
        {
            if (longerWord.Length < shorterWord.Length)
            {
                string temp = longerWord;
                longerWord = shorterWord;
                shorterWord = temp;
            }

            List<List<string>> strResult = new List<List<string>>();
            for (int i = 0; i < shorterWord.Length; i++)
            {
                int start = (int)shorterWord[i];
                int end = (int)longerWord[i];

                if (start > end)
                {
                    int temp = start;
                    start = end;
                    end = temp;
                }

                List<string> strList = new List<string>();
                for (int j = start; j <= end; j++)
                {
                    strList.Add(((char)j).ToString());
                }

                strResult.Add(strList);
            }

            for (int i = shorterWord.Length; i < longerWord.Length; i++)
            {
                strResult.Add(new List<string>() { longerWord[i].ToString() });
            }

            List<string> rangeResult = new List<string>();
            foreach (List<string> strList in strResult)
            {
                if (rangeResult.Count == 0)
                {
                    foreach (string str in strList)
                    {
                        rangeResult.Add(str);
                    }
                }
                else
                {
                    List<string> nextRangeResult = new List<string>();
                    foreach (string beginningWord in rangeResult)
                    {
                        nextRangeResult = nextRangeResult.Concat(GetPermutations(beginningWord, strList)).ToList();
                    }
                    rangeResult = nextRangeResult;
                }
            }

            return rangeResult;
        }

        private List<string> GetPermutations(string beginningWord, List<string> nextPartList)
        {
            List<string> result = new List<string>();

            foreach (string str in nextPartList)
            {
                result.Add(string.Concat(beginningWord, str));
            }

            return result;
        }

        private List<string> GetRange(int start, int end)
        {
            List<string> rangeResult = new List<string>();

            if (start > end)
            {
                // swap these values to make sense
                int temp = start;
                start = end;
                end = temp;
            }

            for (int i = start; i <= end; i++)
            {
                rangeResult.Add(i.ToString());
            }

            return rangeResult;
        }
    }
}

public class RandomItem
{
    private static int idGenerator = 0;
    private static readonly object locker = new object();

    private int id;
    public int Id
    {
        get => id;
        set
        {
            lock (locker)
            {
                if (idGenerator < value)
                {
                    idGenerator = value;
                }
                id = value;
            }
        }
    }
    public string Name { get; set; }
    public bool Popped { get; set; }

    public RandomItem() { }
    public RandomItem(string name)
    {
        Id = Interlocked.Increment(ref idGenerator);
        Name = name;
    }
}

public enum InputType
{
    OneAtATime = 0,
    Range = 1,
    Scramble = 2,
    Word = 3
};