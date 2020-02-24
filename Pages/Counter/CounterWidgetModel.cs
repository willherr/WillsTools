using Microsoft.AspNetCore.Components;

namespace WillsToolsWasm
{
    public class CounterWidgetModel
    {
        private int id;
        private string name;
        private int currentCount;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnChange.InvokeAsync(this);
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = string.IsNullOrEmpty(value) ? "#" + Id : value;
                OnChange.InvokeAsync(this);
            }
        }
        public int CurrentCount
        {
            get => currentCount;
            set
            {
                currentCount = value;
                OnChange.InvokeAsync(this);
            }
        }

        public EventCallback<CounterWidgetModel> OnChange { get; set; }

        public CounterWidgetModel()
        {
            Id = 1;
            Name = "#" + Id;
        }

        public CounterWidgetModel(int id)
        {
            Id = id;
            Name = "#" + Id;
        }

        public CounterWidgetModel(int id, string name, int currentCount)
        {
            Id = id;
            Name = name;
            CurrentCount = currentCount;
        }
    }
}
