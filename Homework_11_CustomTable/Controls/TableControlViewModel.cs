using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Layout;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Homework_11_CustomTable.Controls
{
    public class Node
    {
        public ObservableCollection<Node>? SubNodes { get; } = new ObservableCollection<Node>();
        public string Title { get; }

        public Node(string title)
        {
            Title = title;
        }

        public Node(string title, ObservableCollection<Node> subNodes)
        {
            Title = title;
            SubNodes = subNodes;
        }
    }

    public partial class TableControlViewModel : ObservableObject
    {
        public ObservableCollection<Node> Nodes { get; set; } = new ObservableCollection<Node>();
        [ObservableProperty] public object? _contents = null;

        public void FormContent(Expander? parent, IEnumerable<Node>? nodes)
        {
            if (nodes == null || !nodes.Any()) return;

            var itemsControl = new ItemsControl();
            foreach (var node in nodes)
            {
                if (node.SubNodes != null && node.SubNodes.Any())
                {
                    var nodeExpander = new Expander
                    {
                        Header = node.Title,
                        Margin = new Avalonia.Thickness(10),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };
                    FormContent(nodeExpander, node.SubNodes);
                    itemsControl.ItemsSource = itemsControl.Items.Append(nodeExpander);
                }
                else
                {
                    var textBlock = new TextBox()
                    {
                        Text = node.Title,
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };
                    itemsControl.ItemsSource = itemsControl.Items.Append(textBlock);
                }
            }

            if (parent == null)
            {
                _contents = itemsControl;
            }
            else
            {
                parent.Content = itemsControl;
            }
        }

        public void ParseObject(object? obj, ObservableCollection<Node>? branch = null)
        {
            if (obj == null) return;

            if (branch == null)
            {
                branch = Nodes;
                branch.Clear();
            }

            var type = obj.GetType();
            var rootNode = new Node(type.Name);
            branch.Add(rootNode);

            if (type.IsClass && type != typeof(string))
            {
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    var propertyValue = property.GetValue(obj);
                    var nodeTitle = $"{property.Name}: {propertyValue}";

                    if (propertyValue != null)
                    {
                        if (propertyValue.GetType().IsClass && propertyValue.GetType() != typeof(string))
                        {
                            var subNode = new Node(property.Name);
                            rootNode.SubNodes.Add(subNode);
                            ParseObject(propertyValue, subNode.SubNodes);
                        }
                        else
                        {
                            rootNode.SubNodes.Add(new Node(nodeTitle));
                        }
                    }
                    else
                    {
                        rootNode.SubNodes.Add(new Node($"{property.Name}: null"));
                    }
                }
            }
        }

    }
}