using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WillsToolsWasm.Pages.TestCode
{
    public class TestCodeModel
    {
        public IEnumerable<ChildItem> RunSingleJoinTest()
        {
            var parents = new List<ParentItem>
            {
                new ParentItem(1),
                new ParentItem(2),
                new ParentItem(4)
            };

            var children = new List<ChildItem>
            {
                new ChildItem(1, 1, 1),
                new ChildItem(2, 2, 2),
                new ChildItem(3, 2, 2)
            };

            return (children, parents).SingleJoin(
                child => child.ParentId, 
                parent => parent.Id, 
                (child, parent) => child.Parent = parent);
        }

        public IEnumerable<ParentItem> RunManyJoinTest()
        {
            var parents = new List<ParentItem>
            {
                new ParentItem(1),
                new ParentItem(2),
                new ParentItem(4)
            };

            var children = new List<ChildItem>
            {
                new ChildItem(1, 1, 1),
                new ChildItem(2, 2, 2),
                new ChildItem(3, 2, 2)
            };

            return (parents, children).ManyJoin(
                parent => parent.Id, 
                child => child.ParentId, 
                (parent, parentChildren) => parent.Children = parentChildren);
        }
    }

    public static class JoinExtensions
    {
        public static IEnumerable<T> SingleJoin<T, R, Key>(this (IEnumerable<T>, IEnumerable<R>) self, Func<T, Key> keySelector, Func<R, Key> foreignKeySelector, Action<T, R> assignmentAction)
        {
            var tDict = self.Item1.GroupBy(keySelector).ToDictionary(tGroup => tGroup.Key, tGroup => tGroup);
            var rGroups = self.Item2.GroupBy(foreignKeySelector);

            foreach (var rGroup in rGroups)
            {
                if (tDict.TryGetValue(rGroup.Key, out var tGroup))
                {
                    foreach (var t in tGroup)
                    {
                        assignmentAction(t, rGroup.Single()); // single as there should only be one match
                    }
                }
            }

            return self.Item1;
        }

        public static IEnumerable<T> ManyJoin<T, R, Key>(this (IEnumerable<T>, IEnumerable<R>) self, Func<T, Key> keySelector, Func<R, Key> foreignKeySelector, Action<T, IEnumerable<R>> assignmentAction)
        {
            var tDict = self.Item1.GroupBy(keySelector).ToDictionary(tGroup => tGroup.Key, tGroup => tGroup);
            var rGroups = self.Item2.GroupBy(foreignKeySelector);

            foreach (var rGroup in rGroups)
            {
                if (tDict.TryGetValue(rGroup.Key, out var tGroup))
                {
                    foreach (var t in tGroup)
                    {
                        assignmentAction(t, rGroup); 
                    }
                }
            }

            return self.Item1;
        }
    }

    public class ParentItem
    {
        public int Id { get; set; }

        public IEnumerable<ChildItem> Children { get; set; }

        public ParentItem(int id)
        {
            Id = id;
        }
    }

    public class ChildItem
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int SiblingId { get; set; }

        public ParentItem Parent { get; set; }
        public SiblingItem Sibling { get; set; }

        public ChildItem(int id, int parentId, int siblingId)
        {
            Id = id;
            ParentId = parentId;
            SiblingId = siblingId;
        }
    }

    public class SiblingItem
    {
        public int Id { get; set; }

        public SiblingItem(int id)
        {
            Id = id;
        }
    }
}
