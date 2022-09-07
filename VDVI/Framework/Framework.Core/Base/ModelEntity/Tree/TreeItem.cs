using System.Collections.Generic;
using Newtonsoft.Json;

namespace Framework.Core.Base.ModelEntity.Tree
{
    public class TreeItem<T>
    {
        [JsonProperty("item")]
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Children { get; set; }
    }
}
