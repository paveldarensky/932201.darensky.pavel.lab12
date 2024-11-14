using Microsoft.AspNetCore.Mvc;
using LW_12_WebApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LW_12_WebApplication.Controllers
{
    // CalculatorController.cs
    public class CalculatorController : Controller
    {

        /* MODEL PARSING IN SINGLE ACTION */
        [HttpGet]
        public IActionResult IndexManualParsingInSingleAction()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IndexManualParsingInSingleAction(IFormCollection form)
        {

            var num1Str = form["num1"];
            var num2Str = form["num2"];
            var operationStr = form["operation"];

            if (double.TryParse(num1Str, out double num1) && 
                double.TryParse(num2Str, out double num2) && 
                Enum.TryParse(operationStr, out OperationType operation))
            {
                double result = operation switch
                {
                    OperationType.Add => num1 + num2,
                    OperationType.Subtract => num1 - num2,
                    OperationType.Multiply => num1 * num2,
                    OperationType.Divide => num2 != 0 ? num1 / num2 : double.NaN,
                    _ => 0
                };

                ViewBag.Num1 = num1;
                ViewBag.Num2 = num2;
                ViewBag.Operation = GetOperationSymbol(operation);
                ViewBag.Result = double.IsNaN(result) ? "На ноль делить нельзя!" : result.ToString();

                return View("IndexResult");
            }
            else
            {
                ViewBag.Error = "error: некорректный ввод, введите числа или хоть что-то :( (если вещественные числа, то через ',')";
                return View();
            }
        }
        /* MODEL PARSING IN SINGLE ACTION */


        // для изъятия из enum соответствубщего знака
        private string GetOperationSymbol(OperationType operation)
        {
            var displayAttribute = operation.GetType()
                                            .GetMember(operation.ToString())[0]
                                            .GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? operation.ToString();
        }


        /* MODEL PARSING IN SEPARATE ACTIONS */
        [HttpGet]
        public IActionResult IndexManualParsingInSeparateActions()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IndexManualParsingInSeparateActions(IFormCollection form)
        {

            var num1 = form["num1"];
            var num2 = form["num2"];
            var operation = form["operation"];

            // Action - Validation
            if (!Validate(num1, num2, operation, out double firstNumber, out double secondNumber, out OperationType selectedOperation))
            {
                return View();
            }

            // Action - Calculate 
            double result = Calculate(firstNumber, secondNumber, selectedOperation);

            ViewBag.Num1 = firstNumber;
            ViewBag.Num2 = secondNumber;
            ViewBag.Operation = GetOperationSymbol(selectedOperation);
            ViewBag.Result = double.IsNaN(result) ? "На ноль делить нельзя!" : result.ToString();

            return View("IndexResult");
        }

        // Action - Validation
        private bool Validate(string num1, string num2, string operation, out double firstNumber, out double secondNumber, out OperationType selectedOperation)
        {
            bool isValid = true;
            firstNumber = secondNumber = 0;
            selectedOperation = OperationType.Add; // default

            if (!double.TryParse(num1, out firstNumber) || !double.TryParse(num2, out secondNumber))
            {
                ViewBag.Error = "error: некорректный ввод, введите числа или хоть что-то :( (если вещественные числа, то через ',')";
                isValid = false;
            }

            if (!Enum.TryParse(operation, out selectedOperation))
            {
                ViewBag.Error = "error";
                isValid = false;
            }

            return isValid;
        }

        // Action - Calculate
        private double Calculate(double num1, double num2, OperationType operation)
        {
            return operation switch
            {
                OperationType.Add => num1 + num2,
                OperationType.Subtract => num1 - num2,
                OperationType.Multiply => num1 * num2,
                OperationType.Divide => num2 != 0 ? num1 / num2 : double.NaN,
                _ => 0
            };
        }
        /* MODEL PARSING IN SEPARATE ACTIONS */




        /* MODEL BINDING (PARAMETERS) */
        [HttpGet]
        public IActionResult IndexModelBindingParameters()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IndexModelBindingParameters(string num1, string num2, OperationType operation)
        {

            if (!double.TryParse(num1, out double number1) || !double.TryParse(num2, out double number2))
            {
                ViewBag.Error = "error: некорректный ввод, введите числа или хоть что-то :( (если вещественные числа, то через ',')";
                return View();
            }

            double result = operation switch
            {
                OperationType.Add => number1 + number2,
                OperationType.Subtract => number1 - number2,
                OperationType.Multiply => number1 * number2,
                OperationType.Divide => number2 != 0 ? number1 / number2 : double.NaN,
                _ => 0
            };

            ViewBag.Num1 = num1;
            ViewBag.Num2 = num2;
            ViewBag.Operation = GetOperationSymbol(operation);
            ViewBag.Result = double.IsNaN(result) ? "На ноль делить нельзя!" : result.ToString();

            return View("IndexResult");
        }
        /* MODEL BINDING (PARAMETERS) */




        /* MODEL BINDING (SEPARATE MODEL) */
        [HttpGet]
        public IActionResult IndexModelBindingSeparateModel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IndexModelBindingSeparateModel(CalcModel model)
        {
            if (ModelState.IsValid)
            {
                double result = model.Operation switch
                {
                    OperationType.Add => model.Num1 + model.Num2,
                    OperationType.Subtract => model.Num1 - model.Num2,
                    OperationType.Multiply => model.Num1 * model.Num2,
                    OperationType.Divide => model.Num2 != 0 ? model.Num1 / model.Num2 : double.NaN,
                    _ => 0
                };

                ViewBag.Num1 = model.Num1;
                ViewBag.Num2 = model.Num2;
                ViewBag.Operation = GetOperationSymbol(model.Operation);
                ViewBag.Result = double.IsNaN(result) ? "На ноль делить нельзя!" : result.ToString();
                return View("IndexResult");
            }
            else
            {
                ViewBag.Error = "error: некорректный ввод, введите числа или хоть что-то :( (если вещественные числа, то через ',')";
                return View();
            }
        }
        /* MODEL BINDING (SEPARATE MODEL) */
    }

}
