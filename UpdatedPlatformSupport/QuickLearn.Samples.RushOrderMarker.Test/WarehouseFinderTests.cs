using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace QuickLearn.Samples.Test
{
    [TestClass]
    public class WarehouseFinderTests
    {

        public WarehouseFinderTests()
        {
            WarehouseIdentifiers.Add("WHA123");
            WarehouseIdentifiers.Add("WHB456");
            WarehouseIdentifiers.Add("WHC789");
        }

        public List<string> WarehouseIdentifiers = new List<string>();

        [TestMethod]
        public void GetNextReadyWarehouse_TimeZoneOne_FirstWarehouseReturned()
        {

            DateTime fakeTime = new DateTime(
                            year: 2013,
                            month: 01,
                            day: 01,
                            hour: 1, minute: 0, second: 0);

            using (ShimsContext.Create())
            {

                System.Fakes.ShimDateTime.UtcNowGet = () => { return fakeTime; };
                
                var expectedId = WarehouseIdentifiers[0];

                var actualId = RushOrderHelper.WarehouseFinder.GetNextReadyWarehouse();

                Assert.AreEqual(expectedId, actualId);

            }

        }

        [TestMethod]
        public void GetNextReadyWarehouse_TimeZoneTwo_SecondWarehouseReturned()
        {
            DateTime fakeTime = new DateTime(
                            year: 2013,
                            month: 01,
                            day: 01,
                            hour: 9, minute: 0, second: 0);

            using (ShimsContext.Create())
            {

                System.Fakes.ShimDateTime.UtcNowGet = () => { return fakeTime; };

                var expectedId = WarehouseIdentifiers[1];

                var actualId = RushOrderHelper.WarehouseFinder.GetNextReadyWarehouse();

                Assert.AreEqual(expectedId, actualId);

            }
        }

        [TestMethod]
        public void GetNextReadyWarehouse_TimeZoneThree_ThirdWarehouseReturned()
        {
            DateTime fakeTime = new DateTime(
                            year: 2013,
                            month: 01,
                            day: 01,
                            hour: 17, minute: 0, second: 0);

            using (ShimsContext.Create())
            {

                System.Fakes.ShimDateTime.UtcNowGet = () => { return fakeTime; };

                var expectedId = WarehouseIdentifiers[2];

                var actualId = RushOrderHelper.WarehouseFinder.GetNextReadyWarehouse();

                Assert.AreEqual(expectedId, actualId);

            }

        }

    }
}
