using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample1
{
    public static class DataSource
    {
        private static readonly object LockObj;
        private static readonly List<Order> Orders;
        private static int _nextId;

        static DataSource()
        {
            Orders = new List<Order>();
            LockObj = new object();
            _nextId = 1;
        }

        public static int Add(string name, double price)
        {
            lock (LockObj)
            {
                Orders.Add(new Order
                {
                    Id = _nextId,
                    Name = name,
                    Status = EStatus.New,
                    Price = price,
                });

                _nextId++;
            }


            return _nextId - 1;
        }

        public static void UpdateStatus(int id, EStatus status)
        {
            var order = GetOrder(id);
            order.Status = status;
        }

        public static Order GetOrder(int id)
        {
            var order = Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                throw new ArgumentException($"Invalid Id = {id}", nameof(id));
            }

            return order;
        }
    }

    public class Order
    {
        public int Id { set; get; }

        public string Name { get; set; }

        public EStatus Status { set; get; }

        public double Price { set; get; }
    }

    public enum EStatus
    {
        /// <summary>
        /// Новая заявка
        /// </summary>
        New,

        /// <summary>
        /// Проверка информации
        /// </summary>
        CheckInfo,
   
        /// <summary>
        /// Запрос информации
        /// </summary>
        WaitingInfo,
        
        /// <summary>
        /// Ожидание оплаты
        /// </summary>
        WaitingForPayment,
        
        /// <summary>
        /// Проверка оплаты
        /// </summary>
        CheckPayment,
        
        /// <summary>
        /// Заявка завершена
        /// </summary>
        Complete,
    }
}
