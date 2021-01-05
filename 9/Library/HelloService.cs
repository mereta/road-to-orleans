using System;

namespace Library
{
    public class HelloService:IHelloService
    {
        public string Say(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? "string cannot be null or whitespace" : $"Hello, {name}! Your name is {name.Length} characters long.";
        }
    }

    public interface IHelloService
    {
        string Say(string name);
    }
}