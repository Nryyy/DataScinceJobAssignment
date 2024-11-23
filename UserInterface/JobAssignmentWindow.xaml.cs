using DataManagment.Repositories;
using Models;
using System;
using System.Linq;
using System.Windows;

namespace UserInterface
{
    public partial class JobAssignmentWindow : Window
    {
        private readonly DataManagment.AppContext _context;
        private readonly JobAssignmentRepository _jobAssignmentRepository;

        public JobAssignmentWindow()
        {
            InitializeComponent();
            _context = new DataManagment.AppContext();
            _jobAssignmentRepository = new JobAssignmentRepository(_context);
            LoadJobAssignments();
            LoadJobs();
            LoadEmployees();
            LoadCompanies();
            LoadWorkSettings();
        }

        private void LoadJobAssignments()
        {
            JobAssignmentsListBox.ItemsSource = _jobAssignmentRepository.Get().ToList();
        }

        private void LoadJobs()
        {
            JobComboBox.ItemsSource = _context.Jobs.ToList();
        }

        private void LoadEmployees()
        {
            EmployeeComboBox.ItemsSource = _context.Employees.ToList();
        }

        private void LoadCompanies()
        {
            CompanyComboBox.ItemsSource = _context.Companies.ToList();
        }

        private void LoadWorkSettings()
        {
            WorkSettingComboBox.ItemsSource = _context.WorkSettings.ToList();
        }

        private void AddJobAssignmentButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedJob = JobComboBox.SelectedItem as Job;
            var selectedEmployee = EmployeeComboBox.SelectedItem as Employee;
            var selectedCompany = CompanyComboBox.SelectedItem as Company;
            var selectedWorkSetting = WorkSettingComboBox.SelectedItem as WorkSetting;
            var workYear = int.Parse(WorkYearTextBox.Text);
            var salary = decimal.Parse(SalaryTextBox.Text);
            var salaryCurrency = SalaryCurrencyTextBox.Text;

            if (selectedJob == null || selectedEmployee == null || selectedCompany == null || selectedWorkSetting == null)
            {
                MessageBox.Show("Please select all required fields.");
                return;
            }

            var jobAssignment = new JobAssignment
            {
                JobId = selectedJob.Id,
                EmployeeId = selectedEmployee.Id,
                CompanyId = selectedCompany.Id,
                WorkSettingId = selectedWorkSetting.Id,
                WorkYear = workYear,
                Salary = salary,
                SalaryCurrency = salaryCurrency
            };

            _jobAssignmentRepository.Create(jobAssignment);
            LoadJobAssignments();
        }

        private void UpdateJobAssignmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobAssignmentsListBox.SelectedItem is JobAssignment selectedJobAssignment)
            {
                var selectedJob = JobComboBox.SelectedItem as Job;
                var selectedEmployee = EmployeeComboBox.SelectedItem as Employee;
                var selectedCompany = CompanyComboBox.SelectedItem as Company;
                var selectedWorkSetting = WorkSettingComboBox.SelectedItem as WorkSetting;
                var workYear = int.Parse(WorkYearTextBox.Text);
                var salary = decimal.Parse(SalaryTextBox.Text);
                var salaryCurrency = SalaryCurrencyTextBox.Text;

                if (selectedJob == null || selectedEmployee == null || selectedCompany == null || selectedWorkSetting == null)
                {
                    MessageBox.Show("Please select all required fields.");
                    return;
                }

                selectedJobAssignment.JobId = selectedJob.Id;
                selectedJobAssignment.EmployeeId = selectedEmployee.Id;
                selectedJobAssignment.CompanyId = selectedCompany.Id;
                selectedJobAssignment.WorkSettingId = selectedWorkSetting.Id;
                selectedJobAssignment.WorkYear = workYear;
                selectedJobAssignment.Salary = salary;
                selectedJobAssignment.SalaryCurrency = salaryCurrency;

                _jobAssignmentRepository.Update(selectedJobAssignment);
                LoadJobAssignments();
            }
            else
            {
                MessageBox.Show("Please select a job assignment to update.");
            }
        }

        private void DeleteJobAssignmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobAssignmentsListBox.SelectedItem is JobAssignment selectedJobAssignment)
            {
                _jobAssignmentRepository.Delete(selectedJobAssignment.Id);
                LoadJobAssignments();
            }
            else
            {
                MessageBox.Show("Please select a job assignment to delete.");
            }
        }
    }
}
