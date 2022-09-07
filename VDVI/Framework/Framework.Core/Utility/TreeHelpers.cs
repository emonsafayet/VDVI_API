using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Core.Base.ModelEntity.Tree;

namespace Framework.Core.Utility
{
    public static class TreeHelpers
    {
        /// <summary>
        /// Generates tree of items from item list
        /// </summary>
        /// 
        /// <typeparam name="T">Type of item in collection</typeparam>
        /// <typeparam name="TK">Type of parent_id</typeparam>
        /// 
        /// <param name="collection">Collection of items</param>
        /// <param name="idSelector">Function extracting item's id</param>
        /// <param name="parentIdSelector">Function extracting item's parent_id</param>
        /// <param name="rootId">Root element id</param>
        /// 
        /// <returns>Tree of items</returns>
        public static IEnumerable<TreeItem<T>> GenerateTree<T, TK>(this IEnumerable<T> collection, Func<T, TK> idSelector, Func<T, TK> parentIdSelector, TK rootId = default(TK))
        {
            var enumerable = collection.ToList();
            foreach (var c in enumerable.Where(c => parentIdSelector(c).Equals(rootId)))
            {
                yield return new TreeItem<T>
                {
                    Item = c,
                    Children = enumerable.GenerateTree(idSelector, parentIdSelector, idSelector(c))
                };
            }
        }
    }
}
