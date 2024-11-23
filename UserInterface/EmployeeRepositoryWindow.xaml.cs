using DataManagment.Repositories;
using Models;
using System;
using System.Linq;
using System.Windows;

namespace UserInterface
{
    public partial class EmployeeManagementWindow : Window
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly DataManagment.AppContext _context;

        public EmployeeManagementWindow()
        {
            InitializeComponent();
            _context = new DataManagment.AppContext();
            _employeeRepository = new EmployeeRepository(_context);
            LoadEmployees();
            LoadExperienceLevels();
        }

        private void LoadEmployees()
        {
            EmployeesListBox.ItemsSource = _employeeRepository.Get().ToList();
        }

        private void LoadExperienceLevels()
        {
            ExperienceLevelComboBox.ItemsSource = _context.ExperienceLevels.ToList();
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            var residence = EmployeeResidenceTextBox.Text;
            var selectedExperienceLevel = ExperienceLevelComboBox.SelectedItem as ExperienceLevel;

            if (string.IsNullOrEmpty(residence) || selectedExperienceLevel == null)
            {
                MessageBox.Show("Please enter a residence and select an experience level.");
                return;
            }

            var employee = new Employee
            {
                Residence = residence,
                ExperienceLevelId = selectedExperienceLevel.Id,
                ExperienceLevel = selectedExperienceLevel
            };

            _employeeRepository.Create(employee);
            LoadEmployees();
        }

        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesListBox.SelectedItem is Employee selectedEmployee)
            {
                var residence = EmployeeResidenceTextBox.Text;
                var selectedExperienceLevel = ExperienceLevelComboBox.SelectedItem as ExperienceLevel;

                if (string.IsNullOrEmpty(residence) || selectedExperienceLevel == null)
                {
                    MessageBox.Show("Please enter a residence and select an experience level.");
                    return;
                }

                selectedEmployee.Residence = residence;
                selectedEmployee.ExperienceLevelId = selectedExperienceLevel.Id;
                selectedEmployee.ExperienceLevel = selectedExperienceLevel;

                _employeeRepository.Update(selectedEmployee);
                LoadEmployees();
            }
            else
            {
                MessageBox.Show("Please select an employee to update.");
            }
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesListBox.SelectedItem is Employee selectedEmployee)
            {
                _employeeRepository.Delete(selectedEmployee.Id);
                LoadEmployees();
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.");
            }
        }
    }
}
