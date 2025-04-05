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

namespace TimeManagementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Module> modules = new List<Module>();
        private List<StudyTimeRecord> studyTimeRecord = new List<StudyTimeRecord>();
        public MainWindow()
        {
            InitializeComponent();
            UpdateSelfStudyHours();
        }

        private void moduleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void AddModule_Click(object sender, RoutedEventArgs e)
        {
            // Get module details from input fields
            string code = txtCode.Text;
            string name = txtName.Text;

            //Validate input is number value
            if (!int.TryParse(txtCredits.Text, out int credits))
            {
                MessageBox.Show("Please enter a valid number of credits.");
                return;
            }
            if (!int.TryParse(txtClassHours.Text, out int classHoursPW))
            {
                MessageBox.Show("Please enter a valid number of class hours.");
                return;
            }
            if (!int.TryParse(txtWeeks.Text, out int weeksInSemester))
            {
                MessageBox.Show("Please enter a valid number of weeks in the semester.");
                return;
            }
            //Get Selected Date
            DateTime startDate = dpStartDate.SelectedDate ?? DateTime.MinValue;

            // Create a new module object
            Module newModule = new Module
            {
                Code = code,
                Name = name,
                Credits = credits,
                ClassHoursPW = classHoursPW,
                WeeksInSemester = weeksInSemester,
                StartDate = startDate
            };

            // Add the module to the list
            modules.Add(newModule);

            // Clear input fields
            txtCode.Clear();
            txtName.Clear();
            txtCredits.Clear();
            txtClassHours.Clear();
            txtWeeks.Clear();
            dpStartDate.SelectedDate = null;

            // Update the ListView
            lstModules.ItemsSource = modules;

            UpdateSelfStudyHours();
        }

        private void RecordStudyHours_Click(object sender, RoutedEventArgs e)
        {
            if (lstModules.SelectedItem is Module selectedModule)
            {
                // Get the selected module
                double hoursSpent;
                if (double.TryParse(txtStudyHours.Text, out hoursSpent))
                {
                    // Get the date from the DatePicker
                    DateTime studyDate = dpStudyDate.SelectedDate ?? DateTime.MinValue;

                    // Create a new study record
                    StudyTimeRecord studyTimeRecord = new StudyTimeRecord
                    {
                        StudyDate = studyDate,
                        HoursSpent = hoursSpent
                    };

                    // Add the study record to the selected module
                    selectedModule.StudyTimeRecord.Add(studyTimeRecord);

                    // Clear input fields
                    txtStudyHours.Clear();
                    dpStudyDate.SelectedDate = null;

                    // Update the ListView
                    lstStudyRecords.ItemsSource = selectedModule.StudyTimeRecord;
                    UpdateSelfStudyHours();
                }
                else
                {
                    MessageBox.Show("Please enter a valid number of study hours.");
                }
            }
            else
            {
                MessageBox.Show("Please select a module to record study hours.");
            }
        }

        private void UpdateSelfStudyHours()
        {
            foreach (var module in modules)
            {
                module.CalcRemainingStudyHours();
            }

            lstModules.Items.Refresh(); // Refresh the ListView to update the display
        }


    }
}
