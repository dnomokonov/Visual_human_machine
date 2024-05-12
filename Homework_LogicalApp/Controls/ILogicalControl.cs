using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_LogicalApp.Controls
{
    public interface ILogicalControl
    {
        ObservableCollection<bool> GetOutput(ObservableCollection<bool> input);
    }
}
