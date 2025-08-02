using System.Collections.Generic;
using Enumerations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Object = System.Object;

namespace Editor.NodeHierarchyEditor.NodeSystem.Elements
{
    public class NhNode : Node
    {
        public string GameObjectName { get; set; }
        public string ComponentName { get; set; }
        public List<Object> Components { get; set; }
        
        public NodeHierarchyType NodeType { get; set; }

        public void Initialize(Vector2 position)
        {
            GameObjectName = "Default";
            ComponentName = "Default Component";
            Components = new List<Object>();
            
            SetPosition(new Rect(position,Vector2.zero));
        }

        public void Draw()
        {
            DrawTitleContainer();

            DrawInputContainer();

            DrawExtensionsContainer();
        }

        private void DrawTitleContainer()
        {
            /* TITLE CONTAINER */
            var titleTextField = new TextField
            {
                value = GameObjectName
            };

            titleContainer.Insert(0, titleTextField);
        }

        private void DrawInputContainer()
        {
            /* INPUT CONTAINER */

            var inputPort =
                InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));

            inputPort.portName = "Child Input";

            inputContainer.Add(inputPort);
        }

        private void DrawExtensionsContainer()
        {
            /* EXTENSIONS CONTAINER */

            VisualElement customDataContainer = new VisualElement();

            var textFoldout = new Foldout()
            {
                text = "Components",
                value = false,
            };

            var componentName = new TextField()
            {
                value = ComponentName,
            };

            textFoldout.Add(componentName);

            customDataContainer.Add(textFoldout);

            extensionContainer.Add(customDataContainer);

            RefreshExpandedState();
        }
    }
}