// Пример поведенческого шаблона - Итератор (Iterator)
// Позволяет получить последовательный доступ к элементам некой коллекции без использования описаний каждого из агрегированных объектов
using System;
using System.Collections.Generic;

namespace Behavioral_Pattern_ConsoleApp_Example_2
{
    // Elempoyee - класс работник
    class Elempoyee
    {
        // Публичные поля: ID, имя
        public int ID { get; set; }
        public string Name { get; set; }
        // Конструктор для Elempoyee
        public Elempoyee(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
    // Агрегатный интерфейс
    interface IAbstractCollection
    {
        // Следующий метод будет возвращать объект Iterator, который реализован ниже
        Iterator CreateIterator();
    }
    // ConcreteCollection - класс реализовывающий интерфейс Iterator для возврата экземпляра правильного ConcreteIterator
    class ConcreteCollection : IAbstractCollection
    {
        // Лист работников
        private List<Elempoyee> listEmployees = new List<Elempoyee>();
        // Реализация метода CreateIterator интерфейса IAbstractCollection
        // Метод создающий и возвращающий объект Iterator, который реализован ниже
        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }
        // Метод возвращающий количество элементов в коллекции
        public int Count
        {
            get { return listEmployees.Count; }
        }
        // Добавляем элементы в коллекцию
        public void AddEmployee(Elempoyee employee)
        {
            listEmployees.Add(employee);
        }
        // Получаем элементы из коллекции на основе позиции индекса (индекс начинается с 0)
        public Elempoyee GetEmployee(int IndexPosition)
        {
            return listEmployees[IndexPosition];
        }
    }
    // Интерфейс итератора, определяющий операции доступа и перемещения элементов в коллекции
    interface IAbstractIterator
    {
        Elempoyee First();
        Elempoyee Next();
        bool IsCompleted { get; }
    }
    // Iterator - класс итетератор
    class Iterator : IAbstractIterator
    {
        // Collection - переменная для хранения элементов коллекции
        private ConcreteCollection Collection;
        // Current - переменная используется в качестве позиции индекса для доступа к элементам коллекции
        private int Current = 0;
        // Step - переменная используется для перехода к следующему элементу от текущего элемента
        private readonly int Step = 1;
        // Конструктор для класса Iterator
        public Iterator(ConcreteCollection Collection)
        {
            // Инициализация переменной Collection с помощью конструктора
            this.Collection = Collection;
        }
        // Первый элемент коллекции
        public Elempoyee First()
        {
            // Установка Current равным 0 для доступа к первому элементу последовательности
            Current = 0;
            return Collection.GetEmployee(Current);
        }
        // Следующий элемент из коллекции
        public Elempoyee Next()
        {
            // Увеличь текущую позицию индекса на Step, для доступа к следующему элементу из коллекции
            Current += Step;
            if (!IsCompleted)
            {
                return Collection.GetEmployee(Current);
            }
            else
            {
                return null;
            }
        }
        // Проверrf, завершена ли итерация
        public bool IsCompleted
        {
            // Когда Current >= Collection.Count, это означает, что мы получили доступ ко всем элементам
            get { return Current >= Collection.Count; }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаём коллекцию
            ConcreteCollection collection = new ConcreteCollection();
            collection.AddEmployee(new Elempoyee("Наталья", 1000));
            collection.AddEmployee(new Elempoyee("Александр", 1001));
            collection.AddEmployee(new Elempoyee("Пётр", 1002));
            collection.AddEmployee(new Elempoyee("Василий", 1003));
            collection.AddEmployee(new Elempoyee("Иван", 1004));
            collection.AddEmployee(new Elempoyee("Мария", 1005));
            // Создаём итератор iterator
            Iterator iterator = collection.CreateIterator();
            // Вывод на экран коллекции
            Console.WriteLine("Проходим по коллекции:");
            for (Elempoyee emp = iterator.First(); !iterator.IsCompleted; emp = iterator.Next())
                Console.WriteLine($"ID: {emp.ID}\nИмя: {emp.Name}\n");
            Console.ReadKey();
        }
    }
}
