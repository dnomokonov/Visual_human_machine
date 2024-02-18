using System;
using System.ComponentModel;

namespace Homework3
{
    public class Calculator : INotifyPropertyChanged
    {
        private string _currentInput = "0";
        private double _currentResult = 0;
        private double _previousOperand = 0;
        private char? _lastOperator = null;
        private char? _previousOperator = null;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string CurrentInput
        {
            get => _currentInput;
            private set
            {
                _currentInput = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentInput)));
            }
        }

        public void AppendInput(char input)
        {
            if (_currentInput == "0" && input != ',')
            {
                _currentInput = input.ToString();
            }
            else
            {
                _currentInput += input;
            }
        }

        public void NumberInput(char number)
        {
            AppendInput(number);
            CurrentInput = _currentInput;
        }

        public void OperatorInput(char op)
        {
            if (_lastOperator != null)
            {
                PerformOperation();
            }

            if (_currentResult != 0)
            {
                _currentInput = _currentResult.ToString();
            }
            else
            {
                _currentResult = double.Parse(_currentInput);
            }

            _lastOperator = op;

            CurrentInput = "0";
        }

        public void EqualsInput()
        {
            if (_lastOperator != null)
            {
                PerformOperation();
                _previousOperator = _lastOperator;
                _previousOperand = double.Parse(_currentInput);
                _lastOperator = null;
            }
            else if (_previousOperator != null)
            {
                PerformPreviousOperation();
            }
            CurrentInput = _currentResult.ToString();
        }

        public void ClearInput()
        {
            CurrentInput = "0";
            _currentResult = 0;
            _lastOperator = null;
        }

        public void BackspaceInput()
        {
            if (_currentInput.Length > 0)
            {
                _currentInput = _currentInput.Remove(_currentInput.Length - 1);
                CurrentInput = _currentInput;
                if (_currentInput.Length == 0)
                {
                    _currentInput = "0";
                    CurrentInput = _currentInput;
                }
            } 
        }

        public void ModFunction()
        {
            if (_lastOperator != null)
            {
                PerformOperation();
            }
            _lastOperator = '%';
            _currentResult = double.Parse(_currentInput);
            CurrentInput = "0";
        }

        public void PowerFunction()
        {
            if (_lastOperator != null)
            {
                PerformOperation();
            }
            _lastOperator = '^';
            _currentResult = double.Parse(_currentInput);
            CurrentInput = "0";
        }

        public void LogFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue) && inputValue > 0)
            {
                _currentResult = Math.Log10(inputValue);
                CurrentInput = _currentResult.ToString();
            }
            else
            {
                CurrentInput = "0";
            }
        }

        public void LnFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue) && inputValue > 0)
            {
                _currentResult = Math.Log(inputValue);
                CurrentInput = _currentResult.ToString();
            }
            else
            {
                CurrentInput = "0";
            }
        }

        public void SinFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue))
            {
                _currentResult = Math.Sin(inputValue);
                CurrentInput = _currentResult.ToString();
            }
            else
            {
                CurrentInput = "0";
            }
        }

        public void CosFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue))
            {
                _currentResult = Math.Cos(inputValue);
                CurrentInput = _currentResult.ToString();
            }
            else
            {
                CurrentInput = "0";
            }
        }

        public void TanFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue))
            {
                _currentResult = Math.Tan(inputValue);
                CurrentInput = _currentResult.ToString();
            }
            else
            {
                CurrentInput = "0";
            }
        }

        public void FloorFunction()
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                double inputValue = double.Parse(_currentInput);
                _currentResult = Math.Floor(inputValue);
                CurrentInput = _currentResult.ToString();
            }
        }

        public void CeilFunction()
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                double inputValue = double.Parse(_currentInput);
                _currentResult = Math.Ceiling(inputValue);
                CurrentInput = _currentResult.ToString();
            }

        }
        public void FactorialFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue))
            {
                if (inputValue >= 0 && Math.Floor(inputValue) == inputValue)
                {
                    double result = 1;
                    for (int i = 2; i <= inputValue; i++)
                    {
                        result *= i;
                    }
                    _currentResult = result;
                    CurrentInput = _currentResult.ToString();
                }
                else
                {
                    CurrentInput = "0";
                }
            }
            else
            {
                CurrentInput = "0";
            }
        }

        public void CommaInput()
        {
            if (!_currentInput.Contains(','))
            {
                AppendInput(',');
                CurrentInput = _currentInput;
            }
        }

        private void PerformOperation()
        {
            if (_currentInput.Length > 0)
            {
                double operand = double.Parse(_currentInput);
                switch (_lastOperator)
                {
                    case '+':
                        _currentResult += operand;
                        break;
                    case '-':
                        _currentResult -= operand;
                        break;
                    case '*':
                        _currentResult *= operand;
                        break;
                    case '/':
                        _currentResult /= operand;
                        break;
                    case '%':
                        _currentResult %= operand;
                        break;
                    case '^':
                        _currentResult = Math.Pow(_currentResult, operand);
                        break;
                }
            }
        }

        public void ProcessButtonClick(string buttonContent)
        {
            if (char.IsDigit(buttonContent[0]))
            {
                NumberInput(buttonContent[0]);
            }
            else if (buttonContent == "+" || buttonContent == "-" || buttonContent == "*" || buttonContent == "/")
            {
                OperatorInput(buttonContent[0]);
            }
            else if (buttonContent == "=")
            {
                EqualsInput();
            }
            else if (buttonContent == "C")
            {
                ClearInput();
            }
            else if (buttonContent == "←")
            {
                BackspaceInput();
            }
            else if (buttonContent == "mod")
            {
                ModFunction();
            }
            else if (buttonContent == "^")
            {
                PowerFunction();
            }
            else if (buttonContent == "lg")
            {
                LogFunction();
            }
            else if (buttonContent == "ln")
            {
                LnFunction();
            }
            else if (buttonContent == "sin")
            {
                SinFunction();
            }
            else if (buttonContent == "cos")
            {
                CosFunction();
            }
            else if (buttonContent == "tan")
            {
                TanFunction();
            }
            else if (buttonContent == "floor")
            {
                FloorFunction();
            }
            else if (buttonContent == "ceil")
            {
                CeilFunction();
            }
            else if (buttonContent == "n!")
            {
                FactorialFunction();
            }
            else if (buttonContent == ",")
            {
                CommaInput();
            }
        }

        private void PerformPreviousOperation()
        {
            if (_previousOperator != null)
            {
                switch (_previousOperator)
                {
                    case '+':
                        _currentResult += _previousOperand;
                        break;
                    case '-':
                        _currentResult -= _previousOperand;
                        break;
                    case '*':
                        _currentResult *= _previousOperand;
                        break;
                    case '/':
                        _currentResult /= _previousOperand;
                        break;
                    case '%':
                        _currentResult %= _previousOperand;
                        break;
                    case '^':
                        _currentResult = Math.Pow(_currentResult, _previousOperand);
                        break;
                }
            }
        }
    }
}
