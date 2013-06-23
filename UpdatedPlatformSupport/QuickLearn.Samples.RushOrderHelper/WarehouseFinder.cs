using System;

namespace QuickLearn.Samples.RushOrderHelper
{
    public static class WarehouseFinder
    {

        /// <summary>
        /// Gets the warehouse that is currently picking orders
        /// or the next one that will be picking orders
        /// </summary>
        /// <returns>String containing the warehouse identifier</returns>
        public static string GetNextReadyWarehouse()
        {
            string warehouseId = string.Empty;

            if (DateTime.UtcNow.TimeOfDay >= new TimeSpan(0, 0, 0) &&
                    DateTime.UtcNow.TimeOfDay < new TimeSpan(8, 0, 0))
            {
                warehouseId = "WHA123";
            }
            else if (DateTime.UtcNow.TimeOfDay >= new TimeSpan(8, 0, 0) &&
                    DateTime.UtcNow.TimeOfDay < new TimeSpan(16, 0, 0))
            {
                warehouseId = "WHB456";
            }
            else
            {
                warehouseId = "WHC789";
            }

            return warehouseId;
        }

    }
}
