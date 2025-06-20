﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Models;
using TimeManagementApp.ViewModels;
using TimeManagementLibrary;
using TimeManagementLibrary.Models;
using TimeManagementLibrary.Services;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace TimeManagementApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        public void InitializeWindow()
        {
            // Initialize your dependencies with DbContext options
            var options = new DbContextOptionsBuilder<TimeManagementDbContext>()
                .UseSqlite("Data Source=timemanagement.db")
                .Options;

            var dbContext = new TimeManagementDbContext(options);
            var dataService = new DataService(dbContext);
            DataContext = new MainViewModel(dataService);
        }

        public void SetLoggedInUser(User user)
        {
            if (DataContext is MainViewModel vm)
            {
                vm.SetUser(user);
            }
        }
    }
}//private void AddModule_Click(object sender, RoutedEventArgs e)
//{
//    // Get module details from input fields
//    string code = txtCode.Text;
//    string name = txtName.Text;

//    // Validate input is number value
//    if (!int.TryParse(txtCredits.Text, out int credits))
//    {
//        MessageBox.Show("Please enter a valid number of credits.");
//        return;
//    }
//    if (!int.TryParse(txtClassHours.Text, out int classHoursPW))
//    {
//        MessageBox.Show("Please enter a valid number of class hours.");
//        return;
//    }
//    if (!int.TryParse(txtWeeks.Text, out int weeksInSemester))
//    {
//        MessageBox.Show("Please enter a valid number of weeks in the semester.");
//        return;
//    }
//    // Get Selected Date
//    DateTime startDate = dpStartDate.SelectedDate ?? DateTime.MinValue;

//    // Create a new module object using the constructor
//    Module newModule = new Module(code, name, credits, classHoursPW, weeksInSemester, startDate);

//    // Add the module to the list
//    modules.Add(newModule);

//    // Clear input fields
//    txtCode.Clear();
//    txtName.Clear();
//    txtCredits.Clear();
//    txtClassHours.Clear();
//    txtWeeks.Clear();
//    dpStartDate.SelectedDate = null;

//    // Update the ListView
//    lstModules.ItemsSource = null; 
//    lstModules.ItemsSource = modules;
//}

//private void RecordStudyHours_Click(object sender, RoutedEventArgs e)
//{
//    if (lstModules.SelectedItem is Module selectedModule)
//    {
//        // Get the selected module
//        double hoursSpent;
//        if (double.TryParse(txtStudyHours.Text, out hoursSpent))
//        {
//            // Get the date from the DatePicker
//            DateTime studyDate = dpStudyDate.SelectedDate ?? DateTime.MinValue;

//            // Create a new study record
//            StudyTimeRecord studyTimeRecord = new StudyTimeRecord
//            {
//                StudyDate = studyDate,
//                HoursSpent = hoursSpent
//            };

//            // Add the study record to the selected module
//            selectedModule.StudyTimeRecords.Add(studyTimeRecord);

//            // Clear input fields
//            txtStudyHours.Clear();
//            dpStudyDate.SelectedDate = null;

//            // Update the ListView
//           lstStudyRecords.ItemsSource = null; 
//           lstStudyRecords.ItemsSource = selectedModule.StudyTimeRecords;                  
//        }
//        else
//        {
//            MessageBox.Show("Please enter a valid number of study hours.");
//        }
//    }
//    else
//    {
//        MessageBox.Show("Please select a module to record study hours.");
//    }
//    lstModules.Items.Refresh();

//}



