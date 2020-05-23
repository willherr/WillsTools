using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SysRandom = System.Random;

namespace WillsToolsWasm.Pages.Random
{
    public class RandomListModel
    {
        private static readonly SysRandom Random = new SysRandom();
        private Func<Task<string>> getRandomWord;
        private Action stateHasChanged;

        public List<string> Items { get; set; } = new List<string>();
        public string Input { get; set; }
        public string RandomItem { get; set; }

        public RandomListModel(Func<Task<string>> getRandomWord, Action stateHasChanged)
        {
            this.getRandomWord = getRandomWord;
            this.stateHasChanged = stateHasChanged;
        }

        public void AddInputToList()
        {
            Items.Add(Input);
            Input = null;
        }

        public void Randomize()
        {
            if (Items.Count == 0)
            {
                getRandomWord().ContinueWith(randomWord =>
                {
                    RandomItem = randomWord.Result;
                    stateHasChanged();
                });
            }
            else
            {
                RandomItem = Items[Random.Next(Items.Count)];
            }
        }
    }
}
