using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace WpfCalculator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private enum MathOperators {NONE, DIV, MUL, SUB, ADD};
        private MathOperators _operator = MathOperators.NONE;
        private double _total = 0;
        private bool _pending = false;
        private void AllClear(object sender, RoutedEventArgs e) {
            _operator = MathOperators.NONE;
            _total = 0;
            DisplayExpression.Text = "0";
            BtnDiv.Background = Brushes.Orange;
            BtnMul.Background = Brushes.Orange;
            BtnAdd.Background = Brushes.Orange;
            BtnSub.Background = Brushes.Orange;
        }
        private void Evaluate() {
            if(!Double.TryParse(DisplayExpression.Text, out double tmp)) return;
            switch(_operator) {
                case MathOperators.NONE:
                    _total = tmp;
                    break;
                case MathOperators.DIV:
                    _total /= tmp;
                    break;
                case MathOperators.MUL:
                    _total *= tmp;
                    break;
                case MathOperators.SUB:
                    _total -= tmp;
                    break;
                case MathOperators.ADD:
                    _total += tmp;
                    break;
            }
            _operator = MathOperators.NONE;
        }
        private void ButtonDiv(object sender, RoutedEventArgs e) {
            if (!_pending) {
                Evaluate();
                DisplayExpression.Text = "" + _total;
            }
            _operator = MathOperators.DIV;
            _pending = true;
            BtnDiv.Background = Brushes.SaddleBrown;
            BtnMul.Background = Brushes.Orange;
            BtnAdd.Background = Brushes.Orange;
            BtnSub.Background = Brushes.Orange;
        }
        private void ButtonMul(object sender, RoutedEventArgs e) {
            if (!_pending) {
                Evaluate();
                DisplayExpression.Text = "" + _total;
            }
            _pending = true;
            _operator = MathOperators.MUL;
            BtnDiv.Background = Brushes.Orange;
            BtnMul.Background = Brushes.SaddleBrown;
            BtnAdd.Background = Brushes.Orange;
            BtnSub.Background = Brushes.Orange;
        }
        private void ButtonSub(object sender, RoutedEventArgs e) {
            if (!_pending) {
                Evaluate();
                DisplayExpression.Text = "" + _total;
            }
            _pending = true;
            _operator = MathOperators.SUB;
            BtnDiv.Background = Brushes.Orange;
            BtnMul.Background = Brushes.Orange;
            BtnSub.Background = Brushes.SaddleBrown;
            BtnAdd.Background = Brushes.Orange;
        }
        private void ButtonAdd(object sender, RoutedEventArgs e) {
            if (!_pending) {
                Evaluate();
                DisplayExpression.Text = "" + _total;
            }
            _pending = true;
            _operator = MathOperators.ADD;
            BtnDiv.Background = Brushes.Orange;
            BtnMul.Background = Brushes.Orange;
            BtnAdd.Background = Brushes.SaddleBrown;
            BtnSub.Background = Brushes.Orange;
        }
        private void ButtonEqual(object sender, RoutedEventArgs e) {
            Evaluate();
            DisplayExpression.Text = "" + _total;
            _pending = true;
            _operator = MathOperators.NONE;
            BtnDiv.Background = Brushes.Orange;
            BtnMul.Background = Brushes.Orange;
            BtnAdd.Background = Brushes.Orange;
            BtnSub.Background = Brushes.Orange;
        }
        private void ButtonNum(object sender, RoutedEventArgs e) {
            string tag = ((Button)sender).Tag.ToString();
            if (_pending)
                DisplayExpression.Text = tag;
            else {
                if (DisplayExpression.Text != "0")
                    DisplayExpression.Text += tag;
                else DisplayExpression.Text = tag;
            }
            _pending = false;
        }
        private void Button0(object sender, RoutedEventArgs e) {
            if (_pending)
                DisplayExpression.Text = "0";
            else {
                if (DisplayExpression.Text != "0")
                    DisplayExpression.Text += "0";
                else DisplayExpression.Text = "0";
            }
            _pending = false;
        }
        private void ButtonPercent(object sender, RoutedEventArgs e) {
            Double.TryParse(DisplayExpression.Text, out double value);
            value *= 0.01;
            DisplayExpression.Text = value.ToString();
        }
        private void ButtonChangeSign(object sender, RoutedEventArgs e) {
            Double.TryParse(DisplayExpression.Text, out double value);
            value *= -1;
            DisplayExpression.Text = value.ToString();
        }
        private void ButtonDot(object sender, RoutedEventArgs e) {
            if (_pending)
                DisplayExpression.Text = "0.";
            else {
                if ((DisplayExpression.Text).Any(x => x == '.'))
                    return;
                else
                    DisplayExpression.Text += ".";
            }
            _pending = false;
        }
        public MainWindow() {
            this.DataContext = this;
            InitializeComponent();
            DisplayExpression.Text = "0";
        }


    }
}
