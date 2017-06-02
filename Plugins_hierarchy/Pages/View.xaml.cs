﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using Plugins_hierarchy.Class;
using PluginApi;

namespace Plugins_hierarchy.Pages {
    public partial class View : Page {
        private Plugin plugin;
        private int index;
        private List<TextBox> inputField = new List<TextBox>();
        private List<TextBox> inputProperty = new List<TextBox>();
        private MainWindow super = new MainWindow();

        public View(int index, MainWindow super) {
            InitializeComponent(); Object obj = super.objectList[index];
            foreach (var item in Api.GetInstance().getPluginList()) {
                if (item.objectType == obj.GetType()) {
                    this.plugin = item;
                }
            }

            this.super = super;
            this.index = index;
            CreateMarkup();
            DisplayItem();
        }

        private void CreateMarkup() {
            PluginApi.IPlugin obj = (PluginApi.IPlugin)this.plugin.CreateObject();
            List<string> fieldsInput = obj.getFields();
            List<string> propsInput = obj.getProperties();
            Grid dynamicGrid = CreateGrid(fieldsInput, propsInput);
            CreateInput(dynamicGrid, fieldsInput, propsInput);
        }

        private Grid CreateGrid(List<string> fieldsInput, List<string> propsInput) {
            Grid dynamicGrid = new Grid();

            List<RowDefinition> rows = new List<RowDefinition>();
            foreach (var item in fieldsInput) {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(30);
                rows.Add(row);
            }
            foreach (var item in propsInput) {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(30);
                rows.Add(row);
            }
            RowDefinition rowBtn = new RowDefinition();
            rowBtn.Height = new GridLength(30);
            rows.Add(rowBtn);

            ColumnDefinition colInput = new ColumnDefinition();
            ColumnDefinition colDescription = new ColumnDefinition();
            colDescription.Width = new GridLength(100);

            dynamicGrid.ColumnDefinitions.Add(colDescription);
            dynamicGrid.ColumnDefinitions.Add(colInput);
            foreach (var item in rows) {
                dynamicGrid.RowDefinitions.Add(item);
            }

            return dynamicGrid;
        }

        private TextBox CreateInputRow(string stringInput, int pos, Grid dynamicGrid) {
            TextBlock label = new TextBlock();
            label.Text = stringInput;
            label.Margin = new Thickness(5, 5, 5, 5);
            Grid.SetRow(label, pos);
            Grid.SetColumn(label, 0);
            dynamicGrid.Children.Add(label);

            TextBox inputBox = new TextBox();
            inputBox.Margin = new Thickness(5, 5, 5, 5);
            Grid.SetRow(inputBox, pos);
            Grid.SetColumn(inputBox, 1);
            dynamicGrid.Children.Add(inputBox);
            return inputBox;
        }

        private void CreateInput(Grid dynamicGrid, List<string> fieldsInput, List<string> propsInput) {
            int index = 0;
            for (int i = 0; i < fieldsInput.Count; i++) {
                TextBox inputBox = CreateInputRow(fieldsInput[i], index, dynamicGrid);
                inputField.Add(inputBox);
                index++;
            }

            for (int i = 0; i < propsInput.Count; i++) {
                TextBox inputBox = CreateInputRow(propsInput[i], index, dynamicGrid);
                inputProperty.Add(inputBox);
                index++;
            }

            this.Content = dynamicGrid;
        }

        private void DisplayItem() {
            PluginApi.IPlugin obj = (PluginApi.IPlugin) super.objectList[index];
            List<string> field = obj.getFields();
            List<string> property = obj.getProperties();

            for (int i = 0; i < field.Count; i++) {
                inputField[i].Text = this.plugin.objectType.GetField(field[i]).GetValue(obj).ToString();
            }
            for (int i = 0; i < property.Count; i++) {
                inputProperty[i].Text = this.plugin.objectType.GetProperty(property[i]).GetValue(obj).ToString();
            }
        }
    }
}
