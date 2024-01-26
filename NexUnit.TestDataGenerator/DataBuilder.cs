using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexUnit.TestDataGenerator
{
    public class Builder<T>
    {
        private readonly T item;

        public Builder(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.item = item;
        }

        public Builder<T1> Select<T1>(Func<T, T1> f)
        {
            var newItem = f(this.item);
            return new Builder<T1>(newItem);
        }

        public T Build()
        {
            return this.item;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Builder<T>;
            if (other == null)
                return base.Equals(obj);

            return object.Equals(this.item, other.item);
        }

        public override int GetHashCode()
        {
            return this.item.GetHashCode();
        }
    }
}
