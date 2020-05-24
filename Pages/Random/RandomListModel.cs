using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SysRandom = System.Random;

namespace WillsToolsWasm.Pages.Random
{
    public class RandomViewModel
    {
        private static readonly SysRandom Random = new SysRandom();
        public InputType InputType { get; set; } = InputType.OneAtATime;

        public List<RandomItem> Items { get; set; } = new List<RandomItem>();
        public string Input { get; set; }
        public string RandomItem { get; set; }

        public void AddInputToList()
        {
            if (string.IsNullOrEmpty(Input))
            {
                return;
            }
            Items.Add(new RandomItem(Input));
            Input = null;
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
}

public enum InputType
{
    OneAtATime = 0,
    Range = 1
};