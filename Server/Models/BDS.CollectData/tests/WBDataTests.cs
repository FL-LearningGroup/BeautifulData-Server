using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BDS.CollectData;
using BDS.CollectData.Models;
using BDS.CollectData.BDSException;
using BDS.CollectData.DataStructure;

namespace BDS.CollectData.Tests
{
    public class WBDataTests
    {
        [Fact]
        [Obsolete("Not used any more", true)]
        public void OperateWBDataTest()
        {

            WBData wbData = new WBData();
            List<string> list1 = new List<string> {"k1", "k2", "k3" };
            List<string> list2 = new List<string> { "k1", "k2", "k3" };
            List<string> list3 = new List<string> { "k1", "k2", "k3" };
            List<string> list4 = new List<string> { "k1", "k2", "k3" };
            string key1 = "key1";
            string key2 = "key2";
            string key3 = "key3";
            string key4 = "key4";
            wbData.StoreOrReplaceElementData(key1, list1);
            wbData.StoreOrReplaceElementData(key2, list2);
            wbData.StoreOrReplaceElementData(key3, list2);
            Assert.Equal(wbData.Data.Count, 3);
            wbData.StoreOrReplaceElementData(key3, list1);
            Assert.Equal(wbData.Data.Count, 3);

            var list = wbData.GeElementDataName(key1);
            Assert.Equal(list.Count, 3);

            var count = wbData.AddElementData(key1, "k4");
            Assert.Equal(count, 4);

        }
    }
}
