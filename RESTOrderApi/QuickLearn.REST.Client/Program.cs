using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;

namespace QuickLearn.REST.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();
            order.Id = "12345";
            order.Purchaser = "Alice Bobson";
            order.Items = new OrderItem[] { new OrderItem() { Id = "12345", Qty = 1, Price = 5.95M } };

            XmlSerializer ser = new XmlSerializer(typeof(Order));
            MemoryStream outputStream = new MemoryStream();
            ser.Serialize(outputStream, order);

            StringContent serializedOrder = new StringContent(
                Encoding.Unicode.GetString(outputStream.ToArray()),
                Encoding.Unicode,
                "application/xml");

            Console.WriteLine("Submitting order...");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/services/orderprocessing/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.PostAsync("api/orders/", serializedOrder).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request was successful.");
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine("Request failed.");
            }

            Console.ReadLine();
        }
    }
}
