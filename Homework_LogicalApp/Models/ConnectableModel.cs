using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_LogicalApp.Models
{
    public class ConnectableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public int? InputId { get; set; }
        public int? OutputId { get; set; }
    }
}
