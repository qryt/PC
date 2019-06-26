using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxApplication5
{
    class ActiveMq
    {
        /*
        static void Main(string[] args)
        {
            ActiveMq p = new ActiveMq();
            //p.Producer();
            p.Cosumer();

        }
        */
        public void Producer()
        {
            IConnectionFactory factory = new ConnectionFactory("tcp://127.0.0.1:61616");
            using (IConnection connection = factory.CreateConnection())
            {
                using (ISession session = connection.CreateSession())
                {
                    IMessageProducer prod = session.CreateProducer(new ActiveMQTopic("testing"));
                    int i = 0;
                    while (!Console.KeyAvailable)
                    {
                        ITextMessage msg = prod.CreateTextMessage();
                        msg.Text = i.ToString();
                        Console.WriteLine("Sending:" + i.ToString());
                        prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.VeryHigh, TimeSpan.MinValue);
                        System.Threading.Thread.Sleep(5000);
                        i++;
                    }
                }
            }
            Console.ReadLine();
        }

        public void Cosumer()
        {
            try
            {
                IConnectionFactory factory = new ConnectionFactory("tcp://127.0.0.1:61616");
                using (IConnection connection = factory.CreateConnection())
                {
                    connection.ClientId = "test1";
                    connection.Start();
                    using (ISession session = connection.CreateSession())
                    {
                        Console.WriteLine(11111111111);
                        IMessageConsumer consumer = session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("testing"), "test1", null, false);
                        Console.WriteLine(222222222222);
                        consumer.Listener += new MessageListener(Consumer_Listener);
                        Console.WriteLine(333333333);
                        Console.ReadLine();
                    }
                    Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                    connection.Stop();
                    connection.Close();
                }
            }
            catch
            {

            }
        }

        static void Consumer_Listener(IMessage Message)
        {
            try
            {
                ITextMessage msg = (ITextMessage)Message;
                Console.WriteLine("Consumer" + msg.Text);
            }
            catch
            {

            }
        }
    }
}
