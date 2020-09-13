using Microsoft.AspNetCore.Components;

namespace WillsTools
{
    public class CounterWidgetModel
    {
        private int id;
        private string name;
        private bool isPlaceHolder;
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
                var setPlaceHolder = string.IsNullOrEmpty(value);
                name = setPlaceHolder ? "#" + Id : value;
                IsPlaceHolder = setPlaceHolder;
                OnChange.InvokeAsync(this);
            }
        }
        public bool IsPlaceHolder 
        {
            get => isPlaceHolder; 
            set
            {
                if (value)
                {
                    name += " (placeholder)";
                }
                else if (value != isPlaceHolder)
                {
                    name = name.Replace(" (placeholder)", string.Empty);
                }
                isPlaceHolder = value;
            }
        }
        public int CurrentCount
        {
            get => currentCount;
            set
            {
                currentCount = value;
                IsPlaceHolder = false;
                OnChange.InvokeAsync(this);
            }
        }

        public EventCallback<CounterWidgetModel> OnChange { get; set; }

        public CounterWidgetModel()
        {
            Id = 1;
            Name = "#" + Id;
            IsPlaceHolder = true;
        }

        public CounterWidgetModel(int id)
        {
            Id = id;
            Name = "#" + Id;
            IsPlaceHolder = true;
        }

        public CounterWidgetModel(int id, string name, int currentCount)
        {
            Id = id;
            Name = name;
            CurrentCount = currentCount;
        }
    }
}
