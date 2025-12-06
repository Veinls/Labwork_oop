using labvork_2_csharp.Core.Items.Interfaces;

namespace labvork_2_csharp.Core.Items.Builders
{
    public abstract class BaseItemBuilder<T, TBuilder>
        where TBuilder : BaseItemBuilder<T, TBuilder>
        where T:  IItem
    {
        protected string? _id;
        protected string? _name;
        protected string? _description;

        public TBuilder SetId(string id)
        {
            _id = id ?? throw new ArgumentNullException(nameof(id));
            return (TBuilder)this;
        }

        public TBuilder SetName(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            return (TBuilder)this;
        }

        public TBuilder SetDescription(string description)
        {
            _description = description;
            return (TBuilder)this;
        }

        protected virtual void Validate()
        {
            if (string.IsNullOrWhiteSpace(_id))
            {
                throw new InvalidOperationException("ID не установлен");
            }

            if (string.IsNullOrWhiteSpace(_name))
            {
                throw new InvalidOperationException("Имя не установлено");
            }
        }
        
        public abstract T Build();
    }
}