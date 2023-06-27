﻿namespace DC.Lab
{
    class Program
    {
        static Random r = new Random();
        static ContextProvider<Context> ctxProvider = new ContextProvider<Context>();

        static void Main(string[] args)
        {
            InitializeAndPrintContextAsync(1);
            InitializeAndPrintContextAsync(2);
            InitializeAndPrintContextAsync(3);

            Console.Read();
        }

        static async Task InitializeAndPrintContextAsync(int id)
        {
            var ctx = ctxProvider.InitContext();
            var guid = Guid.NewGuid();

            Console.WriteLine($"Init context for id: {id} - {guid}");

            ctx.Id = guid;

            await Task.Delay(r.Next(1000));

            ctx = ctxProvider.GetContext();
            Console.WriteLine($"Current context for id: {id} - {ctx.Id}");
        }
    }

    class ContextProvider<T> where T : new()
    {
        public T InitContext()
        {
            _asyncLocal.Value = new T();
            return _asyncLocal.Value;
        }

        public T GetContext() => _asyncLocal.Value!;

        private static readonly AsyncLocal<T> _asyncLocal = new AsyncLocal<T>();
    }

    public class Context
    {
        public Guid Id { get; set; }
    }
}