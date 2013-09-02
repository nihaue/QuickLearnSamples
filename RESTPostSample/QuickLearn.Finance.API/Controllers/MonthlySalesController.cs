using QuickLearn.Finance.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace QuickLearn.Finance.API.Controllers
{
    public class MonthlySalesController : ApiController
    {

        public static Dictionary<int, decimal> monthlySales = new Dictionary<int, decimal>();

        // GET api/MonthlySales
        public IEnumerable<string> Get()
        {
            return monthlySales.Select(m => m.Value.ToString("C")).ToArray();
        }

        // GET api/MonthlySales/8
        public string Get(int month)
        {
            return monthlySales[month].ToString("C");
        }

        // POST api/MonthlySales
        public void Post([FromBody]Sale sale)
        {

            var curMonth = DateTime.UtcNow.Month;
            var salesAmount = Convert.ToDecimal(sale.Amount);

            if (monthlySales.ContainsKey(curMonth))
            {
                monthlySales[curMonth] += salesAmount;
            }
            else
            {
                monthlySales.Add(curMonth, salesAmount);
            }

        }

        // PUT api/MonthlySales/5
        public void Put(int id, [FromBody]Sale sale)
        {
            monthlySales[id] = Convert.ToDecimal(sale.Amount);
        }

        // DELETE api/MonthlySales/5
        public void Delete(int id)
        {
            monthlySales[id] = 0M;
        }

    }
}