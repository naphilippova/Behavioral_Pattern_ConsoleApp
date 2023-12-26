// Пример поведенческого шаблона - Посредник (Mediator)
// Позволяет централизованно управлять связями и коммуникацией между объектами, снижая их зависимость друг от друга
using System;
using System.Collections.Generic;

namespace Behavioral_Pattern_ConsoleApp_Example_1
{
    //Интерфейс посредника, определяющий операции которые могут быть вызваны объектами-коллегами для связи.
    public interface IMediator
    {
        // Этот метод используется для отправки сообщения
        void SendMessage(string message, Colleague colleague);
    }
    // Colleague - абстрактный класс, определяющий свойство, содержащее ссылку на посредник
    public abstract class Colleague
    {
        protected IMediator m_mediator;
        public Colleague(IMediator mediator)
        {
            m_mediator = mediator;
        }
        // Методы ReceiveMessage и SendMessage будут реализованы конкретным коллегой
        public abstract void ReceiveMessage(string message);
        public virtual void SendMessage(string message)
        {
            m_mediator.SendMessage(message, this);
        }
    }
    // ConcreteColleague1 - конкретный класс, которые общаются с другим через посредника
    public class ConcreteColleague1 : Colleague
    {
        public ConcreteColleague1(IMediator mediator) : base(mediator) { }
        public override void ReceiveMessage(string message)
        {
            Console.WriteLine($"Коллега 1 получил сообщение: {message}");
        }
    }
    // ConcreteColleague2 - конкретный класс, которые общаются с другим через посредника
    public class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(IMediator mediator) : base(mediator) { }
        public override void ReceiveMessage(string message)
        {
            Console.WriteLine($"Коллега 2 получил сообщение: {message}");
        }
    }
    // Реализация посредника
    public class ConcreteMediator : IMediator
    {
        private List<Colleague> m_colleagues = new List<Colleague>();
        public void AddColleague(Colleague colleague)
        {
            m_colleagues.Add(colleague);
        }
        public void SendMessage(string message, Colleague colleague)
        {
            foreach (var col in m_colleagues)
                if (col != colleague)
                    col.ReceiveMessage(message);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаём экземпляр медиатора
            var mediator = new ConcreteMediator();
            // Создаём коллегу1
            var Natasha = new ConcreteColleague1(mediator);
            // Создаём коллегу2
            var Petr = new ConcreteColleague2(mediator);
            mediator.AddColleague(Natasha);
            mediator.AddColleague(Petr);
            Natasha.SendMessage("Привет от коллеги 1");
            Petr.SendMessage("Привет от коллеги 2");
            Console.ReadKey();
        }
    }
}
