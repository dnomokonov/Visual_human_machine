using Avalonia;
using Homework_LogicalApp.Controls;
using Homework_LogicalApp.Models;
using Homework_LogicalApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_LogicalApp.Helpers
{
    public static class ControlSerializer
    {
        public static ConnectableModel Serialize(Connectable component)
        {
            var model = new ConnectableModel()
            {
                Id = component.Id,
                Name = component.GetType().Name,
                InputId = component.input_el?.Id,
                OutputId = component.output_el?.Id,
                PosX = component.Position.X,
                PosY = component.Position.Y
            };
            return model;
        }

        public static Connectable? Deserialize(ConnectableModel model)
        {
            Type type = Type.GetType($"Homework_LogicalApp.Controls.{model.Name}");
            if (type == null || !typeof(Connectable).IsAssignableFrom(type))
            {
                // Log error or handle type mismatch scenario
                Console.WriteLine("Deserialization error: Type not found or incorrect.");
                return null;
            }

            var component = Activator.CreateInstance(type, model.Id, new Point(model.PosX, model.PosY)) as Connectable;
            if (component == null)
            {
                // Log error or handle failed instantiation
                Console.WriteLine("Deserialization error: Instantiation failed.");
                return null;
            }

            component.Id = model.Id;
            component.SetPosition(new Avalonia.Point(model.PosX, model.PosY));

            return component;
        }
    }
}
