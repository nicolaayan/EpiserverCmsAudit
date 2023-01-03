using EPiServer.Data;
using EPiServer.Data.Dynamic;
using System;
using System.Linq;

namespace N1990.Episerver.Cms.Audit.Models
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    public class BlockTypeUse : IDynamicData
    {
        public Identity Id
        {
            get; set;
        }

        public DateTime Seen { get; set; }

        public int ContentTypeId { get; set; }

        public string AuditJson { get; set; }

        public static BlockTypeUse Get(int contentTypeId)
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(BlockTypeUse));
            return store.Find<BlockTypeUse>("ContentTypeId", contentTypeId).FirstOrDefault();
        }

        public static void Save(BlockTypeUse blockTypeUse)
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(BlockTypeUse));
            store.Save(blockTypeUse);
        }

        public static void CleanUp()
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(BlockTypeUse));
            store.DeleteAll();
        }
    }
}
