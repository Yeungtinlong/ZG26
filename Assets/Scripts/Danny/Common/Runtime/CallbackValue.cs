using System;
using Newtonsoft.Json;

namespace SupportUtils
{
    [Serializable]
    public class CallbackValue<T>
    {
        public event Action<T> OnValueChanged;

        [JsonProperty("value")]
        private T _value;

        [JsonIgnore]
        public T Value
        {
            get => _value;
            set
            {
                if (value.Equals(_value))
                {
                    return;
                }

                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        public CallbackValue() : this(default) { }

        public CallbackValue(T defaultValue)
        {
            _value = defaultValue;
        }

        public void SetValue(T value, bool notify)
        {
            _value = value;
            if (notify)
            {
                OnValueChanged?.Invoke(_value);
            }
        }
    }
}