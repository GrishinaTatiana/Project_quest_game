using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Godot;

namespace BloodTribute
{
    public static class ScuffedServiceProvider
    {
        static Dictionary<Type, object> services;

        static ScuffedServiceProvider()
        {
            services = new();

            services[typeof(IMessagePrinter)] = new ConsolePrinter();
        }

        public static T GetService<T>()
        {
            return (T)services[typeof(T)];
        }
    }



    public interface IMessagePrinter
    {
        void PrintMessage(string message);
    }

    public class ConsolePrinter : IMessagePrinter
    {
        public void PrintMessage(string message)
        {
           GD.Print(message);
        }
    }
}
