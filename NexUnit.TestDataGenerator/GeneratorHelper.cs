using System.Linq;

namespace NextUnit.Autofixture.AutoMoq.Core
{
    public class Generator<T>
    {
        private readonly Func<Random, T> generate;

        public Generator(Func<Random, T> generate)
        {
            if (generate == null)
                throw new ArgumentNullException(nameof(generate));

            this.generate = generate;
        }

        public Generator<T1> Select<T1>(Func<T, T1> f)
        {
            if (f == null)
                throw new ArgumentNullException(nameof(f));

            Func<Random, T1> newGenerator = r => f(this.generate(r));
            return new Generator<T1>(newGenerator);
        }

        public T Generate(Random random)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random));

            return this.generate(random);
        }
    }

    public static class GeneratorHelper
    {
        //public Generator<TResult> SelectMany<TResult>(Func<T, Generator<TResult>> selector)
        //{
        //    Func<Random, TResult> newGenerator = r =>
        //    {
        //        Generator<TResult> g = selector(generate(r));
        //        return g.Generate(r);
        //    };
        //    return new Generator<TResult>(newGenerator);
        //}
                //public Generator<TResult> SelectMany<U, TResult>(
        //    Func<T, Generator<U>> k,
        //    Func<T, U, TResult> s)
        //{
        //    return SelectMany(x => k(x).Select(y => s(x, y)));
        //}

        //public static Generator<T> Flatten<T>(this Generator<Generator<T>> generator)
        //{
        //    return generator.SelectMany(x => x);
        //}

        //public static Generator<T> Return<T>(T value)
        //{
        //    return new Generator<T>(_ => value);
        //}

        public static Generator<T> Elements<T>(params T[] alternatives)
        {
            if (alternatives == null)
                throw new ArgumentNullException(nameof(alternatives));

            return new Generator<T>(r =>
            {
                var index = r.Next(alternatives.Length);
                return alternatives[index];
            });
        }

        public static Generator<TResult> Apply<T, TResult>(this Generator<Func<T, TResult>> selectors, Generator<T> generator)
        {
            if (selectors == null)
            {
                throw new ArgumentNullException(nameof(selectors));
            }
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            Func<Random, TResult> newGenerator = r =>
            {
                var f = selectors.Generate(r);
                var x = generator.Generate(r);
                return f(x);
            };
            return new Generator<TResult>(newGenerator);
        }
    }
}
