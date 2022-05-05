using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;

namespace Driver.Tools
{
    // ReSharper disable once UnusedMember.Global
    public static class CapterAttachTreeMenuAction
    {
        [MenuAction]
        // ReSharper disable once UnusedMember.Global
        public static void AssertAll(object obj, string accessPath)
        {
            foreach (var e in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (e.GetIndexParameters().Length != 0) continue;
                var objTmp = e.GetValue(obj);
                if (objTmp == null) continue;

                var childAccessPath = accessPath + "." + e.Name;
                if (objTmp is WPFComboBox comboBox) Assert(comboBox, childAccessPath);
                else if (objTmp is WPFListBox listBox) Assert(listBox, childAccessPath);
                else if (objTmp is WPFListView listView) Assert(listView, childAccessPath);
                else if (objTmp is WPFProgressBar progressBar) Assert(progressBar, childAccessPath);
                else if (objTmp is WPFRichTextBox richTextBox) Assert(richTextBox, childAccessPath);
                else if (objTmp is WPFSelector selector) Assert(selector, childAccessPath);
                else if (objTmp is WPFSlider slider) Assert(slider, childAccessPath);
                else if (objTmp is WPFTabControl tabControl) Assert(tabControl, childAccessPath);
                else if (objTmp is WPFTextBox textBox) Assert(textBox, childAccessPath);
                else if (objTmp is WPFTextBlock textBlock) Assert(textBlock, childAccessPath);
                else if (objTmp is WPFToggleButton toggleButton) Assert(toggleButton, childAccessPath);
                else if (objTmp is WPFTreeView treeView) Assert(treeView, childAccessPath);
                else if (objTmp is WPFCalendar calendar) Assert(calendar, childAccessPath);
                else if (objTmp is WPFDatePicker dataPicker) Assert(dataPicker, childAccessPath);
                else if (objTmp is WPFDataGrid dataGrid) Assert(dataGrid, childAccessPath);
            }
        }

        [MenuAction]
        public static void Assert(WPFComboBox comboBox, string accessPath)
            => CaptureAdaptor.AddCode($"{accessPath}.SelectedIndex.Is({comboBox.SelectedIndex});");

        [MenuAction]
        public static void Assert(WPFListBox listBox, string accessPath)
            => CaptureAdaptor.AddCode($"{accessPath}.SelectedIndex.Is({listBox.SelectedIndex});");

        [MenuAction]
        public static void Assert(WPFListView listView, string accessPath)
            => CaptureAdaptor.AddCode($"{accessPath}.SelectedIndex.Is({listView.SelectedIndex});");

        [MenuAction]
        public static void Assert(WPFProgressBar progressBar, string accessPath)
            => CaptureAdaptor.AddCode($"{accessPath}.Value.Is({progressBar.Value});");

        [MenuAction]
        public static void Assert(WPFRichTextBox richTextBox, string accessPath)
            => CaptureAdaptor.AddCode($"{accessPath}.Text.Is({ToLiteral(richTextBox.Text)});");

        [MenuAction]
        public static void Assert(WPFSelector selector, string accessPath)
            => CaptureAdaptor.AddCode($"{accessPath}.SelectedIndex.Is({selector.SelectedIndex});");

        [MenuAction]
        public static void Assert(WPFSlider slider, string accessPath)
            => CaptureAdaptor.AddCode($"{accessPath}.Value.Is({slider.Value});");

        [MenuAction]
        public static void Assert(WPFTabControl tabControl, string accessPath)
            => CaptureAdaptor.AddCode($"{accessPath}.SelectedIndex.Is({tabControl.SelectedIndex});");

        [MenuAction]
        public static void Assert(WPFTextBox textBox, string accessPath)
            => CaptureAdaptor.AddCode($"{accessPath}.Text.Is({ToLiteral(textBox.Text)});");

        [MenuAction]
        public static void Assert(WPFTextBlock textBlock, string accessPath)
            => CaptureAdaptor.AddCode($"{accessPath}.Text.Is({ToLiteral(textBlock.Text)});");

        [MenuAction]
        public static void Assert(WPFToggleButton toggleButton, string accessPath)
        {
            var value = toggleButton.IsChecked == null ? "null" : toggleButton.IsChecked.Value.ToString().ToLower();
            CaptureAdaptor.AddCode($"{accessPath}.IsChecked.Is({value});");
        }

        [MenuAction]
        public static void Assert(WPFTreeView treeView, string accessPath)
        {
            CaptureAdaptor.AddCode(treeView.SelectedItem.AppVar.IsNull
                ? $"{accessPath}.SelectedItem.AppVar.IsNull.IsTrue();"
                : $"{accessPath}.SelectedItem.Text.Is({ToLiteral(treeView.SelectedItem.Text)});");
        }

        [MenuAction]
        public static void Assert(WPFCalendar calendar, string accessPath)
        {
            CaptureAdaptor.AddUsing(typeof(DateTime).Namespace);
            if (calendar.SelectedDate.HasValue)
            {
                var value = calendar.SelectedDate.Value;
                CaptureAdaptor.AddCode($"{accessPath}.SelectedDate.Is(new DateTime({value.Year}, {value.Month}, {value.Day}));");
            }
            else
            {
                CaptureAdaptor.AddCode($"{accessPath}.SelectedDate.IsNull();");
            }
        }

        [MenuAction]
        public static void Assert(WPFDatePicker datePicker, string accessPath)
        {
            CaptureAdaptor.AddUsing(typeof(DateTime).Namespace);
            if (datePicker.SelectedDate.HasValue)
            {
                var value = datePicker.SelectedDate.Value;
                CaptureAdaptor.AddCode($"{accessPath}.SelectedDate.Is(new DateTime({value.Year}, {value.Month}, {value.Day}));");
            }
            else
            {
                CaptureAdaptor.AddCode($"{accessPath}.SelectedDate.IsNull();");
            }
        }

        [MenuAction]
        public static void Assert(WPFDataGrid dataGrid, string accessPath)
        {
            var rowCount = dataGrid.ItemCount;
            var colCount = dataGrid.ColCount;
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    var text = ToLiteral(dataGrid.GetCellText(row, col));
                    CaptureAdaptor.AddCode($"{accessPath}.GetCellText({row}, {col}).Is({text});");
                }
            }
        }

        static string ToLiteral(string text)
        {
            using var writer = new StringWriter();
            using var provider = CodeDomProvider.CreateProvider("CSharp");
            var expression = new CodePrimitiveExpression(text);
            provider.GenerateCodeFromExpression(expression, writer, options: null);
            return writer.ToString();
        }
    }
}
