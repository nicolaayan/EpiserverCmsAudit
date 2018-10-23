using EPiServer.Core;
using EPiServer.Data;
using EPiServer.Data.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N1990.Episerver.Cms.Audit.Models
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    public class VisitorGroupUse : IDynamicData
    {
        public Identity Id
        {
            get; set;
        }

        public DateTime Seen { get; set; }

        public string VisitorGroup { get; set; }

        public ContentReference Content { get; set; }

        public string PropertyName { get; set; }

        public string ContentType { get; set; }

        public string ContentName { get; set; }

        public static IEnumerable<VisitorGroupUse> ListForVisitorGroup(string vg)
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(VisitorGroupUse));
            return store.Find<VisitorGroupUse>("VisitorGroup", vg);
        }

        public static void Save(VisitorGroupUse vguse)
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(VisitorGroupUse));
            store.Save(vguse);
        }

        public static void CleanUp()
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(VisitorGroupUse));
            store.DeleteAll();
        }

    }
}
