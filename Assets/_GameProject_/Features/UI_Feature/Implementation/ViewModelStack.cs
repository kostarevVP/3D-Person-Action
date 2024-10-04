using System.Collections.Generic;
using System.Linq;

namespace WKosArch.UI_Feature
{
    public class ViewModelStack<TreeNode> where TreeNode : ViewModelTreeNode
    {
        public int Length => _viewModelQueue.Count;
        public List<TreeNode> ViewModelQueue => _viewModelQueue;

        private List<TreeNode> _viewModelQueue;

        public ViewModelStack()
        {
            _viewModelQueue = new List<TreeNode>();
        }

        public void Push(TreeNode windowType)
        {
            bool queAllReadyHasIHomeWindow = Length == 1 && typeof(IHomeWindow).IsAssignableFrom(windowType.GetType());

            if (queAllReadyHasIHomeWindow)
                return;
            else
                _viewModelQueue.Add(windowType);
        }

        public TreeNode Pop()
        {
            TreeNode result = null;

            if (_viewModelQueue.Any())
            {
                var lastIndex = _viewModelQueue.Count - 1;

                result = _viewModelQueue[lastIndex];

                _viewModelQueue.RemoveAt(lastIndex);
            }

            return result;
        }

        public void Clear()
        {
            _viewModelQueue.Clear();
        }

        public void RemoveLast(TreeNode type)
        {
            if (_viewModelQueue.Contains(type))
            {
                var lastIndexOf = _viewModelQueue.LastIndexOf(type);

                _viewModelQueue.RemoveAt(lastIndexOf);
            }
        }

        public TreeNode GetLast()
        {
            return _viewModelQueue.Last();
        }

        internal void Remove(TreeNode windowType)
        {
            _viewModelQueue.Remove(windowType);
        }
    }
}